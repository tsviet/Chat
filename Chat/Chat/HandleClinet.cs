using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat
{
    //Class to handle each client request separatly
    public class HandleClinet
    {
        TcpClient clientSocket;
        int clNo;

        public void startClient(TcpClient inClientSocket, ref int clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(new Chat(clientSocket, clineNo).doChat);
            ctThread.Start();
        }
    }
}

