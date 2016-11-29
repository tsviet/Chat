using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Messages messages = new Messages();
        private string userName = "";
        private string roomName;

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

        public Form1()
        {
            InitializeComponent();
        }

        /*private void UpdateChatWindow()
        {
            while (true)
            {
                RPC("GetAllMessages", roomName);
                Thread.Yield();
                Thread.Sleep(1000);
            }
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");

            
            //Thread tread = new Thread(UpdateChatWindow);
            //tread.Start();
            //Todo: Update roomlist and userlist from the server 
        }
        private Client.Message IPC(Command command, string message)
        {
            //Create message object and setup values
            Client.Message request = new Client.Message();
            request.command = command;
            request.message = new List<string>() { message };

            //Send message and seve on a server and recieve message from a server
            return messages.RPC(request);
        }

        private Client.Message IPC(Command command, string message, string other)
        {
            //Create message object and setup values
            Client.Message request = new Client.Message();
            request.command = command;
            request.message = new List<string>() { message };
            request.other = other;

            //Send message and seve on a server and recieve message from a server
            return messages.RPC(request);
        }

        //Run form and set user
        private void SetUser()
        {
            using (var form = new Form2())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string val = form.ReturnValue1;            //values preserved after close
                    userName = val;
                }
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0 && comboBox1.SelectedIndex >= 0)
            {
                Client.Message responce = IPC(Command.SendMessage, userName + " says: " + sendMessage_textBox.Text, comboBox1.SelectedItem.ToString());

                sendMessage_textBox.Text = "";
                sendMessage_textBox.Focus();

                if (responce != null && responce.command.HasFlag(Command.OK))
                {
                    //Message Sent
                    label7.Visible = true;
                    label7.Text = "Message sent!";

                    Thread tread = new Thread(turnOfMessageSent);
                    tread.Start();
                }
            }
            else
            {
                msg("Choose room before sending messages!");
            }
        }

        public void msg(string mesg)
        {
            chatMainWindow.Items.Add(">> " + mesg);
        }

        //Create room button click
        private void createRoom_Click(object sender, EventArgs e)
        {
            //Send create message to a server
            //chatMainWindow.Items.Add("User name : " + userName);
            if (string.IsNullOrWhiteSpace(createRoom_textBox.Text)) return;
            
            Client.Message responce = IPC(Command.Create, createRoom_textBox.Text);

            if(responce == null || responce.command == Command.NotConnected) { msg("Connect to server first!"); return; }
            if (responce != null && responce.command == Command.Error)
            {
                label6.Visible = true;
                label6.Text = "Room already exist!";

                Thread tread = new Thread(turnOfErrorSent);
                tread.Start();

            }
            else if (responce != null && responce.command.HasFlag(Command.OK))
            {
                //Update current roomName with new chatroom
                roomName = createRoom_textBox.Text;
                //Post message to a log window
                msg("Chat room : " + roomName + " were created!");

            }
        }

        
        private void turnOfMessageSent()
        {
            Thread.Sleep(1000); //Wait 1 sec
            //Call method on different thread lambda function with no argument
            label7.Invoke((MethodInvoker)(() => label7.Visible = false));
        }

        private void turnOfErrorSent()
        {
            Thread.Sleep(1000); //Wait 1 sec
            //Call method on different thread lambda function with no argument
            label7.Invoke((MethodInvoker)(() => label6.Visible = false));
        }


        private void refreshUserList_Click(object sender, EventArgs e)
        {

            Client.Message responce = IPC(Command.ListUsers, userName);

            if (responce != null && responce.command.HasFlag(Command.OK))
            {
                List<string> mess = responce.message;
                userList.Items.Clear();
                foreach (var m in mess)
                {
                    userList.Items.Add(m);
                }

            }
            else
            {
                msg("Can't list users!");
            }
        }

        private void serverConnect_Click(object sender, EventArgs e)
        {
            
            string resp = messages.Connect(serverNamePort.Text.Split(':'));
            Client.Message responce = new Client.Message();
            msg(resp);
            if (resp != null && resp.Contains("connected"))
            {
                do
                {
                    SetUser();
                    responce = IPC(Command.SetUser, userName);

                } while (!responce.command.HasFlag(Command.OK));

            }
        }

        private void serverDisconnect_Click(object sender, EventArgs e)
        {
            Client.Message responce = IPC(Command.DisconnectClient, userName);
            if (responce != null && responce.command.HasFlag(Command.OK))
            {
                msg("You were disconected from server !");
            }
            else
            {
                msg("Connect to a server first!");
            }
        }

        private void refreshRooms_Click(object sender, EventArgs e)
        {
            Client.Message responce = IPC(Command.RefreshRooms, userName);
            if (responce != null && responce.command.HasFlag(Command.OK))
            {
                listOfRooms.Items.Clear();
                foreach (var m in responce.message)
                {
                    listOfRooms.Items.Add(m);
                }
            }
            else
            {
                msg("No rooms found!");
            }
        }

        private void joinButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in listOfRooms.SelectedItems)
                {
                    Client.Message responce = IPC(Command.JoinRoom, userName, item.ToString());
                    if (responce != null && responce.command.HasFlag(Command.Error))
                    {
                        msg("You are already in the room " + item.ToString());
                    }
                    else
                    {
                        msg("User " + userName + " joined room " + item.ToString());
                    }

                }
            }
            catch (Exception)
            {
                msg("Error: Choose room first!");
            }
        }

        private void leaveRoom_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in listOfRooms.SelectedItems)
                {
                    Client.Message responce = IPC(Command.LeaveRoom, userName, item.ToString());
                    if (responce != null && responce.command.HasFlag(Command.OK))
                    {
                        msg("User " + userName + " leaved room " + item.ToString());
                    }
                    else
                    {
                        msg("Error: unnable to leave selected room!");
                    }
                }
            }
            catch (Exception)
            {
                msg("Choose room first!");
            }
        }

        private void refreshMainChat_Click(object sender, EventArgs e)
        {
            Client.Message responce = IPC(Command.RefreshCurrentRoom, userName);
            if (responce != null && responce.command.HasFlag(Command.OK))
            {
                chatMainWindow.Items.Clear();
                foreach (var m in responce.message)
                {
                    chatMainWindow.Items.Add(m);
                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            Client.Message responce = IPC(Command.UpdateDropDown, userName);
            if (responce != null && responce.command.HasFlag(Command.OK))
            {
                comboBox1.Items.Clear();
                foreach (var m in responce.message)
                {
                    comboBox1.Items.Add(m);
                }
            }
        }

       /* private void privateMessage_Click(object sender, EventArgs e)
        {
            if (clientSocket.Connected && !string.IsNullOrEmpty(userList.SelectedItem.ToString()))
            {
                sendMessage_textBox.Text = "@" + userList.SelectedItem.ToString() + " ";
            }
        }*/
    }
}
