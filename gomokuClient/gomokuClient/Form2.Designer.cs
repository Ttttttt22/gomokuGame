namespace gomokuClient
{
    partial class opponentSelectionForm
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
            opponentListBox = new ListBox();
            label = new Label();
            askGameButton = new Button();
            countLabel = new Label();
            logListBox = new ListBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // opponentListBox
            // 
            opponentListBox.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            opponentListBox.FormattingEnabled = true;
            opponentListBox.ItemHeight = 27;
            opponentListBox.Location = new Point(34, 53);
            opponentListBox.Name = "opponentListBox";
            opponentListBox.Size = new Size(233, 355);
            opponentListBox.TabIndex = 0;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label.Location = new Point(4, 8);
            label.Name = "label";
            label.Size = new Size(263, 27);
            label.TabIndex = 1;
            label.Text = "pleaseSelecYourOpponent";
            // 
            // askGameButton
            // 
            askGameButton.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            askGameButton.Location = new Point(62, 414);
            askGameButton.Name = "askGameButton";
            askGameButton.Size = new Size(171, 36);
            askGameButton.TabIndex = 2;
            askGameButton.Text = "twoPlayerBattle";
            askGameButton.UseVisualStyleBackColor = true;
            askGameButton.Click += startGameButton_Click;
            // 
            // countLabel
            // 
            countLabel.AutoSize = true;
            countLabel.Location = new Point(125, 35);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(43, 17);
            countLabel.TabIndex = 3;
            countLabel.Text = "label1";
            // 
            // logListBox
            // 
            logListBox.FormattingEnabled = true;
            logListBox.ItemHeight = 17;
            logListBox.Location = new Point(273, 53);
            logListBox.Name = "logListBox";
            logListBox.Size = new Size(233, 344);
            logListBox.TabIndex = 4;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(322, 414);
            button1.Name = "button1";
            button1.Size = new Size(122, 36);
            button1.TabIndex = 5;
            button1.Text = "singlePlayer";
            button1.UseVisualStyleBackColor = true;
            button1.Click += singlePlayer_Click;
            // 
            // opponentSelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 467);
            Controls.Add(button1);
            Controls.Add(logListBox);
            Controls.Add(countLabel);
            Controls.Add(askGameButton);
            Controls.Add(label);
            Controls.Add(opponentListBox);
            Name = "opponentSelectionForm";
            Text = "opponentSelectionForm";
            Load += opponentSelectionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox opponentListBox;
        private Label label;
        private Button askGameButton;
        private Label countLabel;
        private ListBox logListBox;
        private Button button1;
    }
}