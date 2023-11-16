using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Net;         //������·ͨѶЭ����غ���
using System.Net.Sockets; //������·�������ܺ���
using System.Threading;   //������̹߳��ܺ��� 
using System.Collections; //���뼯�����
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
        TcpListener Server;             //�������·������(�൱�ڵ绰�ܻ�)
        Socket Client;                  //���ͻ��õ��������(�൱�ڵ绰�ֻ�)
        Thread Th_Svr;                  //�������������߳�(�绰�ܻ�������)
        Thread Th_Clt;                  //�ͻ��õ�ͨ���߳�(�绰�ֻ�������)
        Hashtable HT = new Hashtable(); //�ͻ�������ͨѶ����ļ���(��ϣ��)(key:Name, Socket)
        //List<string> userlist = new List<string>();
        String connStr = "server=localhost;database=gomoku;uid=root;password=12345678;charset=utf8";
        private void serverForm_Load(object sender, EventArgs e)
        {
            serverIPTextBox.Text = MyIP();

        }
        private void serverStartButton_Click(object sender, EventArgs e)
        {
            //���Կ��̴߳���Ĵ���(������̴߳�ȡ��Դ)
            CheckForIllegalCrossThreadCalls = false;
            Th_Svr = new Thread(ServerSub);     //���������߳�(����ʽServerSub)
            Th_Svr.IsBackground = true;         //�趨Ϊ��̨�߳�
            Th_Svr.Start();                     //���������߳�
            serverStartButton.Enabled = false;            //�ð����޷�ʹ��(�����ظ�����������)
        }
        //���ܿͻ�����Ҫ��ĳ���(��ͬ�绰�ܻ�)�����ÿһ�ͻ��Ὠ��һ�����ߣ��Լ������߳�
        private void ServerSub()
        {
            //Server IP �� Port
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(serverIPTextBox.Text), int.Parse(serverPortTextBox.Text));
            Server = new TcpListener(EP);       //��������˼�����(�ܻ�)
            Server.Start(100);                  //���������趨�������������100��
            while (true)                        //����ѭ����������Ҫ��
            {
                Client = Server.AcceptSocket(); //�����˿ͻ����������Client
                Th_Clt = new Thread(Listen);    //������������ͻ����ߵĶ����߳�
                Th_Clt.IsBackground = true;     //�趨Ϊ��̨�߳�
                Th_Clt.Start();                 //��ʼ�̵߳�����
            }
        }
        //��ȡ����IP
        private string MyIP()
        {
            string hn = Dns.GetHostName();
            IPAddress[] ip = Dns.GetHostEntry(hn).AddressList; //��ȡ����IP����
            foreach (IPAddress it in ip)
            {
                if (it.AddressFamily == AddressFamily.InterNetwork)
                {
                    return it.ToString(); //�����IPv4���ش�IP�ַ���
                }
            }
            return "";                    //�Ҳ����ϸ�IP���ؿ��ַ���
        }

        //�����ͻ���Ϣ�ĳ���
        private void Listen()
        {
            Socket Sck = Client; //����Clientͨ�����������ͻ�ר�����Sck
            Thread Th = Th_Clt;  //�����߳�Th_Clt���������Th
            while (true)
            {
                try
                {
                    byte[] msgB = new byte[1023];
                    int inLen = Sck.Receive(msgB);
                    string Msg = Encoding.Default.GetString(msgB, 0, inLen);
                    string Cmd = Msg.Substring(0, 1);  //ȡ�������� (��һ����)
                    string Str = Msg.Substring(1);  //ȡ��������֮�����Ϣ
                    this.Invoke(new Action(() =>
                    {
                        logListBox.Items.Add("�ӿͻ��˽��յ���" + Msg);
                    }));
                    switch (Cmd)
                    {
                        case "0"://��¼
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
                            //�������ݿ����Ӷ���
                            MySqlConnection logInConn = new MySqlConnection(connStr);
                            //�����ݿ�
                            logInConn.Open();
                            //����ִ�нű�����
                            String sql = $"select * from users where name='{logInName}'";
                            MySqlCommand logInCommand = new MySqlCommand(sql, logInConn);
                            //ִ�нű�
                            MySqlDataReader logInReader = logInCommand.ExecuteReader();
                            if (logInReader.Read())
                            {
                                if (logInReader["password"].ToString() == logInPassword)
                                {
                                    logListBox.Items.Add($"name:{logInReader["name"]}, password:{logInReader["password"]}����");
                                    HT.Add(logInName, Sck);   //�����û����ߣ�����ʹ��������
                                    countLabel.Text = HT.Count.ToString();
                                    Sck.Send(Encoding.Default.GetBytes("0" + "success"), SocketFlags.None);
                                    Thread.Sleep(800);
                                    SendAllException("a" + logInName, Sck);//��֪���û����ߣ������б�
                                    logListBox.Items.Add("�������û�����" + "a" + logInName);

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

                            //�ͷ���Դ
                            logInReader.Close();
                            logInConn.Close();
                            break;//���ܵ�¼����
                        case "1"://ע��
                            string[] signInS = Str.Split('|');
                            String signInName = signInS[0];
                            String signInPassword = signInS[1];

                            //�������ݿ����Ӷ���
                            MySqlConnection signUpConn = new MySqlConnection(connStr);
                            //�����ݿ�
                            signUpConn.Open();
                            //����ִ�нű�����
                            String signInSql = $"INSERT INTO users(`name`, `password`) VALUES('{signInName}', '{signInPassword}')";
                            String trySignInSql = $"select * from users where name='{signInName}'";
                            MySqlCommand signInCommand = new MySqlCommand(signInSql, signUpConn);
                            MySqlCommand trySignInCommand = new MySqlCommand(trySignInSql, signUpConn);
                            //ִ�нű�
                            MySqlDataReader trySignInReader = trySignInCommand.ExecuteReader();

                            //ִ�нű�
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
                                    logListBox.Items.Add("�ɹ�ע��");
                                    Sck.Send(Encoding.Default.GetBytes("1" + "Successful registration"), SocketFlags.None);
                                }
                            }
                            catch (Exception)
                            {
                                logListBox.Items.Add("ע��ʧ��");
                                Sck.Send(Encoding.Default.GetBytes("1" + "Registration failed"), SocketFlags.None);
                            }

                            signUpConn.Close();
                            break;//����ע������
                        case "2"://�����˳�����
                            //logListBox.Items.Add("�յ��˳�����");

                            SendAllException("b" + Str, Sck);
                            logListBox.Items.Add("���û��˳���" + "b" + Str);
                            break;//�����˳�����
                        case "a"://���·����û��б�
                            foreach (DictionaryEntry entry in HT)
                            {
                                Thread.Sleep(800);
                                Sck.Send(Encoding.Default.GetBytes("a" + $"{entry.Key}"), SocketFlags.None);
                                logListBox.Items.Add("��Ӧ�б�����" + "a" + $"{entry.Key}");
                            }
                            break;//���·����û��б�
                        case "b"://���ն�ս����
                            String[] C = Str.Split('|');
                            String Challenger = C[0];
                            String Opponent = C[1];
                            SendTo("c"+ Challenger, Opponent);
                            logListBox.Items.Add($"{Challenger}��ս{Opponent}������:" + "c" + Challenger);
                            break;//���ն�ս����
                        case "c"://����ս�߾ܾ�
                            String[] rC = Str.Split('|');
                            String rChallenger = rC[0];
                            String rOpponent = rC[1];
                            logListBox.Items.Add($"�յ�:{rOpponent}�ܾ�{rChallenger}����ս����");
                            SendTo("d" + rOpponent, rChallenger);
                            break;//����ս�߾ܾ�
                        case "d"://ת������λ��
                            String[] dC = Str.Split('|');
                            String[] location = dC[0].Split(",");
                            logListBox.Items.Add($"�յ���{dC[1]}�������ӵ�x:{location[0]} y:{location[1]}");
                            SendTo("f" + dC[0], dC[1]);
                            logListBox.Items.Add($"���͸�{dC[1]}: e + {dC[0]}");
                            break;//ת������λ��
                        case "e"://����ս�߽���
                            String[] eC = Str.Split('|');
                            String eOpponent = eC[0]; 
                            String eChallenger = eC[1];
                            logListBox.Items.Add($"�յ�{eOpponent}����{eChallenger}����ս����");
                            SendTo("e" + eOpponent, eChallenger);
                            logListBox.Items.Add($"���͸�{eChallenger}:e + {eOpponent}");
                            break;//����ս�߽���
                        case "f":
                            String[] fC = Str.Split('|');
                            String[] flocation = fC[0].Split(",");
                            logListBox.Items.Add($"�յ���{fC[1]}�������ӵ�x:{flocation[0]} y:{flocation[1]}����{fC[1]}�������");
                            SendTo("g" + fC[0], fC[1]);
                            logListBox.Items.Add($"���͸�{fC[1]}: e + {fC[0]}���Ҳ�����ʾ�Ѿ��������");
                            break;//ת������λ�ò�����ʤ����ʾ
                        default:
                            break;
                    }

                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        // �ͻ���ǿ�ƹر����ӣ�ִ���������
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
                        //ɾ��HT������û�
                        
                        countLabel.Text = HT.Count.ToString();
                        Sck.Close();
                        logListBox.Items.Add("jieshu");
                        break;
                    }// �ͻ���ǿ�ƹر����ӣ�ִ���������
                    // ��������SocketException
                }
                catch (Exception)
                {
                    logListBox.Items.Add("�쳣");
                }
            }
        }

        //������Ϣ��ָ���Ŀͻ�
        private void SendTo(string Str, string User)
        {
            byte[] B = Encoding.Default.GetBytes(Str);  //��Ϣת��Ϊbyte����
            Socket Sck = HT[User] as Socket;              //ȡ�����Ͷ���User��ͨѶ���
            Sck.Send(B, 0, B.Length, SocketFlags.None); //������Ϣ
        }
        //������Ϣ�����е����Ͽͻ�
        private void SendAll(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);   //��Ϣת��ΪByte����
            foreach (Socket s in HT.Values)              //HT��ϣ�������е�Socket
                s.Send(B, 0, B.Length, SocketFlags.None);//������Ϣ
        }
        //������Ϣ�����е����Ͽͻ�
        private void SendAllException(string Str, Socket sk)
        {
            byte[] B = Encoding.Default.GetBytes(Str);   //��Ϣת��ΪByte����
            foreach (Socket s in HT.Values)              //HT��ϣ�������е�Socket
            {
                if (s != sk)
                    s.Send(B, 0, B.Length, SocketFlags.None);//������Ϣ
            }
        }

    }

}
