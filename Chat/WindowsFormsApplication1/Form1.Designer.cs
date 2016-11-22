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
            this.SuspendLayout();
            // 
            // sendMessage_textBox
            // 
            this.sendMessage_textBox.Location = new System.Drawing.Point(180, 423);
            this.sendMessage_textBox.Multiline = true;
            this.sendMessage_textBox.Name = "sendMessage_textBox";
            this.sendMessage_textBox.Size = new System.Drawing.Size(525, 32);
            this.sendMessage_textBox.TabIndex = 1;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(630, 461);
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
            this.label1.Location = new System.Drawing.Point(724, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "List of rooms";
            // 
            // listOfRooms
            // 
            this.listOfRooms.FormattingEnabled = true;
            this.listOfRooms.Location = new System.Drawing.Point(727, 62);
            this.listOfRooms.Name = "listOfRooms";
            this.listOfRooms.Size = new System.Drawing.Size(190, 134);
            this.listOfRooms.TabIndex = 4;
            // 
            // createRoom
            // 
            this.createRoom.Location = new System.Drawing.Point(842, 349);
            this.createRoom.Name = "createRoom";
            this.createRoom.Size = new System.Drawing.Size(78, 23);
            this.createRoom.TabIndex = 5;
            this.createRoom.Text = "Create";
            this.createRoom.UseVisualStyleBackColor = true;
            this.createRoom.Click += new System.EventHandler(this.createRoom_Click);
            // 
            // createRoom_textBox
            // 
            this.createRoom_textBox.Location = new System.Drawing.Point(730, 311);
            this.createRoom_textBox.Name = "createRoom_textBox";
            this.createRoom_textBox.Size = new System.Drawing.Size(190, 20);
            this.createRoom_textBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(727, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Create Room";
            // 
            // joinButton
            // 
            this.joinButton.Location = new System.Drawing.Point(842, 212);
            this.joinButton.Name = "joinButton";
            this.joinButton.Size = new System.Drawing.Size(75, 23);
            this.joinButton.TabIndex = 8;
            this.joinButton.Text = "Join room";
            this.joinButton.UseVisualStyleBackColor = true;
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
            this.label4.Location = new System.Drawing.Point(180, 404);
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
            this.label6.Location = new System.Drawing.Point(803, 295);
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
            this.refreshUserList.Location = new System.Drawing.Point(86, 274);
            this.refreshUserList.Name = "refreshUserList";
            this.refreshUserList.Size = new System.Drawing.Size(75, 23);
            this.refreshUserList.TabIndex = 15;
            this.refreshUserList.Text = "Refresh";
            this.refreshUserList.UseVisualStyleBackColor = true;
            this.refreshUserList.Click += new System.EventHandler(this.refreshUserList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 525);
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
    }
}

