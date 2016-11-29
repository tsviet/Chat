using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static WindowsFormsApplication1.Form1;

namespace Client
{
    public class Message
    {
        public Command command {get; set;}
        public List<string> message { get; set;}
        public string other { get; set; }
    }
}
