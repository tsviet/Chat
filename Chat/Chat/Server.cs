using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public static class Server
    {
        private static Dictionary<string, ChatRoom> chatrooms = new Dictionary<string, ChatRoom>();
        private static string activeChatRoom = "";


        //Create room in a dictionary
        internal static void AddRoom(ChatRoom chatroom)
        {
            chatrooms.Add(chatroom.GetName(), chatroom);
            activeChatRoom = chatroom.GetName();
        }

        //Check if room exist
        internal static bool HasRoom(string name)
        {
            bool isHere = false;
            if(chatrooms.Count > 0) { isHere = chatrooms.ContainsKey(name); }
            return isHere;
        }

        //Delete room
        internal static void RemoveRoom(string id)
        {
            chatrooms.Remove(id);
        }

        //Connect user to a specific chatroom
        internal static void ConnectUser(string chatroom, string user)
        {
            chatrooms[chatroom].AddUser(user);
        }

        //Add message to a room
        internal static void AddMessage(string user, string param)
        {
            chatrooms[activeChatRoom].AddMessage(user + " says: " +param);
        }

        //Get list of messages
        internal static ObservableCollection<string> GetMessageList()
        {
            return chatrooms[activeChatRoom].GetMesages();
        }

        //Get list of messages
        internal static ObservableCollection<string> GetUserList()
        {
            return chatrooms[activeChatRoom].GetUserList();
        }

        //Getter for active chat room
        internal static string GetActiveRoom() {
            return activeChatRoom;
        }

        //Getter for active chat room
        internal static ChatRoom GetCurrentChatRoom()
        {
            return chatrooms[activeChatRoom];
        }
    }
}
