using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient();
        private string userName = "";

        public Form1(string name)
        {
            InitializeComponent();
            userName = name;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect("127.0.0.1", 8888);

            //Todo: Update roomlist and userlist from the server 

            RPC("SetUser~", userName);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(sendMessage_textBox.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
            serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);
            string returndata = Encoding.UTF8.GetString(inStream);
            msg(returndata);
            sendMessage_textBox.Text = "";
            sendMessage_textBox.Focus();
            serverStream.Flush();
        }

        public void msg(string mesg)
        {
            chatMainWindow_textBox.Text = chatMainWindow_textBox.Text + Environment.NewLine + " >> " + mesg;
        }

        private void userList_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //Create room button click
        private void createRoom_Click(object sender, EventArgs e)
        {
            //Send create message to a server
            RPC("create", createRoom_textBox.Text);

        }

        private bool RPC(string command, string name)
        {
            if (string.IsNullOrWhiteSpace(createRoom_textBox.Text)) return false; 
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(command + "~" + name + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
            serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);
            string returndata = Encoding.UTF8.GetString(inStream);

            msg(returndata);

            if (command.Contains("create") && returndata.Contains("Error"))
            {
                label6.Visible = true;
                label6.Text = returndata;
                serverStream.Flush();
            } else if (returndata.Contains("Created"))
            {
                listOfRooms.Items.Add(name);
                //Update user list on roon create
                userList.Items.Add(userName);
                msg("Chat room : " + name + " were created!");
                serverStream.Flush();
            }

            return string.IsNullOrEmpty(returndata);
        }
    }
}
