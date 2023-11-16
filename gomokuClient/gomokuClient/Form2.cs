using A21智能五子棋04200923;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gomokuClient
{
    public partial class opponentSelectionForm : Form
    {
        IPEndPoint EP;
        Socket T;
        List<string> usersList = new List<string>();
        private static bool stopThread = false;
        chessForm cform;
        Mainform mform;
        String name;
        Thread Th;
        public opponentSelectionForm(IPEndPoint EP, Socket T, string name)
        {
            InitializeComponent();
            this.EP = EP;
            this.T = T;
            this.name = name;
        }

        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);
            T.Send(B, 0, B.Length, SocketFlags.None);
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
                this.Invoke(new Action(() =>
                {
                    logListBox.Items.Add("从服务端接收到：" + Msg);
                }));
                Cmd = Msg.Substring(0, 1);
                Str = Msg.Substring(1);
                switch (Cmd)
                {
                    case "a"://更新用户列表

                        if (!usersList.Contains(Str))
                        {
                            usersList.Add(Str);
                        }
                        this.Invoke(new Action(() =>
                        {
                            opponentListBox.Items.Clear();
                            foreach (string item in usersList)
                            {
                                opponentListBox.Items.Add(item);
                            }
                            this.countLabel.Text = usersList.Count.ToString();
                        }));

                        break;
                    case "b":
                        if (usersList.Contains(Str))
                        {
                            usersList.Remove(Str);
                        }
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                opponentListBox.Items.Clear();
                                foreach (string item in usersList)
                                {
                                    opponentListBox.Items.Add(item);
                                }
                                this.countLabel.Text = usersList.Count.ToString();
                            }));

                        }
                        break;
                    case "c"://收到对战邀请
                        DialogResult result = MessageBox.Show($"{Str}AskForBattle", "确认", MessageBoxButtons.YesNo);
                        //Str是挑战者name被挑战者
                        if (result == DialogResult.Yes)
                        {
                            Send("e" + name + "|" + Str);//告知服务器接受挑战
                            cform = new chessForm(name, Str, EP, T, false);//接受挑战开启对战窗口
                            cform.ShowDialog();
                            cform.Dispose();
                        }
                        else
                        {
                            Send("c" + Str + "|" + name);//向服务器发送邀请拒绝
                        }
                        break;
                    case "d"://收到对方拒绝
                        this.Invoke(new Action(() =>
                        {
                            logListBox.Items.Add($"{Str}拒绝了对战请求");
                        }));
                        MessageBox.Show($"{Str}Refused");
                        break;
                    case "e"://对方同意对战需要打开对战窗口
                        cform = new chessForm(name, Str, EP, T, true);
                        cform.ShowDialog();
                        cform.Dispose();
                        this.Invoke(new Action(() =>
                        {
                            logListBox.Items.Add($"{Str}同意了对战请求");
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

        private void opponentSelectionForm_Load(object sender, EventArgs e)
        {
            Th = startListen();
            Send("a");//要用户列表
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            if (opponentListBox.SelectedItem.ToString() == name)
            {
                MessageBox.Show("you cannot select your yourself");
                return;
            }
            if (opponentListBox.SelectedIndex >= 0)
            {
                Send("b" + name + "|" + opponentListBox.SelectedItem);//发送对战请求
                this.Invoke(new Action(() =>
                {
                    logListBox.Items.Add("向服务器发送：" + "b" + opponentListBox.SelectedItem);
                }));
            }

        }

        private void singlePlayer_Click(object sender, EventArgs e)
        {
            mform = new Mainform();
            mform.ShowDialog();
            mform.Dispose();
        }
    }
}
