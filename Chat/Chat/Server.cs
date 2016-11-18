using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Server
    {
        private int port = 8032; //DefaultPort

        public int GetPort()
        {
            return port;
        }

        public void SetPort(int v)
        {
            port = v;
        }
    }
}
