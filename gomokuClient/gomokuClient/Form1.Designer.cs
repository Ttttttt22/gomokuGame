namespace gomokuClient
{
    partial class startForm
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
            components = new System.ComponentModel.Container();
            nameLabel = new Label();
            passwordLabel = new Label();
            nameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            signUpButton = new Button();
            nameRrrorProvider = new ErrorProvider(components);
            passwordErrorProvider = new ErrorProvider(components);
            connectionStateLabel = new Label();
            ConnectionToolTip = new ToolTip(components);
            portTextBox = new TextBox();
            IPTextBox = new TextBox();
            portLabel = new Label();
            IPLabel = new Label();
            logListBox = new ListBox();
            ((System.ComponentModel.ISupportInitialize)nameRrrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)passwordErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            nameLabel.Location = new Point(100, 169);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(70, 27);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "name:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            passwordLabel.Location = new Point(61, 225);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(109, 27);
            passwordLabel.TabIndex = 1;
            passwordLabel.Text = "password:";
            // 
            // nameTextBox
            // 
            nameTextBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            nameTextBox.Location = new Point(190, 169);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(184, 33);
            nameTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            passwordTextBox.Location = new Point(190, 225);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(184, 33);
            passwordTextBox.TabIndex = 3;
            // 
            // loginButton
            // 
            loginButton.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            loginButton.Location = new Point(93, 283);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(97, 37);
            loginButton.TabIndex = 4;
            loginButton.Text = "login";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // signUpButton
            // 
            signUpButton.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            signUpButton.Location = new Point(228, 283);
            signUpButton.Name = "signUpButton";
            signUpButton.Size = new Size(97, 37);
            signUpButton.TabIndex = 5;
            signUpButton.Text = "signUp";
            signUpButton.UseVisualStyleBackColor = true;
            signUpButton.Click += signUpButton_Click;
            // 
            // nameRrrorProvider
            // 
            nameRrrorProvider.ContainerControl = this;
            // 
            // passwordErrorProvider
            // 
            passwordErrorProvider.ContainerControl = this;
            // 
            // connectionStateLabel
            // 
            connectionStateLabel.AutoSize = true;
            connectionStateLabel.ForeColor = Color.Red;
            connectionStateLabel.Location = new Point(5, 2);
            connectionStateLabel.Name = "connectionStateLabel";
            connectionStateLabel.Size = new Size(89, 17);
            connectionStateLabel.TabIndex = 6;
            connectionStateLabel.Text = "notConnected";
            connectionStateLabel.Click += connectionStateLabel_Click;
            // 
            // portTextBox
            // 
            portTextBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            portTextBox.Location = new Point(190, 100);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(184, 33);
            portTextBox.TabIndex = 10;
            // 
            // IPTextBox
            // 
            IPTextBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            IPTextBox.Location = new Point(190, 44);
            IPTextBox.Name = "IPTextBox";
            IPTextBox.Size = new Size(184, 33);
            IPTextBox.TabIndex = 9;
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            portLabel.Location = new Point(116, 100);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(53, 27);
            portLabel.TabIndex = 8;
            portLabel.Text = "port";
            // 
            // IPLabel
            // 
            IPLabel.AutoSize = true;
            IPLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            IPLabel.Location = new Point(139, 44);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new Size(30, 27);
            IPLabel.TabIndex = 7;
            IPLabel.Text = "IP";
            // 
            // logListBox
            // 
            logListBox.FormattingEnabled = true;
            logListBox.ItemHeight = 17;
            logListBox.Location = new Point(16, 42);
            logListBox.Name = "logListBox";
            logListBox.Size = new Size(77, 123);
            logListBox.TabIndex = 11;
            // 
            // startForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 355);
            Controls.Add(logListBox);
            Controls.Add(portTextBox);
            Controls.Add(IPTextBox);
            Controls.Add(portLabel);
            Controls.Add(IPLabel);
            Controls.Add(connectionStateLabel);
            Controls.Add(signUpButton);
            Controls.Add(loginButton);
            Controls.Add(passwordTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(nameLabel);
            Name = "startForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "gomokuClient";
            FormClosing += startForm_FormClosing;
            Load += startForm_Load;
            ((System.ComponentModel.ISupportInitialize)nameRrrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)passwordErrorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label nameLabel;
        private Label passwordLabel;
        private TextBox nameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button signUpButton;
        private ErrorProvider nameRrrorProvider;
        private ErrorProvider passwordErrorProvider;
        private Label connectionStateLabel;
        private ToolTip ConnectionToolTip;
        private TextBox portTextBox;
        private TextBox IPTextBox;
        private Label portLabel;
        private Label IPLabel;
        private ListBox logListBox;
    }
}