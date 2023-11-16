using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;



namespace gomokuClient
{
    public partial class chessForm : Form
    {
        IPEndPoint EP;
        Socket T;
        private byte[,] S; // 棋盘状态数组0为空格，1为黑子，2为白子
        private static bool stopThread = false;
        String name, opponent;
        bool color;//挑战者为TRUE白，被挑战者为FALSE黑
        bool state;//true为可下棋，false为不可下棋
        Thread Th;

        public chessForm(String name, String opponent, IPEndPoint EP, Socket T, bool color)
        {
            InitializeComponent();
            this.name = name;
            this.opponent = opponent;
            this.EP = EP;
            this.T = T;
            this.Text = $"{name} fight {opponent}";
            this.color = color;
            state = color;
        }
        private void Chess(int i, int j, bool st)
        {
            using (Graphics g = chessBoardPanel.CreateGraphics())
            {
                Rectangle circleRect = new Rectangle(i * 30, j * 30, 30, 30);
                if (st)
                    g.FillEllipse(Brushes.White, circleRect);
                else
                    g.FillEllipse(Brushes.Black, circleRect);

            }
        }
        private void chessBoardPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (state)
            {
                int i = e.X / 30;
                int j = e.Y / 30;
                bool win = false;
                if (S[i, j] == 0)
                {

                    if (color)
                    {
                        S[i, j] = 1;
                        Chess(i, j, false);
                        if (chk5(i, j, 1))
                            win = true;
                    }
                    else
                    {
                        S[i, j] = 2;
                        Chess(i, j, true);
                        if (chk5(i, j, 2))
                            win = true;
                    }
                    if (win)
                    {
                        Send("f" + i.ToString() + "," + j.ToString() + "|" + opponent);
                        DialogResult result = MessageBox.Show("you win", "back to  menu", MessageBoxButtons.YesNo);
                        stopThread = true;
                        this.Close();
                    }
                    else
                    {
                        Send("d" + i.ToString() + "," + j.ToString() + "|" + opponent);
                        state = false;
                    }
                }
            }
        }
        private void Listen()
        {
            EndPoint ServerEP = (EndPoint)T.RemoteEndPoint;

            byte[] B = new byte[1023];
            int inLen = 0;
            string Msg;
            string Cmd;
            string Str;
            while (!stopThread)
            {
                try
                {
                    inLen = T.ReceiveFrom(B, ref ServerEP);
                }
                catch (Exception)
                {
                    T.Close();
                    stopThread = true;
                    return;
                }
                Msg = Encoding.Default.GetString(B, 0, inLen);
                if (Msg == "")
                    return;
                /*this.Invoke(new Action(() =>
                {
                    logListBox.Items.Add("从服务端接收到：" + Msg);
                }));*/
                Cmd = Msg.Substring(0, 1);
                Str = Msg.Substring(1);
                switch (Cmd)
                {
                    case "f":
                        String[] location = Str.Split(',');
                        int i = int.Parse(location[0]), j = int.Parse(location[1]);
                        if (color)
                        {
                            S[i, j] = 2;
                            Chess(i, j, true);
                        }
                        else
                        {
                            S[i, j] = 1;
                            Chess(i, j, false);
                        }
                        state = true;
                        break;
                    case "g":
                        String[] glocation = Str.Split(',');
                        int gi = int.Parse(glocation[0]), gj = int.Parse(glocation[1]);
                        if (color)
                        {
                            S[gi, gj] = 2;
                            Chess(gi, gj, true);
                        }
                        else
                        {
                            S[gi, gj] = 1;
                            Chess(gi, gj, false);
                        }
                        DialogResult result = MessageBox.Show("you lose", "back to  menu", MessageBoxButtons.YesNo);
                        this.Invoke(new Action(() =>
                        {
                            stopThread = true;
                            this.Close();
                        }));
                        break;
                    default:
                        break;
                }

            }
        }
        private Thread startListen()
        {
            stopThread = false;
            Thread th = new Thread(Listen);
            th.IsBackground = true;
            th.Start();
            return th;
        }

        private void chessForm_Load(object sender, EventArgs e)
        {
            chessBoardPanel.Size = new Size(570, 570);
            chessBoardPanel.BackColor = Color.BurlyWood;
            chessBoardPanel.Paint += new PaintEventHandler(chessBoardPanel_Paint); // 将事件处理程序与Paint事件关联
            this.Controls.Add(chessBoardPanel);
            Th = startListen();
            S = new byte[19, 19];
        }
        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);
            T.Send(B, 0, B.Length, SocketFlags.None);
        }

        private void chessBoardPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 15; i <= 555; i += 30)
            {
                // 画19条垂直线
                Pen pen = new Pen(Color.Black);
                g.DrawLine(pen, i, 15, i, 555);
            }

            for (int j = 15; j <= 555; j += 30)
            {
                // 画19条水平线
                Pen pen = new Pen(Color.Black);
                g.DrawLine(pen, 15, j, 555, j);
            }
        }
        private bool chk5(int i, int j, byte tg)
        {
            int n = 0;
            int ii, jj;
            for (int k = -4; k <= 4; k++)
            {
                ii = i + k;
                if (ii >= 0 && ii < 19)
                {
                    if (S[ii, j] == tg)
                    {
                        n += 1;
                        if (n == 5) return true;
                    }
                    else
                    {
                        n = 0;
                    }
                }
            }
            //垂直
            n = 0;
            for (int k = -4; k <= 4; k++)
            {
                jj = j + k;
                if (jj >= 0 && jj < 19)
                {
                    if (S[i, jj] == tg)
                    {
                        n += 1;
                        if (n == 5) return true;
                    }
                    else
                    {
                        n = 0;
                    }
                }
            }
            //左上至右下
            n = 0;
            for (int k = -4; k <= 4; k++)
            {
                ii = i + k; jj = j + k;
                if (ii >= 0 && ii < 19 && jj >= 0 && jj < 19)
                {
                    if (S[ii, jj] == tg)
                    {
                        n += 1;
                        if (n == 5) return true;
                    }
                    else
                    {
                        n = 0;
                    }
                }
            }
            //右上至左下
            n = 0;
            for (int k = -4; k <= 4; k++)
            {
                ii = i - k; jj = j + k;
                if (ii >= 0 && ii < 19 && jj >= 0 && jj < 19)
                {
                    if (S[ii, jj] == tg)
                    {
                        n += 1;
                        if (n == 5) return true;
                    }
                    else
                    {
                        n = 0;
                    }
                }

            }
            return false;
        }
    }
}
