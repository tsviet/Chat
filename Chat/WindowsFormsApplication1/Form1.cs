using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient();
        private string userName = "";
        private string roomName = "";

        public Form1()
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
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect("127.0.0.1", 8888);

            //Todo: Update roomlist and userlist from the server 
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            //Send message and seve on a server
            RPC("sendMessage", sendMessage_textBox.Text);
            sendMessage_textBox.Text = "";
            sendMessage_textBox.Focus();
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
            chatMainWindow.Items.Add("User name : " + userName);
            RPC("SetUser", userName);
            RPC("create", createRoom_textBox.Text);

        }

        private void RPC(string command, string name)
        {
            NetworkStream serverStream = clientSocket.GetStream();
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

            } else if (returndata.Contains("Created"))
            {
                //Update list of rooms
                listOfRooms.Items.Add(name);
                //Update user list on roon create
                //userList.Items.Add(userName);
                //Update current roomName with new chatroom
                roomName = name;
                //Post message to a log window
                msg("Chat room : " + name + " were created!");
                serverStream.Flush();
            } else if (returndata.Contains("Messages~"))
            {
                string mess = returndata.Replace("Messages~", "");
                string[] list = mess.Split(';');
                chatMainWindow.Items.Clear();
                foreach (var m in list)
                {
                    chatMainWindow.Items.Add(m);
                }
            }
            else if (returndata.Contains("Users~"))
            {
                string mess = returndata.Replace("Users~", "");
                string[] list = mess.Split(';');
                userList.Items.Clear();
                foreach (var m in list)
                {
                    userList.Items.Add(m);
                }
            }
            else
            {
                msg(returndata);
            }

            //return string.IsNullOrEmpty(returndata);
        }
    }
}
