using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using WindowsFormsApplication1;
using static WindowsFormsApplication1.Form1;

namespace Client
{
    public class Messages
    {
        private TcpClient clientSocket = new TcpClient();
        private NetworkStream serverStream = null;
        //private string roomName = "";
        //private string[] server;
        private Object _lock = new Object();

        public Message RPC(Message message)
        {
            lock (_lock)
            {
                try
                {
                    string json_out = JsonConvert.SerializeObject(message, Formatting.Indented);
                    //Made this code save for threads
                    byte[] outStream = Encoding.ASCII.GetBytes(json_out);
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();

                    byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);
                    string json = Encoding.UTF8.GetString(inStream);

                    Message responce = JsonConvert.DeserializeObject<Message>(json);

                    serverStream.Flush();

                    return responce;

                }
                catch (Exception)
                {
                    clientSocket.Close();
                    //msg("Server stoped!!!");
                }
                return new Message();
            }
        }

        public string Connect(string[] server)
        {
            
            string resp = "";
            lock (_lock)
            {
                try
                {
                    //Check if client is connected
                    if (!clientSocket.Connected)
                    {
                        clientSocket = new TcpClient(); //Create TcpClient obj
                                                        //server = 
                        clientSocket.Connect(server[0], int.Parse(server[1]));
                        serverStream = clientSocket.GetStream();
                        resp = "Server " + server[0] + " connected!";
                    }
                    else
                    {
                        resp = "Server " + server.First() + " is stoped disconnect first to connect to another server!!!";
                    }
                }
                catch (Exception)
                {
                    resp = "Can't connect to a server!!!";
                }
            }
            return resp;
        }
    }
}
