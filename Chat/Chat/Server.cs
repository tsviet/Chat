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
        private Dictionary<string, ChatRoom> chatrooms;

        public Server()
        {
            chatrooms = new Dictionary<string, ChatRoom>();
        }

        internal void AddRoom(ChatRoom chatroom)
        {
            chatrooms.Add(chatroom.GetName(), chatroom);
        }

        internal bool HasRoom(string name)
        {
            return chatrooms.ContainsKey(name);
        }

        internal void RemoveRoom(string id)
        {
            chatrooms.Remove(id);
        }

        //Connect user to a specific chatroom
        internal void ConnectUser(string chatroom, string user)
        {
            foreach (var value in chatrooms.Values.Distinct())
            {
                if(value.GetName() == chatroom)
                {
                    value.AddUser(user);
                }
            }
        }
    }
}
