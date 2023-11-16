using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace A21智能五子棋04200923
{
    class ChessBoard
    {
        public Chess[,] chess;
        public PictureBox[,] pbox;
        public ListBox lb;
        public int round = 1;
        public string win=null;
        public ChessBoard(PictureBox[,] pbox, ListBox lb)
        {
            this.lb = lb;
            this.chess = new Chess[15, 15];
            this.pbox = pbox;
            for (int i = 0; i < 15; i++)//初始化全部
            {
                for (int j = 0; j < 15; j++)
                {
                    chess[i, j] = new Chess(i, j);
                }
            }
        }
        public void resetpbox()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (pbox[i, j].Image != null)
                        chess[i, j].clearpicture(pbox);
                }
            }
        }
        public void setpicture(int x, int y)
        {
            if (chess[x, y].color == "b")
                pbox[x, y].Image = Image.FromFile("黑棋.png");    
            if (chess[x, y].color == "w")
                pbox[x, y].Image = Image.FromFile("白棋.png");
            //Thread.Sleep(1);
        }
        public void addchess(int x, int y, string color,bool test)//1为黑，0为白
        {
            //lb.Items.Clear();//
            chess[x, y].color = color;
            setpicture(x, y);
            if (judge(x, y,color))
            {
                if(chess[x, y].color == "w")
                {
                    if(test==false)
                        MessageBox.Show("白方获胜");
                    win = "white";
                }
                    
                if (chess[x, y].color == "b")
                {
                    if (test == false)
                        MessageBox.Show("黑方获胜");
                    win = "black";
                }
            }
            //int hs = huosi(x, y,color);
            //int cs = chongsi(x, y, color);
            //int hsan = huosan(x, y, color);
            //int he = huoer(x, y, color);
            round++;
        }
        public int bijiao(string targrt, string major)
        {
            if (major.Length < targrt.Length)
                return 0;
            int result = 0;
            for (int i = 0; i < major.Length; i++)
            {
                int j = 0;
                for (j = 0; j < targrt.Length; j++)
                {
                    if (i + j < major.Length)
                    {
                        if (targrt[j] != major[i + j])
                            break;
                    }
                    else
                        break;
                }
                if (j == targrt.Length)
                    result++;
            }
            return result;
        }
        public bool judge(int x,int y,string col)
        {
            lb.Items.Add(x+" "+y+" "+col+"胜负判断");
            string[] panduan = new string[4];
            for(int i = 1; i <= 4 ; i++)
            {
                if (x + i <= 14)
                {
                    if (chess[x + i, y].color == col)
                        panduan[0] += "1";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[0] = "1" + panduan[0];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0)
                {
                    if (chess[x - i, y].color == col)
                        panduan[0] = "1" + panduan[0];
                    else
                        break;
                }
                else
                    break; 
            }
            //------------横
            for (int i = 1; i <= 4; i++)
            {
                if (y + i <= 14)
                {
                    if (chess[x, y + i].color == col)
                        panduan[1] += "1";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[1] = "1" + panduan[1];
            for (int i = 1; i <= 4; i++)
            {
                if (y - i >= 0)
                {
                    if (chess[x, y - i].color == col)
                        panduan[1] = "1" + panduan[1];
                    else
                        break;
                }
                else
                    break;
            }
            //------------竖
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14&& y + i <= 14)
                {
                    if (chess[x + i, y + i].color == col)
                        panduan[2] += "1";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[2] = "1" + panduan[2];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0&& y - i >= 0)
                {
                    if (chess[x - i, y - i].color == col)
                        panduan[2] = "1" + panduan[2];
                    else
                        break;
                }
                else
                    break;
            }
            //------------右斜
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14 && y - i >= 0)
                {
                    if (chess[x + i, y - i].color == col)
                        panduan[3] += "1";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[3] = "1" + panduan[3];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0 && y + i <= 14)
                {
                    if (chess[x - i, y + i].color == col)
                        panduan[3] = "1" + panduan[3];
                    else
                        break;
                }
                else
                    break;
            }
            //------------左斜
            for (int i = 0; i < 4; i++)
            {
                lb.Items.Add(col + panduan[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                if (bijiao("11111", panduan[i]) >= 1)
                {
                    lb.Items.Add(col + "获胜");
                    return true;
                }  
            }
            lb.Items.Add("否");
            lb.Items.Add("---------------");
            return false;
        }
        public int huosi(int x, int y,string col)
        {
            lb.Items.Add(x + " " + y + " " + col + "活四判断");
            int result=0;
            string[] panduan = new string[4];
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14)
                {
                    if (chess[x + i, y].color == col)
                        panduan[0] += "1";
                    else if (chess[x + i, y].color == null)
                        panduan[0] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[0] = "1" + panduan[0];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0)
                {
                    if (chess[x - i, y].color == col)
                        panduan[0] = "1" + panduan[0];
                    else if (chess[x - i, y].color == null)
                        panduan[0] = "0"+ panduan[0];
                    else
                        break;
                }
                else
                    break;
            }
            //------------横
            for (int i = 1; i <= 4; i++)
            {
                if (y + i <= 14)
                {
                    if (chess[x, y + i].color == col)
                        panduan[1] += "1";
                    else if (chess[x, y + i].color == null)
                        panduan[1] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[1] = "1" + panduan[1];
            for (int i = 1; i <= 4; i++)
            {
                if (y - i >= 0)
                {
                    if (chess[x, y - i].color == col)
                        panduan[1] = "1" + panduan[1];
                    else if (chess[x, y - i].color == null)
                        panduan[1] = "0" + panduan[1];
                    else
                        break;
                }
                else
                    break;
            }
            //------------竖
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14&& y + i <= 14)
                {
                    if (chess[x + i, y + i].color == col)
                        panduan[2] += "1";
                    else if (chess[x + i, y + i].color == null)
                        panduan[2] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[2] = "1" + panduan[2];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0 && y - i >= 0)
                {
                    if (chess[x - i, y - i].color == col)
                        panduan[2] = "1" + panduan[2];
                    else if (chess[x - i, y - i].color == null)
                        panduan[2] = "0" + panduan[2];
                    else
                        break;
                }
                else
                    break;
            }
            //------------右斜
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14 && y - i >= 0)
                {
                    if (chess[x + i, y - i].color == col)
                        panduan[3] += "1";
                    else if (chess[x + i, y - i].color == null)
                        panduan[3] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[3] = "1" + panduan[3];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0 && y + i <= 14)
                {
                    if (chess[x - i, y + i].color == col)
                        panduan[3] = "1" + panduan[3];
                    else if (chess[x - i, y + i].color == null)
                        panduan[3] = "0" + panduan[3];
                    else
                        break;
                }
                else
                    break;
            }
            //------------左斜
            for (int i = 0; i < 4; i++)
            {
                lb.Items.Add(col + panduan[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                result += bijiao("011110", panduan[i]);
            }
            lb.Items.Add("活四：" + result);
            lb.Items.Add("---------------");
            return result;

        }
        public int chongsi(int x, int y,string col)
        {
            lb.Items.Add(x + " " + y + " "+col+ "冲四判断");
            int result = 0;
            string[] panduan = new string[4];
            for (int i = 1; i <= 5; i++)
            {
                if (x + i <= 14)
                {
                    if (chess[x + i, y].color == col)
                        panduan[0] += "1";
                    else if (chess[x + i, y].color == null)
                        panduan[0] += "0";
                    else
                    {
                        panduan[0] += "f";
                        break;
                    }
                }
                else
                {
                    panduan[0] += "|";
                    break;
                }
            }
            panduan[0] = "1" + panduan[0];
            for (int i = 1; i <= 5; i++)
            {
                if (x - i >= 0)
                {
                    if (chess[x - i, y].color == col)
                        panduan[0] = "1" + panduan[0];
                    else if (chess[x - i, y].color == null)
                        panduan[0] = "0" + panduan[0];
                    else
                    {
                        panduan[0] = "f" + panduan[0];
                        break;
                    } 
                }
                else
                {
                    panduan[0] = "|" + panduan[0];
                    break;
                }
            }
            //------------横
            for (int i = 1; i <= 5; i++)
            {
                if (y + i <= 14)
                {
                    if (chess[x, y + i].color == col)
                        panduan[1] += "1";
                    else if (chess[x, y + i].color == null)
                        panduan[1] += "0";
                    else
                    {
                        panduan[1] += "f";
                        break;
                    }
                }
                else
                {
                    panduan[1] += "|";
                    break;
                }
            }
            panduan[1] = "1" + panduan[1];
            for (int i = 1; i <= 5; i++)
            {
                if (y - i >= 0)
                {
                    if (chess[x, y - i].color == col)
                        panduan[1] = "1" + panduan[1];
                    else if (chess[x, y - i].color == null)
                        panduan[1] = "0" + panduan[1];
                    else
                    {
                        panduan[1] = "f" + panduan[1];
                        break;
                    }
                }
                else
                {
                    panduan[1] = "|" + panduan[1];
                    break;
                }
            }
            //------------竖
            for (int i = 1; i <= 5; i++)
            {
                if (x + i <= 14&& y + i <= 14)
                {
                    if (chess[x + i, y + i].color == col)
                        panduan[2] += "1";
                    else if (chess[x + i, y + i].color == null)
                        panduan[2] += "0";
                    else
                    {
                        panduan[2] += "f";
                        break;
                    }
                }
                else
                {
                    panduan[2] += "|";
                    break;
                }
            }
            panduan[2] = "1" + panduan[2];
            for (int i = 1; i <= 5; i++)
            {
                if (x - i >= 0&& y - i >= 0)
                {
                    if (chess[x - i, y - i].color == col)
                        panduan[2] = "1" + panduan[2];
                    else if (chess[x - i, y - i].color == null)
                        panduan[2] = "0" + panduan[2];
                    else
                    {
                        panduan[2] = "f" + panduan[2];
                        break;
                    }
                }
                else
                {
                    panduan[2] = "|" + panduan[2];
                    break;
                }
            }
            //------------右斜
            for (int i = 1; i <= 5; i++)
            {
                if (x + i <= 14 && y - i >= 0)
                {
                    if (chess[x + i, y - i].color == col)
                        panduan[3] += "1";
                    else if (chess[x + i, y - i].color == null)
                        panduan[3] += "0";
                    else
                    {
                        panduan[3] += "f";
                        break;
                    }
                }
                else
                {
                    panduan[3] += "|";
                    break;
                }
            }
            panduan[3] = "1" + panduan[3];
            for (int i = 1; i <= 5; i++)
            {
                if (x - i >= 0 && y + i <= 14)
                {
                    if (chess[x - i, y + i].color == col)
                        panduan[3] = "1" + panduan[3];
                    else if (chess[x - i, y + i].color == null)
                        panduan[3] = "0" + panduan[3];
                    else
                    {
                        panduan[3] = "f" + panduan[3];
                        break;
                    }
                }
                else
                {
                    panduan[3] = "|" + panduan[3];
                    break;
                }
            }
            //------------左斜
            for (int i = 0; i < 4; i++)
            {
                lb.Items.Add(col + panduan[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                result += bijiao("|01111|", panduan[i]);
                result += bijiao("f01111|", panduan[i]);
                result += bijiao("001111|", panduan[i]);
                result += bijiao("|11110|", panduan[i]);
                result += bijiao("|11110f", panduan[i]);
                result += bijiao("|111100", panduan[i]);
                //----
                result += bijiao("|11101|", panduan[i]);
                result += bijiao("f11101|", panduan[i]);
                result += bijiao("011101|", panduan[i]);
                result += bijiao("|11101f", panduan[i]);
                result += bijiao("f11101f", panduan[i]);
                result += bijiao("011101f", panduan[i]);
                result += bijiao("|111010", panduan[i]);
                result += bijiao("f111010", panduan[i]);
                result += bijiao("0111010", panduan[i]);
                //----
                result += bijiao("|11011|", panduan[i]);
                result += bijiao("f11011|", panduan[i]);
                result += bijiao("011011|", panduan[i]);
                result += bijiao("|11011f", panduan[i]);
                result += bijiao("f11011f", panduan[i]);
                result += bijiao("011011f", panduan[i]);
                result += bijiao("|110110", panduan[i]);
                result += bijiao("f110110", panduan[i]);
                result += bijiao("0110110", panduan[i]);
                //----
                result += bijiao("|10111|", panduan[i]);
                result += bijiao("f10111|", panduan[i]);
                result += bijiao("010111|", panduan[i]);
                result += bijiao("|10111f", panduan[i]);
                result += bijiao("f10111f", panduan[i]);
                result += bijiao("010111f", panduan[i]);
                result += bijiao("|101110", panduan[i]);
                result += bijiao("f101110", panduan[i]);
                result += bijiao("0101110", panduan[i]);
                //----
            }
            lb.Items.Add("冲四：" + result);
            lb.Items.Add("---------------");
            return result;
        }
        public int huosan (int x, int y,string col)
        {
            lb.Items.Add(x + " " + y + " "+col+ "活三判断");
            int result = 0;
            string[] panduan = new string[4];
            for (int i = 1; i <= 5; i++)
            {
                if (x + i <= 14)
                {
                    if (chess[x + i, y].color == col)
                        panduan[0] += "1";
                    else if (chess[x + i, y].color == null)
                        panduan[0] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[0] = "1" + panduan[0];
            for (int i = 1; i <= 5; i++)
            {
                if (x - i >= 0)
                {
                    if (chess[x - i, y].color == col)
                        panduan[0] = "1" + panduan[0];
                    else if (chess[x - i, y].color == null)
                        panduan[0] = "0" + panduan[0];
                    else
                        break;
                }
                else
                    break;
            }
            //------------横
            for (int i = 1; i <= 5; i++)
            {
                if (y + i <= 14)
                {
                    if (chess[x, y + i].color == col)
                        panduan[1] += "1";
                    else if (chess[x, y + i].color == null)
                        panduan[1] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[1] = "1" + panduan[1];
            for (int i = 1; i <= 5; i++)
            {
                if (y - i >= 0)
                {
                    if (chess[x, y - i].color == col)
                        panduan[1] = "1" + panduan[1];
                    else if (chess[x, y - i].color == null)
                        panduan[1] = "0" + panduan[1];
                    else
                        break;
                }
                else
                    break;
            }
            //------------竖
            for (int i = 1; i <= 5; i++)
            {
                if (x + i <= 14 && y + i <= 14)
                {
                    if (chess[x + i, y + i].color == col)
                        panduan[2] += "1";
                    else if (chess[x + i, y + i].color == null)
                        panduan[2] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[2] = "1" + panduan[2];
            for (int i = 1; i <= 5; i++)
            {
                if (x - i >= 0 && y - i >= 0)
                {
                    if (chess[x - i, y - i].color == col)
                        panduan[2] = "1" + panduan[2];
                    else if (chess[x - i, y - i].color == null)
                        panduan[2] = "0" + panduan[2];
                    else
                        break;
                }
                else
                    break;
            }
            //------------右斜
            for (int i = 1; i <= 5; i++)
            {
                if (x + i <= 14 && y - i >= 0)
                {
                    if (chess[x + i, y - i].color == col)
                        panduan[3] += "1";
                    else if (chess[x + i, y - i].color == null)
                        panduan[3] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[3] = "1" + panduan[3];
            for (int i = 1; i <= 5; i++)
            {
                if (x - i >= 0 && y + i <= 14)
                {
                    if (chess[x - i, y + i].color == col)
                        panduan[3] = "1" + panduan[3];
                    else if (chess[x - i, y + i].color == null)
                        panduan[3] = "0" + panduan[3];
                    else
                        break;
                }
                else
                    break;
            }
            //------------左斜
            for (int i = 0; i < 4; i++)
            {
                lb.Items.Add(chess[x, y].color + panduan[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                if (bijiao("0011100", panduan[i]) > 0)
                    result += 1;
                else if(bijiao("011100", panduan[i])>0)
                    result += 1;
                else if(bijiao("001110", panduan[i])>0)
                    result += 1;
                result += bijiao("010110", panduan[i]);
                result += bijiao("011010", panduan[i]);
            }
            lb.Items.Add("活三：" + result);
            lb.Items.Add("---------------");
            return result;
        }
        public int huoer(int x,int y,string col)
        {
            lb.Items.Add(x + " " + y + " " + col + "活二判断");
            int result = 0;
            string[] panduan = new string[4];
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14)
                {
                    if (chess[x + i, y].color == col)
                        panduan[0] += "1";
                    else if (chess[x + i, y].color == null)
                        panduan[0] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[0] = "1" + panduan[0];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0)
                {
                    if (chess[x - i, y].color == col)
                        panduan[0] = "1" + panduan[0];
                    else if (chess[x - i, y].color == null)
                        panduan[0] = "0" + panduan[0];
                    else
                        break;
                }
                else
                    break;
            }
            //------------横
            for (int i = 1; i <= 4; i++)
            {
                if (y + i <= 14)
                {
                    if (chess[x, y + i].color == col)
                        panduan[1] += "1";
                    else if (chess[x, y + i].color == null)
                        panduan[1] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[1] = "1" + panduan[1];
            for (int i = 1; i <= 4; i++)
            {
                if (y - i >= 0)
                {
                    if (chess[x, y - i].color == col)
                        panduan[1] = "1" + panduan[1];
                    else if (chess[x, y - i].color == null)
                        panduan[1] = "0" + panduan[1];
                    else
                        break;
                }
                else
                    break;
            }
            //------------竖
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14 && y + i <= 14)
                {
                    if (chess[x + i, y + i].color == col)
                        panduan[2] += "1";
                    else if (chess[x + i, y + i].color == null)
                        panduan[2] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[2] = "1" + panduan[2];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0 && y - i >= 0)
                {
                    if (chess[x - i, y - i].color == col)
                        panduan[2] = "1" + panduan[2];
                    else if (chess[x - i, y - i].color == null)
                        panduan[2] = "0" + panduan[2];
                    else
                        break;
                }
                else
                    break;
            }
            //------------右斜
            for (int i = 1; i <= 4; i++)
            {
                if (x + i <= 14 && y - i >= 0)
                {
                    if (chess[x + i, y - i].color == col)
                        panduan[3] += "1";
                    else if (chess[x + i, y - i].color == null)
                        panduan[3] += "0";
                    else
                        break;
                }
                else
                    break;
            }
            panduan[3] = "1" + panduan[3];
            for (int i = 1; i <= 4; i++)
            {
                if (x - i >= 0 && y + i <= 14)
                {
                    if (chess[x - i, y + i].color == col)
                        panduan[3] = "1" + panduan[3];
                    else if (chess[x - i, y + i].color == null)
                        panduan[3] = "0" + panduan[3];
                    else
                        break;
                }
                else
                    break;
            }
            //------------左斜
            for (int i = 0; i < 4; i++)
            {
                lb.Items.Add(chess[x, y].color + panduan[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                if (bijiao("001100", panduan[i]) > 0)
                    result++;
                if (bijiao("001010", panduan[i]) > 0)
                    result++;
                else if (bijiao("010100", panduan[i]) > 0)
                    result++;
            }
            lb.Items.Add("活二：" + result);
            lb.Items.Add("---------------");
            return result;
        }
        public bool sansanjs(int x, int y,string col)
        {
            if (huosan(x, y, col) >= 2)
                return true;
            return false;
        }
        public bool sisijs(int x, int y, string col)
        {
            if ((huosi(x, y, col) + chongsi(x, y, col) >= 2))
                return true;
            return false;
        }
        
    }
}

