using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class Program
    {
        private static string userName = "";
        static void Main(string[] args)
        {

            TcpListener serverSocket = new TcpListener(Dns.GetHostEntry("localhost").AddressList[1], 8888);
            int requestCount = 0;
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Accept connection from client");
            requestCount = 0;
            Server server = new Server();

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                    string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    string param = dataFromClient.Split('~')[1];
                    //Create chatroom and name it and addcurrent user to it
                    if (dataFromClient.Contains("SetUser~"))
                    {
                        userName = param;
                    } else if (dataFromClient.Contains("create~"))
                    {
                        if (!server.HasRoom(param))
                        {
                            ChatRoom chatroom = new ChatRoom(param);
                            chatroom.AddUser(userName);
                            server.AddRoom(chatroom);
                            SendResponce(networkStream, "Created");
                        } else
                        {
                            SendResponce(networkStream, "Error: " + param + " exists.");
                        }
                    }                   
                    else
                    {
                        SendResponce(networkStream, userName + " says: " + dataFromClient);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> exit");
            Console.ReadLine();
        }

        public static void SendResponce(NetworkStream networkStream, string message)
        {
            string serverResponse = message;
            Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }

    }
}
