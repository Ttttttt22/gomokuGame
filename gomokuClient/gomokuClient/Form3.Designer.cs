namespace gomokuClient
{
    partial class chessForm
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
            chessBoardPanel = new Panel();
            logListBox = new ListBox();
            SuspendLayout();
            // 
            // chessBoardPanel
            // 
            chessBoardPanel.Location = new Point(12, 12);
            chessBoardPanel.Name = "chessBoardPanel";
            chessBoardPanel.Size = new Size(570, 570);
            chessBoardPanel.TabIndex = 0;
            chessBoardPanel.Paint += chessBoardPanel_Paint;
            chessBoardPanel.MouseDown += chessBoardPanel_MouseDown;
            // 
            // logListBox
            // 
            logListBox.FormattingEnabled = true;
            logListBox.ItemHeight = 17;
            logListBox.Location = new Point(606, 12);
            logListBox.Name = "logListBox";
            logListBox.Size = new Size(160, 565);
            logListBox.TabIndex = 1;
            // 
            // chessForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 596);
            Controls.Add(logListBox);
            Controls.Add(chessBoardPanel);
            Name = "chessForm";
            Text = "chessForm";
            Load += chessForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel chessBoardPanel;
        private ListBox logListBox;
    }
}