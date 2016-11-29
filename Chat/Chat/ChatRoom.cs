using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class ChatRoom
    {
        
        private ObservableCollection<string> userList;
        private ObservableCollection<string> messageList;
        public Dictionary<string, string> FileList { get; set; }
        private string name = "";

        //Create object
        public ChatRoom(string name)
        {
            userList = new ObservableCollection<string>();
            messageList = new ObservableCollection<string>();
            FileList = new Dictionary<string, string>();
            this.name = name;
        }

        //Get user list array
        public ObservableCollection<string> GetUserList()
        {
            return userList;
        }

        //Add message to chat room
        public void AddMessage(string message)
        {
            messageList.Add(message);
        }

        //Get message list
        public ObservableCollection<string> GetMesages()
        {
            return messageList;
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

        //Add user to a list
        internal void AddUser(string user)
        {
            userList.Add(user);
        }
    }
}
