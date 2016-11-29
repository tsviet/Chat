using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Chat
{
    //Class to handle each client request separatly
    public class HandleClinet
    {
        TcpClient clientSocket;
        private static string userName = "";
        int clNo;

        public enum Command
        {
            NotConnected,
            Error, 
            OK //Server done operation successfully
        };

        public void startClient(TcpClient inClientSocket, ref int clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        public static void SendResponce(NetworkStream networkStream, string message)
        {
            string serverResponse = message;
            byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }

        private void doChat()
        {
            byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
            string dataFromClient = null;

            while ((true))
            {
                try
                {                   
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                    dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    string param = dataFromClient.Split('~')[1];
                    //Set user name for this client
                    if (dataFromClient.Contains("SetUser~"))
                    {
                        userName = param;
                        SendResponce(networkStream, "OK");
                    }
                    //Add message to chatroom data structure
                   /* else if (dataFromClient.Contains("sendMessage~@"))
                    {
                        string[] res = param.Split(';');
                        if (string.IsNullOrEmpty(res[0]) || string.IsNullOrWhiteSpace(res[0]))
                        {
                            SendResponce(networkStream, "Choose room on a left or create one!");
                        }
                        else
                        {
                            var sendTo = Regex.Match(res[0], @"(is?)@(\w+) ").Groups[1].Value;
                            Server.GetMessageList(res[0]).Add(userName + " says: " + res[1]); 
                            SendResponce(networkStream, "202.OK");
                        }

                    }*/
                    else if (dataFromClient.Contains("sendMessage~"))
                    {
                        string[] res = param.Split(';');
                        if (string.IsNullOrEmpty(res[0]) || string.IsNullOrWhiteSpace(res[0]))
                        {
                            SendResponce(networkStream, "Choose room on a left or create one!");
                        }
                        else
                        {
                            Server.GetMessageList(res[0]).Add(userName + " says: " + res[1]);
                            SendResponce(networkStream, "202.OK");
                        }

                    }
                    //Create chat room
                    else if (dataFromClient.Contains("create~"))
                    {
                        if (!Server.HasRoom(param))
                        {
                            ChatRoom chatroom = new ChatRoom(param);
                            chatroom.AddUser(userName);
                            Server.AddRoom(chatroom);
                            SendResponce(networkStream, "Created");
                        }
                        //Send error on existing chat room
                        else
                        {
                            SendResponce(networkStream, "Error: " + param + " exists.");
                        }
                    }
                    else if (dataFromClient.Contains("DisconnectClient~"))
                    {
                        clientSocket.Close();
                        Console.WriteLine("Client " + clNo + " disconnected!!");
                        break;
                    }
                    else if (dataFromClient.Contains("GetAllMessages~"))
                    {
                        if (Server.HasRoom(param))
                        {
                            string res = "";

                            foreach (var m in Server.GetMessageList(param))
                            {
                                res += m + ";";
                            }
                            SendResponce(networkStream, "Messages~" + res);
                        }
                        else
                        {
                            SendResponce(networkStream, "No chat room exist!");
                        }
                    }
                    else if (dataFromClient.Contains("RefreshRooms~"))
                    {
                        if (Server.ServerExist())
                        {
                            string res = "";

                            foreach (var m in Server.GetChatRoomList())
                            {
                                res += m.Value.GetName() + ";";
                            }
                            SendResponce(networkStream, "ChatRoomsNames~" + res);
                        }
                        else
                        {
                            SendResponce(networkStream, "No chat room exist!");
                        }
                    }
                    else if (dataFromClient.Contains("RefreshCurrentRoom~"))
                    {
                        if (Server.ServerExist())
                        {
                            string res = "";

                            foreach (var m in Server.GetMessageList())
                            {
                                res += m + ";";
                            }
                            SendResponce(networkStream, "RefreshCurrentRoom~" + res);
                        }
                        else
                        {
                            SendResponce(networkStream, "No chat room exist!");
                        }
                    }

                    else if (dataFromClient.Contains("ListUsers~"))
                    {
                        if (Server.ServerExist())
                        {
                            string res = "";

                            foreach (var m in Server.GetCurrentChatRoom().GetUserList())
                            {
                                res += m + ";";
                            }
                            SendResponce(networkStream, "ListUsers~" + res);
                        }
                        else
                        {
                            SendResponce(networkStream, "No chat room exist!");
                        }
                    }
                    else if (dataFromClient.Contains("JoinRoom~"))
                    {
                        if (Server.ServerExist())
                        {
                            string[] res = param.Split(';');

                            if (Server.GetUserList(res[1]).Contains(res[0]))
                            {
                                SendResponce(networkStream, "Can't join room " + res[1] + " user " + res[0] + " already in this room.");
                            }
                            else
                            {
                                Server.GetUserList(res[1]).Add(res[0]);
                                SendResponce(networkStream, "User " + res[0] + " joined room " + res[1]);
                            }
                        }
                        else
                        {
                            SendResponce(networkStream, "No chat room exist!");
                        }
                    }

                    else if (dataFromClient.Contains("UpdateDropDown~"))
                    {
                        if (Server.ServerExist())
                        {
                            string res = "";
                            foreach (var room in Server.GetChatRoomList().Values)
                            {
                                if (room.GetUserList().Contains(param))
                                {
                                    res += room.GetName() + ";";
                                }
                                
                            }
                            SendResponce(networkStream, "UpdateDropDown~" + res);
                        }
                        else
                        {
                            SendResponce(networkStream, "No chat room exist!");
                        }
                    }

                    else if (dataFromClient.Contains("LeaveRoom~"))
                    {
                        if (Server.ServerExist())
                        {
                            string[] res = param.Split(';');
                            if (Server.GetUserList(res[1]).Contains(res[0]))
                            {
                                Server.GetUserList(res[1]).Remove(res[0]);
                                SendResponce(networkStream, "User " + res[0] + " leaved room " + res[1]);
                            }
                            else
                            {
                                SendResponce(networkStream, "Can't leave room user " + res[1] + " not in this room " + res[0]);
                            }
                        }
                        else
                        {
                            SendResponce(networkStream, "No chat room exist!");
                        }
                    }
                    
                }
                catch (Exception)
                {
                    clientSocket.Close();
                    Console.WriteLine("Client " + clNo + " disconnected!!");
                    break;
                }
            }//while

            Thread.CurrentThread.Abort();
        }
    }
}