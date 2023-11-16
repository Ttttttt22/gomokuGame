using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Net;         //导入网路通讯协定相关函数
using System.Net.Sockets; //导入网路插座功能函数
using System.Threading;   //导入多线程功能函数 
using System.Collections; //导入集合物件
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

using System.Xml.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using Google.Protobuf.WellKnownTypes;
using System.Windows.Forms;

namespace gomokuServer
{

    public partial class serverForm : Form
    {
        public serverForm()
        {
            InitializeComponent();
        }
        TcpListener Server;             //服务端网路监听器(相当于电话总机)
        Socket Client;                  //给客户用的连线物件(相当于电话分机)
        Thread Th_Svr;                  //服务器监听用线程(电话总机开放中)
        Thread Th_Clt;                  //客户用的通话线程(电话分机连线中)
        Hashtable HT = new Hashtable(); //客户名称与通讯物件的集合(哈希表)(key:Name, Socket)
        //List<string> userlist = new List<string>();
        String connStr = "server=localhost;database=gomoku;uid=root;password=12345678;charset=utf8";
        private void serverForm_Load(object sender, EventArgs e)
        {
            serverIPTextBox.Text = MyIP();

        }
        private void serverStartButton_Click(object sender, EventArgs e)
        {
            //忽略跨线程处理的错误(允许跨线程存取资源)
            CheckForIllegalCrossThreadCalls = false;
            Th_Svr = new Thread(ServerSub);     //创建监听线程(副程式ServerSub)
            Th_Svr.IsBackground = true;         //设定为后台线程
            Th_Svr.Start();                     //启动监听线程
            serverStartButton.Enabled = false;            //让按键无法使用(不能重复启动服务器)
        }
        //接受客户连线要求的程序(如同电话总机)，针对每一客户会建立一个连线，以及独立线程
        private void ServerSub()
        {
            //Server IP 和 Port
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(serverIPTextBox.Text), int.Parse(serverPortTextBox.Text));
            Server = new TcpListener(EP);       //建立服务端监听器(总机)
            Server.Start(100);                  //启动监听设定允许最多连线数100人
            while (true)                        //无限循环监听连线要求
            {
                Client = Server.AcceptSocket(); //建立此客户的连线物件Client
                Th_Clt = new Thread(Listen);    //建立监听这个客户连线的独立线程
                Th_Clt.IsBackground = true;     //设定为后台线程
                Th_Clt.Start();                 //开始线程的运作
            }
        }
        //获取本机IP
        private string MyIP()
        {
            string hn = Dns.GetHostName();
            IPAddress[] ip = Dns.GetHostEntry(hn).AddressList; //获取本机IP阵列
            foreach (IPAddress it in ip)
            {
                if (it.AddressFamily == AddressFamily.InterNetwork)
                {
                    return it.ToString(); //如果是IPv4返回此IP字符串
                }
            }
            return "";                    //找不到合格IP返回空字符串
        }

