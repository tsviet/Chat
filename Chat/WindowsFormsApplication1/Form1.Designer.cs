namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sendMessage_textBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listOfRooms = new System.Windows.Forms.ListBox();
            this.createRoom = new System.Windows.Forms.Button();
            this.createRoom_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.joinButton = new System.Windows.Forms.Button();
            this.userList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chatMainWindow = new System.Windows.Forms.ListBox();
            this.refreshUserList = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.serverDisconnect = new System.Windows.Forms.Button();
            this.serverConnect = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.serverNamePort = new System.Windows.Forms.TextBox();
            this.refreshRooms = new System.Windows.Forms.Button();
            this.leaveRoom = new System.Windows.Forms.Button();
            this.refreshMainChat = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sendMessage_textBox
            // 
            this.sendMessage_textBox.Location = new System.Drawing.Point(180, 423);
            this.sendMessage_textBox.Multiline = true;
            this.sendMessage_textBox.Name = "sendMessage_textBox";
            this.sendMessage_textBox.Size = new System.Drawing.Size(512, 32);
            this.sendMessage_textBox.TabIndex = 1;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(617, 461);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(724, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "List of rooms";
            // 
            // listOfRooms
            // 
            this.listOfRooms.FormattingEnabled = true;
            this.listOfRooms.Location = new System.Drawing.Point(727, 49);
            this.listOfRooms.Name = "listOfRooms";
            this.listOfRooms.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listOfRooms.Size = new System.Drawing.Size(190, 147);
            this.listOfRooms.TabIndex = 4;
            // 
            // createRoom
            // 
            this.createRoom.Location = new System.Drawing.Point(839, 461);
            this.createRoom.Name = "createRoom";
            this.createRoom.Size = new System.Drawing.Size(78, 23);
            this.createRoom.TabIndex = 5;
            this.createRoom.Text = "Create";
            this.createRoom.UseVisualStyleBackColor = true;
            this.createRoom.Click += new System.EventHandler(this.createRoom_Click);
            // 
            // createRoom_textBox
            // 
            this.createRoom_textBox.Location = new System.Drawing.Point(727, 423);
            this.createRoom_textBox.Name = "createRoom_textBox";
            this.createRoom_textBox.Size = new System.Drawing.Size(190, 20);
            this.createRoom_textBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(724, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Create Room";
            // 
            // joinButton
            // 
            this.joinButton.Location = new System.Drawing.Point(727, 202);
            this.joinButton.Name = "joinButton";
            this.joinButton.Size = new System.Drawing.Size(75, 23);
            this.joinButton.TabIndex = 8;
            this.joinButton.Text = "Join room";
            this.joinButton.UseVisualStyleBackColor = true;
            this.joinButton.Click += new System.EventHandler(this.joinButton_Click);
            // 
            // userList
            // 
            this.userList.FormattingEnabled = true;
            this.userList.Location = new System.Drawing.Point(15, 45);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(146, 212);
            this.userList.TabIndex = 9;
            this.userList.SelectedIndexChanged += new System.EventHandler(this.userList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "User list";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 404);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Type your message here:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Chat room";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(799, 404);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "label6";
            this.label6.Visible = false;
            // 
            // chatMainWindow
            // 
            this.chatMainWindow.FormattingEnabled = true;
            this.chatMainWindow.Location = new System.Drawing.Point(183, 45);
            this.chatMainWindow.Name = "chatMainWindow";
            this.chatMainWindow.Size = new System.Drawing.Size(509, 329);
            this.chatMainWindow.TabIndex = 14;
            // 
            // refreshUserList
            // 
            this.refreshUserList.Location = new System.Drawing.Point(86, 21);
            this.refreshUserList.Name = "refreshUserList";
            this.refreshUserList.Size = new System.Drawing.Size(75, 23);
            this.refreshUserList.TabIndex = 15;
            this.refreshUserList.Text = "Refresh";
            this.refreshUserList.UseVisualStyleBackColor = true;
            this.refreshUserList.Click += new System.EventHandler(this.refreshUserList_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label7.Location = new System.Drawing.Point(601, 404);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // serverDisconnect
            // 
            this.serverDisconnect.Location = new System.Drawing.Point(839, 303);
            this.serverDisconnect.Name = "serverDisconnect";
            this.serverDisconnect.Size = new System.Drawing.Size(75, 23);
            this.serverDisconnect.TabIndex = 17;
            this.serverDisconnect.Text = "Disconnect";
            this.serverDisconnect.UseVisualStyleBackColor = true;
            this.serverDisconnect.Click += new System.EventHandler(this.serverDisconnect_Click);
            // 
            // serverConnect
            // 
            this.serverConnect.Location = new System.Drawing.Point(727, 303);
            this.serverConnect.Name = "serverConnect";
            this.serverConnect.Size = new System.Drawing.Size(75, 23);
            this.serverConnect.TabIndex = 18;
            this.serverConnect.Text = "Connect";
            this.serverConnect.UseVisualStyleBackColor = true;
            this.serverConnect.Click += new System.EventHandler(this.serverConnect_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(724, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Enter server name and port:";
            // 
            // serverNamePort
            // 
            this.serverNamePort.Location = new System.Drawing.Point(727, 277);
            this.serverNamePort.Name = "serverNamePort";
            this.serverNamePort.Size = new System.Drawing.Size(190, 20);
            this.serverNamePort.TabIndex = 20;
            this.serverNamePort.Text = "127.0.0.1:8888";
            // 
            // refreshRooms
            // 
            this.refreshRooms.Location = new System.Drawing.Point(842, 26);
            this.refreshRooms.Name = "refreshRooms";
            this.refreshRooms.Size = new System.Drawing.Size(75, 23);
            this.refreshRooms.TabIndex = 21;
            this.refreshRooms.Text = "Refresh";
            this.refreshRooms.UseVisualStyleBackColor = true;
            this.refreshRooms.Click += new System.EventHandler(this.refreshRooms_Click);
            // 
            // leaveRoom
            // 
            this.leaveRoom.Location = new System.Drawing.Point(842, 202);
            this.leaveRoom.Name = "leaveRoom";
            this.leaveRoom.Size = new System.Drawing.Size(75, 23);
            this.leaveRoom.TabIndex = 22;
            this.leaveRoom.Text = "Leave";
            this.leaveRoom.UseVisualStyleBackColor = true;
            this.leaveRoom.Click += new System.EventHandler(this.leaveRoom_Click);
            // 
            // refreshMainChat
            // 
            this.refreshMainChat.Location = new System.Drawing.Point(617, 21);
            this.refreshMainChat.Name = "refreshMainChat";
            this.refreshMainChat.Size = new System.Drawing.Size(75, 23);
            this.refreshMainChat.TabIndex = 23;
            this.refreshMainChat.Text = "Refresh";
            this.refreshMainChat.UseVisualStyleBackColor = true;
            this.refreshMainChat.Click += new System.EventHandler(this.refreshMainChat_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(40, 423);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 24;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 403);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Choose room:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 525);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.refreshMainChat);
            this.Controls.Add(this.leaveRoom);
            this.Controls.Add(this.refreshRooms);
            this.Controls.Add(this.serverNamePort);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.serverConnect);
            this.Controls.Add(this.serverDisconnect);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.refreshUserList);
            this.Controls.Add(this.chatMainWindow);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.joinButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.createRoom_textBox);
            this.Controls.Add(this.createRoom);
            this.Controls.Add(this.listOfRooms);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.sendMessage_textBox);
            this.Name = "Form1";
            this.Text = "Socket Chat 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox sendMessage_textBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listOfRooms;
        private System.Windows.Forms.Button createRoom;
        private System.Windows.Forms.TextBox createRoom_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button joinButton;
        private System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox chatMainWindow;
        private System.Windows.Forms.Button refreshUserList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button serverDisconnect;
        private System.Windows.Forms.Button serverConnect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox serverNamePort;
        private System.Windows.Forms.Button refreshRooms;
        private System.Windows.Forms.Button leaveRoom;
        private System.Windows.Forms.Button refreshMainChat;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
    }
}

