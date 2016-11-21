using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class ChatRoom
    {
        
        private List<string> userList;
        private string name = "";

        //Create object
        public ChatRoom(string name)
        {
            userList = new List<string>();
            this.name = name;
        }

        //Get user list array
        public List<string> GetUserList()
        {
            return userList;
        }

        //Add user to chat room
        public void AddUser(string user)
        {
            userList.Add(user);
        }

        //Get name of chatroom
        public string GetName()
        {
            return name;
        }

        //Remove user from chat room
        public void RemoveUser(string name)
        {
            userList.Remove(name);
        }

    }
}
