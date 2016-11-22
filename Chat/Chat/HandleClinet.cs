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
        string clNo;

        public void startClient(TcpClient inClientSocket, string clineNo)
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
                        } else
                        {
                            Server.AddMessage(userName, param);
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

                    //On Add to message list update all threads chat window
                    if (Server.HasRoom(param))
                    {
                        var ser = Server.GetMessageList();
                        ser.CollectionChanged += (sender, e) =>
                        {
                            if (e.Action == NotifyCollectionChangedAction.Add)
                            {
                                string res = "";

                                foreach (var m in ser)
                                {
                                    res += m + ";";
                                }
                                SendResponce(clientSocket.GetStream(), "Messages~" + res);
                            }
                        };

                       /* var users = Server.GetUserList();
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
                        };*/
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                    break;
                }
            }
        }
    }
}