        //监听客户信息的程序
        private void Listen()
        {
            Socket Sck = Client; //复制Client通信物件到个别客户专用物件Sck
            Thread Th = Th_Clt;  //复制线程Th_Clt到区域变数Th
            while (true)
            {
                try
                {
                    byte[] msgB = new byte[1023];
                    int inLen = Sck.Receive(msgB);
                    string Msg = Encoding.Default.GetString(msgB, 0, inLen);
                    string Cmd = Msg.Substring(0, 1);  //取出命令码 (第一个字)
                    string Str = Msg.Substring(1);  //取出命令码之后的信息
                    this.Invoke(new Action(() =>
                    {
                        logListBox.Items.Add("从客户端接收到：" + Msg);
                    }));
                    switch (Cmd)
                    {
                        case "0"://登录
                            string[] logInS = Str.Split('|');
                            String logInName = logInS[0];
                            String logInPassword = logInS[1];
                            bool br = false;
                            foreach (String name in HT.Keys)
                            {
                                if (name == logInName)
                                {
                                    Sck.Send(Encoding.Default.GetBytes("0" + "this user is already online"), SocketFlags.None);
                                    br = true;
                                    break;
                                }
                            }
                            if (br)
                                break;
                            //logListBox.Items.Add("name: " + logInName + " password: " + logInPassword);
                            //创建数据库连接对象
                            MySqlConnection logInConn = new MySqlConnection(connStr);
                            //打开数据库
                            logInConn.Open();
                            //创建执行脚本对象
                            String sql = $"select * from users where name='{logInName}'";
                            MySqlCommand logInCommand = new MySqlCommand(sql, logInConn);
                            //执行脚本
                            MySqlDataReader logInReader = logInCommand.ExecuteReader();
                            if (logInReader.Read())
                            {
                                if (logInReader["password"].ToString() == logInPassword)
                                {
                                    logListBox.Items.Add($"name:{logInReader["name"]}, password:{logInReader["password"]}上线");
                                    HT.Add(logInName, Sck);   //有新用户上线，加入使用者名单
                                    countLabel.Text = HT.Count.ToString();
                                    Sck.Send(Encoding.Default.GetBytes("0" + "success"), SocketFlags.None);
                                    Thread.Sleep(800);
                                    SendAllException("a" + logInName, Sck);//告知新用户上线，更新列表
                                    logListBox.Items.Add("向所有用户发送" + "a" + logInName);

                                }
                                else
                                {
                                    Sck.Send(Encoding.Default.GetBytes("0" + "passwordError"), SocketFlags.None);
                                }
                            }
                            else
                            {
                                Sck.Send(Encoding.Default.GetBytes("0" + "nameError"), SocketFlags.None);
                            }

                            //释放资源
                            logInReader.Close();
                            logInConn.Close();
                            break;//接受登录请求
                        case "1"://注册
                            string[] signInS = Str.Split('|');
                            String signInName = signInS[0];
                            String signInPassword = signInS[1];

                            //创建数据库连接对象
                            MySqlConnection signUpConn = new MySqlConnection(connStr);
                            //打开数据库
                            signUpConn.Open();
                            //创建执行脚本对象
                            String signInSql = $"INSERT INTO users(`name`, `password`) VALUES('{signInName}', '{signInPassword}')";
                            String trySignInSql = $"select * from users where name='{signInName}'";
                            MySqlCommand signInCommand = new MySqlCommand(signInSql, signUpConn);
                            MySqlCommand trySignInCommand = new MySqlCommand(trySignInSql, signUpConn);
                            //执行脚本
                            MySqlDataReader trySignInReader = trySignInCommand.ExecuteReader();

                            //执行脚本
                            if (trySignInReader.Read())
                            {
                                Sck.Send(Encoding.Default.GetBytes("1" + "Username already in use"), SocketFlags.None);
                                trySignInReader.Close();
                                signUpConn.Close();
                                break;
                            }
                            trySignInReader.Close();
                            try
                            {
                                int result = signInCommand.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    logListBox.Items.Add("成功注册");
                                    Sck.Send(Encoding.Default.GetBytes("1" + "Successful registration"), SocketFlags.None);
                                }
                            }
                            catch (Exception)
                            {
                                logListBox.Items.Add("注册失败");
                                Sck.Send(Encoding.Default.GetBytes("1" + "Registration failed"), SocketFlags.None);
                            }

                            signUpConn.Close();
                            break;//接受注册请求
                        case "2"://接收退出请求
                            //logListBox.Items.Add("收到退出请求");

                            SendAllException("b" + Str, Sck);
                            logListBox.Items.Add("有用户退出：" + "b" + Str);
                            break;//接收退出请求
                        case "a"://重新发送用户列表
                            foreach (DictionaryEntry entry in HT)
                            {
                                Thread.Sleep(800);
                                Sck.Send(Encoding.Default.GetBytes("a" + $"{entry.Key}"), SocketFlags.None);
                                logListBox.Items.Add("回应列表请求：" + "a" + $"{entry.Key}");
                            }
                            break;//重新发送用户列表
                        case "b"://接收对战请求
                            String[] C = Str.Split('|');
                            String Challenger = C[0];
                            String Opponent = C[1];
                            SendTo("c"+ Challenger, Opponent);
                            logListBox.Items.Add($"{Challenger}挑战{Opponent}，发送:" + "c" + Challenger);
                            break;//接收对战请求
                        case "c"://被挑战者拒绝
                            String[] rC = Str.Split('|');
                            String rChallenger = rC[0];
                            String rOpponent = rC[1];
                            logListBox.Items.Add($"收到:{rOpponent}拒绝{rChallenger}的挑战请求");
                            SendTo("d" + rOpponent, rChallenger);
                            break;//被挑战者拒绝
                        case "d"://转发落子位置
                            String[] dC = Str.Split('|');
                            String[] location = dC[0].Split(",");
                            logListBox.Items.Add($"收到对{dC[1]}请求落子到x:{location[0]} y:{location[1]}");
                            SendTo("f" + dC[0], dC[1]);
                            logListBox.Items.Add($"发送给{dC[1]}: e + {dC[0]}");
                            break;//转发落子位置
                        case "e"://被挑战者接受
                            String[] eC = Str.Split('|');
                            String eOpponent = eC[0]; 
                            String eChallenger = eC[1];
                            logListBox.Items.Add($"收到{eOpponent}接受{eChallenger}的挑战请求");
                            SendTo("e" + eOpponent, eChallenger);
                            logListBox.Items.Add($"发送给{eChallenger}:e + {eOpponent}");
                            break;//被挑战者接受
                        case "f":
                            String[] fC = Str.Split('|');
                            String[] flocation = fC[0].Split(",");
                            logListBox.Items.Add($"收到对{fC[1]}请求落子到x:{flocation[0]} y:{flocation[1]}并且{fC[1]}输掉比赛");
                            SendTo("g" + fC[0], fC[1]);
                            logListBox.Items.Add($"发送给{fC[1]}: e + {fC[0]}并且并且提示已经输掉比赛");
                            break;//转发落子位置并发出胜负提示
                        default:
                            break;
                    }

                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        // 客户端强制关闭连接，执行清理操作
                        Socket socketToDelete = Sck;
                        ArrayList keysToDelete = new ArrayList();

                        //
                        foreach (DictionaryEntry entry in HT)
                        {
                            if (entry.Value.Equals(socketToDelete))
                            {
                                keysToDelete.Add(entry.Key);
                                //userlist.Remove(entry.Value.ToString());
                            }
                        }

                        foreach (object key in keysToDelete)
                        {
                            HT.Remove(key);
                        }
                        //删除HT里面的用户
                        
                        countLabel.Text = HT.Count.ToString();
                        Sck.Close();
                        logListBox.Items.Add("jieshu");
                        break;
                    }// 客户端强制关闭连接，执行清理操作
                    // 处理其他SocketException
                }
                catch (Exception)
                {
                    logListBox.Items.Add("异常");
                }
            }
        }

        //发送信息给指定的客户
        private void SendTo(string Str, string User)
        {
            byte[] B = Encoding.Default.GetBytes(Str);  //信息转译为byte阵列
            Socket Sck = HT[User] as Socket;              //取出发送对象User的通讯物件
            Sck.Send(B, 0, B.Length, SocketFlags.None); //发送信息
        }
        //发送信息给所有的线上客户
        private void SendAll(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);   //信息转译为Byte阵列
            foreach (Socket s in HT.Values)              //HT哈希表内所有的Socket
                s.Send(B, 0, B.Length, SocketFlags.None);//发送信息
        }
        //发送信息给所有的线上客户
        private void SendAllException(string Str, Socket sk)
        {
            byte[] B = Encoding.Default.GetBytes(Str);   //信息转译为Byte阵列
            foreach (Socket s in HT.Values)              //HT哈希表内所有的Socket
            {
                if (s != sk)
                    s.Send(B, 0, B.Length, SocketFlags.None);//发送信息
            }
        }

    }

}
