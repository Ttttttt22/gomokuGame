namespace gomokuServer
{
    partial class serverForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            userListBox = new ListBox();
            serverPortTextBox = new TextBox();
            serverIPTextBox = new TextBox();
            portLabel = new Label();
            IPLabel = new Label();
            serverStartButton = new Button();
            logListBox = new ListBox();
            userLabel = new Label();
            logLabel = new Label();
            countLabel = new Label();
            SuspendLayout();
            // 
            // userListBox
            // 
            userListBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            userListBox.FormattingEnabled = true;
            userListBox.ItemHeight = 27;
            userListBox.Location = new Point(36, 42);
            userListBox.Name = "userListBox";
            userListBox.Size = new Size(176, 301);
            userListBox.TabIndex = 0;
            // 
            // serverPortTextBox
            // 
            serverPortTextBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            serverPortTextBox.Location = new Point(339, 98);
            serverPortTextBox.Name = "serverPortTextBox";
            serverPortTextBox.Size = new Size(184, 33);
            serverPortTextBox.TabIndex = 9;
            serverPortTextBox.Text = "2019";
            // 
            // serverIPTextBox
            // 
            serverIPTextBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            serverIPTextBox.Location = new Point(339, 42);
            serverIPTextBox.Name = "serverIPTextBox";
            serverIPTextBox.Size = new Size(184, 33);
            serverIPTextBox.TabIndex = 8;
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            portLabel.Location = new Point(265, 98);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(53, 27);
            portLabel.TabIndex = 7;
            portLabel.Text = "port";
            // 
            // IPLabel
            // 
            IPLabel.AutoSize = true;
            IPLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            IPLabel.Location = new Point(288, 42);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new Size(30, 27);
            IPLabel.TabIndex = 6;
            IPLabel.Text = "IP";
            // 
            // serverStartButton
            // 
            serverStartButton.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            serverStartButton.Location = new Point(373, 152);
            serverStartButton.Name = "serverStartButton";
            serverStartButton.Size = new Size(104, 40);
            serverStartButton.TabIndex = 10;
            serverStartButton.Text = "start";
            serverStartButton.UseVisualStyleBackColor = true;
            serverStartButton.Click += serverStartButton_Click;
            // 
            // logListBox
            // 
            logListBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            logListBox.FormattingEnabled = true;
            logListBox.ItemHeight = 17;
            logListBox.Location = new Point(244, 226);
            logListBox.Name = "logListBox";
            logListBox.Size = new Size(417, 106);
            logListBox.TabIndex = 11;
            // 
            // userLabel
            // 
            userLabel.AutoSize = true;
            userLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            userLabel.Location = new Point(78, 12);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(83, 27);
            userLabel.TabIndex = 12;
            userLabel.Text = "userList";
            // 
            // logLabel
            // 
            logLabel.AutoSize = true;
            logLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            logLabel.Location = new Point(244, 196);
            logLabel.Name = "logLabel";
            logLabel.Size = new Size(43, 27);
            logLabel.TabIndex = 13;
            logLabel.Text = "log";
            // 
            // countLabel
            // 
            countLabel.AutoSize = true;
            countLabel.Location = new Point(94, 346);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(43, 17);
            countLabel.TabIndex = 14;
            countLabel.Text = "label1";
            // 
            // serverForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(673, 377);
            Controls.Add(countLabel);
            Controls.Add(logLabel);
            Controls.Add(userLabel);
            Controls.Add(logListBox);
            Controls.Add(serverStartButton);
            Controls.Add(serverPortTextBox);
            Controls.Add(serverIPTextBox);
            Controls.Add(portLabel);
            Controls.Add(IPLabel);
            Controls.Add(userListBox);
            Name = "serverForm";
            Text = "gomokuServerForm";
            Load += serverForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox userListBox;
        private TextBox serverPortTextBox;
        private TextBox serverIPTextBox;
        private Label portLabel;
        private Label IPLabel;
        private Button serverStartButton;
        private ListBox logListBox;
        private Label userLabel;
        private Label logLabel;
        private Label countLabel;
    }
}