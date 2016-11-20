using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Server
    {
        private int port = 8032; //DefaultPort
        private string name = "localhost";
        private TcpClient clientSocket = default(TcpClient);
        private TcpListener serverSocket;

        public int GetPort()
        {
            return port;
        }

        public void SetPort(int v)
        {
            port = v;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string v)
        {
            name = v;
        }

        public bool OpenSocket()
        {
            //Starts listening
            TcpListener serverSocket = new TcpListener(Dns.GetHostEntry(name).AddressList[0], port);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");

            //Open to accept clients request
            
            clientSocket = serverSocket.AcceptTcpClient();
            //Console.WriteLine(" >> Accept connection from client");

            return serverSocket.Server.IsBound;
        }

        public TcpClient GetClient()
        {
            return clientSocket;
        }
        public TcpListener GetServerSocket()
        {
            return serverSocket;
        }

    }
}
