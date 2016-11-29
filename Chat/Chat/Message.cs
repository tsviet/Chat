using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class Message
    {
        public Command command { get; set; }
        public List<string> message { get; set; }
        public string other { get; set; }
    }
}
