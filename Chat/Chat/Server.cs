﻿using System;
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

        //Add file
        internal static void AddFile(string chatroom, string filename, string file)
        {
            chatrooms[chatroom].FileList.Add(filename, file);
        }

        //Get file
        internal static string GetFile(string chatroom, string filename)
        {
            return chatrooms[chatroom].FileList[filename];
        }

        //Create room in a dictionary
        internal static void AddRoom(ChatRoom chatroom)
        {
            chatrooms.Add(chatroom.GetName(), chatroom);
           // roomList.Add(chatroom);
            activeChatRoom = chatroom.GetName();
        }

        //Set current chat room
        internal static void SetActiveChatRoom(string name)
        {
            activeChatRoom = name;
        }

        //Check if room exist
        internal static bool HasRoom(string name)
        {
            bool isHere = false;
            if(chatrooms.Count > 0) { isHere = chatrooms.ContainsKey(name); }
            return isHere;
        }

        //Check if server exist
        internal static bool ServerExist()
        {
            bool isHere = false;
            if (chatrooms.Count > 0) { isHere = true; }
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
        internal static ObservableCollection<string> GetMessageList(string Room)
        {
            return chatrooms[Room].GetMesages();
        }

        //Get list of messages
        internal static ObservableCollection<string> GetUserListInRoom(string Room)
        {
            return chatrooms[Room].GetUserList();
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

        //Getter for room list
        internal static Dictionary<string, ChatRoom> GetChatRoomList()
        {
            return chatrooms;
        }
    }
}
