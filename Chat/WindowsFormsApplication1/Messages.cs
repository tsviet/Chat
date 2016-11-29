using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using WindowsFormsApplication1;
using static WindowsFormsApplication1.Form1;

namespace Client
{
    public class Messages
    {
        private TcpClient clientSocket = new TcpClient();
        private NetworkStream serverStream = null;
        //private string roomName = "";
        //private string[] server;
        

        public Message RPC(Message message)
        {
            try
            {
                string json_out = JsonConvert.SerializeObject(message, Formatting.Indented);
                //Made this code save for threads
                byte[] outStream = Encoding.ASCII.GetBytes(json_out);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
                serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);
                string json = Encoding.UTF8.GetString(inStream);

                Message responce = JsonConvert.DeserializeObject<Message>(json);

                serverStream.Flush();

                return responce;

                /*else if (returndata.Contains("RefreshCurrentRoom~"))
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
                }*/
            }
            catch (Exception)
            {
                clientSocket.Close();
                //msg("Server stoped!!!");
            }
            return new Message();
        }

        public string Connect(string[] server)
        {
            string resp = "";
            try
            {
                //Check if client is connected
                if (!clientSocket.Connected)
                {
                    clientSocket = new TcpClient(); //Create TcpClient obj
                    //server = 
                    clientSocket.Connect(server[0], int.Parse(server[1]));
                    serverStream = clientSocket.GetStream();
                    resp = "Server " + server[0] + " connected!";
                }
                else
                {
                    resp = "Server " + server.First() + " is connected disconnect first to connect to another server!!!";
                }
            }
            catch (Exception)
            {
                resp = "Can't connect to a server!!!";
            }
            return resp;
        }
    }
}
