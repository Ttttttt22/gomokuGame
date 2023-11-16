using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace gomokuClient
{
    public partial class startForm : Form
    {
        Socket T; //通信控件
        Thread Th; //监听线程
        string User; //用户
        string IP;
        int Port;
        IPEndPoint EP;
        bool connectionState;
        private static bool stopThread = false;
        opponentSelectionForm osFrom;


        public startForm(String IP, int port)
        {
            InitializeComponent();
            this.IP = IP;
            this.Port = port;
            IPTextBox.Text = IP;
            portTextBox.Text = Port.ToString();
            EP = new IPEndPoint(IPAddress.Parse(IP), Port);
            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            nameRrrorProvider.Clear();
            passwordErrorProvider.Clear();
            //Control.CheckForIllegalCrossThreadCalls = false; //---

            if (connectionState == false)
            {
                MessageBox.Show("notConnected");
                return;
            }
            Send("0" + nameTextBox.Text + "|" + passwordTextBox.Text);


        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            if (connectionState == false)
            {
                MessageBox.Show("notConnected");
            }
            Send("1" + nameTextBox.Text + "|" + passwordTextBox.Text);
        }

        private void startForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connectionState == false)
                return;
            try
            {
                Send("2" + User);
            }
            catch (Exception ex)
            {

            }
        }

        //发送信息到服务器
        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);
            T.Send(B, 0, B.Length, SocketFlags.None);
        }

        private void startForm_Load(object sender, EventArgs e)
        {
            if (tryToConnect(EP))
            {
                Thread Th = startListen();
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

        //尝试重新连接
        private void connectionStateLabel_Click(object sender, EventArgs e)
        {
            IP = IPTextBox.Text;
            Port = int.Parse(portTextBox.Text);
            EP = new IPEndPoint(IPAddress.Parse(IP), Port);
            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (tryToConnect(EP))
            {
                Thread Th = startListen();
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
                    OnConnectionFailure();
                    stopThread = true;
                    return;
                }


                Msg = Encoding.Default.GetString(B, 0, inLen);
                if (Msg == "")
                    return;
                Cmd = Msg.Substring(0, 1);
                Str = Msg.Substring(1);
                switch (Cmd)
                {
                    case "0":
                        if (logInCheck(Str))
                        {
                            User = nameTextBox.Text;
                            osFrom = new opponentSelectionForm(EP, T, nameTextBox.Text);
                            osFrom.ShowDialog();
                            osFrom.Close();
                            osFrom.Dispose();
                        }
                        break;
                    case "1":
                        signUpCheck(Str);
                        break;
                    case "":

                        break;
                    default:
                        break;
                }



            }
        }
        private bool tryToConnect(IPEndPoint EP)
        {
            try
            {
                T.Connect(EP);
                OnConnectionSuccess();
            }
            catch
            {
                OnConnectionFailure();
                MessageBox.Show("please check or reset IP and port");
                return false;
            }
            return true;
        }
        private void SetConnectionStateLabel(string text, Color color)
        {
            if (connectionStateLabel.InvokeRequired)
            {
                // 如果不是UI线程，使用Invoke在UI线程上执行操作
                connectionStateLabel.Invoke(new Action(() =>
                {
                    connectionStateLabel.Text = text;
                    connectionStateLabel.ForeColor = color;
                    if (color == Color.Green)
                    {
                        ConnectionToolTip.RemoveAll(); // 删除所有ToolTip
                    }
                    else
                    {
                        ConnectionToolTip.SetToolTip(connectionStateLabel, "click to retry");
                    }
                }));
            }
            else
            {
                // 如果是UI线程，直接更新UI控件
                connectionStateLabel.Text = text;
                connectionStateLabel.ForeColor = color;
                if (color == Color.Green)
                {
                    ConnectionToolTip.RemoveAll(); // 删除所有ToolTip
                }
                else
                {
                    ConnectionToolTip.SetToolTip(connectionStateLabel, "click to retry");
                }
            }
        }
        // 在连接成功时调用此方法
        private void OnConnectionSuccess()
        {
            SetConnectionStateLabel("connected", Color.Green);

            connectionState = true;
        }

        // 在连接失败时调用此方法
        private void OnConnectionFailure()
        {
            SetConnectionStateLabel("notConnected", Color.Red);

            connectionState = false;
        }
        private bool logInCheck(String text)
        {
            if (text == "nameError")
            {
                this.Invoke(new Action(() =>
                {
                    nameRrrorProvider.SetError(nameTextBox, "please check");
                }));
                return false;
            }
            else if (text == "passwordError")
            {
                this.Invoke(new Action(() =>
                {
                    passwordErrorProvider.SetError(passwordTextBox, "please check");
                }));
                return false;
            }
            else if (text == "this user is already online")
            {
                this.Invoke(new Action(() =>
                {
                    nameRrrorProvider.SetError(nameTextBox, "this user is already online");
                }));
                return false;
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    logListBox.Items.Add(text);
                }));
                return true;
            }

        }
        private bool signUpCheck(String text)
        {
            if (text == "Username already in use")
            {
                this.Invoke(new Action(() =>
                {
                    nameRrrorProvider.SetError(nameTextBox, "Username already in use");
                }));
                return false;
            }
            else if (text == "Successful registration")
            {
                MessageBox.Show("Successful registration");
                return true;
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Registration failed");
                }));
                return false;
            }

        }


    }
}