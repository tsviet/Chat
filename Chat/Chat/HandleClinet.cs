using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat
{
    //Class to handle each client request separatly
    public class HandleClinet
    {
        TcpClient clientSocket;
        private static string userName = "";
        int clNo;

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
                    else if (dataFromClient.Contains("sendMessage~"))
                    {
                        if (string.IsNullOrEmpty(Server.GetActiveRoom()))
                        {
                            SendResponce(networkStream, "Error: there is no active room available... create one first!");
                        }
                        else
                        {
                            Server.AddMessage(userName, param);
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
                    }

                    /* //On Add to message list update all threads chat window
                     if (Server.HasRoom(param))
                     {
                         var ser = Server.GetMessageList();
                         ser.CollectionChanged += (sender, e) =>
                         {
                             if (e.Action == NotifyCollectionChangedAction.Add)
                             {
                                 string res = "";

                                 foreach (var m in Server.GetMessageList())
                                 {
                                     res += m + ";";
                                 }
                                 SendResponce(clientSocket.GetStream(), "Messages~" + res);
                             }
                         };

                         var users = Server.GetUserList();
                         users.CollectionChanged += (sender, e) =>
                         {
                             Console.WriteLine("Adding user...");
                             if (e.Action == NotifyCollectionChangedAction.Add)
                             {
                                 string res = "";

                                 foreach (var m in users)
                                 {
                                     res += m + ";";
                                 }
                                 SendResponce(networkStream, "Users~" + res);
                             }
                         };
                     }*/

                }
                catch (Exception ex)
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