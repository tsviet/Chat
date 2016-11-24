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
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = null;
        private string userName = "";
        private string roomName = "";
        private string[] server;

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

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                //Send message and seve on a server
                RPC("sendMessage", comboBox1.SelectedItem.ToString() + ";" + sendMessage_textBox.Text);
                sendMessage_textBox.Text = "";
                sendMessage_textBox.Focus();
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

        private void userList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        //Create room button click
        private void createRoom_Click(object sender, EventArgs e)
        {
            //Send create message to a server
            //chatMainWindow.Items.Add("User name : " + userName);
            if (string.IsNullOrWhiteSpace(createRoom_textBox.Text)) return;
            RPC("create", createRoom_textBox.Text);
        }

        public string RPC(string command, string name)
        {
            string ret = "";
            try
            {
                //Made this code save for threads
                byte[] outStream = Encoding.ASCII.GetBytes(command + "~" + name + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
                serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);
                string returndata = Encoding.UTF8.GetString(inStream);

                if (command.Contains("create") && returndata.Contains("Error"))
                {
                    label6.Visible = true;
                    label6.Text = returndata;
                    serverStream.Flush();

                    Thread tread = new Thread(turnOfErrorSent);
                    tread.Start();

                }
                else if (returndata.Contains("Created"))
                {
                    //Update list of rooms
                    //listOfRooms.Items.Add(name);
                    //Update user list on roon create
                    //userList.Items.Add(userName);
                    //Update current roomName with new chatroom
                    roomName = name;
                    //Post message to a log window
                    msg("Chat room : " + name + " were created!");
                    serverStream.Flush();

                }
                else if (returndata.Contains("RefreshCurrentRoom~"))
                {
                    string mess = returndata.Replace("RefreshCurrentRoom~", "");
                    string[] list = mess.Split(';');
                    chatMainWindow.Items.Clear();
                    foreach (var m in list)
                    {
                        chatMainWindow.Items.Add(m);
                    }
                    serverStream.Flush();
                }
                //UpdateDropDown~
                else if (returndata.Contains("UpdateDropDown~"))
                {
                    string mess = returndata.Replace("UpdateDropDown~", "");
                    string[] list = mess.Split(';');
                    comboBox1.Items.Clear();
                    foreach (var m in list)
                    {
                        comboBox1.Items.Add(m);
                    }
                    serverStream.Flush();
                }
                else if (returndata.Contains("ListUsers~"))
                {
                    string mess = returndata.Replace("ListUsers~", "");
                    string[] list = mess.Split(';');
                    userList.Items.Clear();
                    foreach (var m in list)
                    {
                        userList.Items.Add(m);
                    }
                    serverStream.Flush();
                }
                //ChatRoomsNames~
                else if (returndata.Contains("ChatRoomsNames~"))
                {
                    string mess = returndata.Replace("ChatRoomsNames~", "");
                    string[] list = mess.Split(';');
                    listOfRooms.Items.Clear();
                    foreach (var m in list)
                    {
                        listOfRooms.Items.Add(m);
                    }
                    serverStream.Flush();
                }
                else if (returndata.Contains("202.OK"))
                {
                    //Message Sent
                    label7.Visible = true;
                    label7.Text = "Message sent!";

                    Thread tread = new Thread(turnOfMessageSent);
                    tread.Start();
                }
                else
                {
                    msg(returndata);
                    ret = returndata;
                    serverStream.Flush();
                }
            }catch(Exception)
            {
                clientSocket.Close();
                msg("Server stoped!!!");
            }
            return ret;
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
            //Check if client is connected
            if (clientSocket.Connected)
            {
                RPC("ListUsers", userName);
            }
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

        private void serverConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if client is connected
                if (!clientSocket.Connected)
                {
                    clientSocket = new TcpClient(); //Create TcpClient obj
                    server = serverNamePort.Text.Split(':');
                    clientSocket.Connect(server[0], int.Parse(server[1]));
                    serverStream = clientSocket.GetStream();
                    msg("Server " + serverNamePort.Text + " started!");
                    SetUser();
                    while (!RPC("SetUser", userName).Contains("OK"))
                    {
                        SetUser();
                    }

                } else
                {
                    msg("Server " + server.First() + " is connected disconnect first to connect to another server!!!");
                }
            }
            catch (Exception)
            {
                msg("Can't connect to a server!!!");
            }
        }

        private void serverDisconnect_Click(object sender, EventArgs e)
        {
            //Check if client is connected
            if (clientSocket != null && clientSocket.Connected)
            {
                RPC("DisconnectClient", "");
                clientSocket.Close();
                msg("Server " + server.First() + " stoped!");
            }
        }

        private void refreshRooms_Click(object sender, EventArgs e)
        {
            //Check if client is connected
            if (clientSocket.Connected)
            {
                RPC("RefreshRooms", "");
            }
        }

        private void joinButton_Click(object sender, EventArgs e)
        {
            //Check if client is connected
            if (clientSocket.Connected)
            {
                try
                {
                    foreach (var item in listOfRooms.SelectedItems) {
                        RPC("JoinRoom", userName + ";" + item.ToString());
                    }
                }
                catch (Exception)
                {
                    msg("Choose room first!");
                }
            }
        }

        private void leaveRoom_Click(object sender, EventArgs e)
        {
            //Check if client is connected
            if (clientSocket.Connected)
            {
                try
                {
                    //RPC("LeaveRoom", userName + ";" + listOfRooms.SelectedItem.ToString());
                    foreach (var item in listOfRooms.SelectedItems)
                    {
                        RPC("LeaveRoom", userName + ";" + item.ToString());
                    }
                }
                catch (Exception)
                {
                    msg("Choose room first!");
                }
            }
        }

        private void refreshMainChat_Click(object sender, EventArgs e)
        {
            if (clientSocket.Connected)
            {
                RPC("RefreshCurrentRoom", "");
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            if (clientSocket.Connected)
            {
                RPC("UpdateDropDown", userName);
            }
        }
    }
}
