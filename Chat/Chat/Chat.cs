using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat
{
    public class Chat
    {
        NetworkStream networkStream;
        private TcpClient clientSocket;
        private int clineNo;
        private static string userName = "";
        private Object _lock = new Object();

        public enum Command
        {
            NotConnected,
            Error, //Error send by a server with error code
            OK, //Server done operation successfully
            SendMessage, //User sending message using button send
            Create, //Create chat room 
            RefreshCurrentRoom, //Get list of messages from a server
            UpdateDropDown, //Get list of rooms current user connected
            ListUsers, //Get user list connected to a current room
            SetUser, //Set username to server
            DisconnectClient, //Client request to be disconnected from a server
            RefreshRooms, //Client request list of rooms existing on a server
            JoinRoom, //Client wants to join a room
            LeaveRoom //Client wants to leave a room
        };

        public Chat(TcpClient clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        public Chat(TcpClient clientSocket, int clineNo) : this(clientSocket)
        {
            this.clineNo = clineNo;
        }

        public void SendResponce(Message message)
        {
            lock (_lock)
            {
                //string serverResponse = message;
                string serverResponse = JsonConvert.SerializeObject(message, Formatting.Indented);
                byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
        }

        //With message
        private void IPC(Command command, List<string> message)
        {
            //Create message object and setup values
            Message request = new Message();
            request.command = command;
            request.message = message;

            //Send message and seve on a server and recieve message from a server
            SendResponce(request);
        }

        //Without message
        private void IPC(Command command)
        {
            //Create message object and setup values
            Message request = new Message();
            request.command = command;

            //Send message and seve on a server and recieve message from a server
            SendResponce(request);
        }

        public void doChat()
        {
            byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
            string dataFromClient = null;

            while ((true))
            {
                try
                {
                    lock (_lock)
                    {

                        networkStream = clientSocket.GetStream();
                        networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                        dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("}") + 1);

                        Message responce = JsonConvert.DeserializeObject<Message>(dataFromClient);

                        if (responce == null) continue;
                        switch (responce.command)
                        {

                            case Command.SendMessage:
                                SendMessage(responce);
                                break;
                            case Command.Create:
                                Create(responce);
                                break;
                            case Command.RefreshCurrentRoom:
                                RefreshCurrentRoom(responce);
                                break;
                            case Command.UpdateDropDown:
                                UpdateDropDown(responce);
                                break;
                            case Command.ListUsers:
                                ListUsers(responce);
                                break;
                            case Command.SetUser:
                                SetUser(responce);
                                break;
                            case Command.DisconnectClient:
                                DisconnectClient(responce);
                                break;
                            case Command.RefreshRooms:
                                RefreshRooms(responce);
                                break;
                            case Command.JoinRoom:
                                JoinRoom(responce);
                                break;
                            case Command.LeaveRoom:
                                LeaveRoom(responce);
                                break;
                            default:
                                RequestEmpty();
                                break;

                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                    clientSocket.Close();
                    Console.WriteLine("Client " + clineNo + " disconnected!!");
                    break;
                }
            }//while

            Thread.CurrentThread.Abort();
        }

        private void RequestEmpty()
        {
            IPC(Command.Error);
        }

        private void LeaveRoom(Message responce)
        {
            if (Server.ServerExist())
            {
                if (Server.GetUserListInRoom(responce.other).Contains(responce.message[0]))
                {
                    Server.GetUserListInRoom(responce.other).Remove(responce.message[0]);
                    IPC(Command.OK);
                }
                else
                {
                    IPC(Command.Error);
                }
            }
            else
            {
                IPC(Command.Error);
            }
        }

        private void JoinRoom(Message responce)
        {
            if (Server.ServerExist())
            {
                if (Server.GetUserListInRoom(responce.other).Contains(responce.message[0]))
                {
                    IPC(Command.Error);
                }
                else
                {
                    Server.GetUserListInRoom(responce.other).Add(responce.message[0]);
                    IPC(Command.OK);
                }
            }
            else
            {
                IPC(Command.Error);
            }
        }

        private void RefreshRooms(Message responce)
        {
            if (Server.ServerExist())
            {
                List<string> res = new List<string>();
                foreach (var m in Server.GetChatRoomList())
                {
                    res.Add(m.Value.GetName());
                }
                IPC(Command.OK, res);
            }
            else
            {
                IPC(Command.Error);
            }
        }

        private void SetUser(Message responce)
        {
            //if (responce.message.Count == 0) return;
            userName = responce.message[0];
            IPC(Command.OK);
        }

        private void DisconnectClient(Message responce)
        {
            clientSocket.Close();
            Console.WriteLine("Client " + clineNo + " disconnected!!");
        }

        private void ListUsers(Message responce)
        {
            if (Server.ServerExist())
            {
                List<string> res = new List<string>();

                foreach (var m in Server.GetCurrentChatRoom().GetUserList())
                {
                    res.Add(m);
                }
                IPC(Command.OK, res);
            }
            else
            {
                IPC(Command.Error);
            }
        }

        private void UpdateDropDown(Message responce)
        {
            if (Server.ServerExist())
            {
                List<string> res = new List<string>();
                foreach (var room in Server.GetChatRoomList().Values)
                {
                    if (room.GetUserList().Contains(responce.message[0]))
                    {
                        res.Add(room.GetName());
                    }

                }
                IPC(Command.OK, res);
            }
            else
            {
                IPC(Command.Error);
            }
        }

        private void RefreshCurrentRoom(Message responce)
        {
            if (Server.ServerExist())
            {
                List<string> res = new List<string>();

                foreach (var m in Server.GetMessageList())
                {
                    res.Add(m);
                }
                IPC(Command.OK, res);
            }
            else
            {
                IPC(Command.Error);
            }
        }

        private void Create(Message responce)
        {
            if (!Server.HasRoom(responce.message[0]))
            {
                ChatRoom chatroom = new ChatRoom(responce.message[0]);
                chatroom.AddUser(userName);
                Server.AddRoom(chatroom);
                IPC(Command.OK);
            }
            //Send error on existing chat room
            else
            {
                IPC(Command.Error);
            }
        }

        private void SendMessage(Message responce)
        {  
            Server.GetMessageList(responce.other).Add(userName + " says: " + responce.message[0]);
            IPC(Command.OK);
        }
    }
}
