using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace A21智能五子棋04200923
{
    public partial class Mainform : Form
    {
        ChessBoard chessboard;
        ChessAI cAI;
        string col = null;
        bool test = false;
        int level = 1;
        public Mainform()
        {
            InitializeComponent();
            PictureBox[,] chesspbox = {
                { pbox0x0, pbox0x1, pbox0x2, pbox0x3, pbox0x4, pbox0x5, pbox0x6, pbox0x7, pbox0x8, pbox0x9, pbox0x10, pbox0x11, pbox0x12, pbox0x13, pbox0x14 },
                { pbox1x0, pbox1x1, pbox1x2, pbox1x3, pbox1x4, pbox1x5, pbox1x6, pbox1x7, pbox1x8, pbox1x9, pbox1x10, pbox1x11, pbox1x12, pbox1x13, pbox1x14 },
                { pbox2x0, pbox2x1, pbox2x2, pbox2x3, pbox2x4, pbox2x5, pbox2x6, pbox2x7, pbox2x8, pbox2x9, pbox2x10, pbox2x11, pbox2x12, pbox2x13, pbox2x14 },
                { pbox3x0, pbox3x1, pbox3x2, pbox3x3, pbox3x4, pbox3x5, pbox3x6, pbox3x7, pbox3x8, pbox3x9, pbox3x10, pbox3x11, pbox3x12, pbox3x13, pbox3x14 },
                { pbox4x0, pbox4x1, pbox4x2, pbox4x3, pbox4x4, pbox4x5, pbox4x6, pbox4x7, pbox4x8, pbox4x9, pbox4x10, pbox4x11, pbox4x12, pbox4x13, pbox4x14 },
                { pbox5x0, pbox5x1, pbox5x2, pbox5x3, pbox5x4, pbox5x5, pbox5x6, pbox5x7, pbox5x8, pbox5x9, pbox5x10, pbox5x11, pbox5x12, pbox5x13, pbox5x14 },
                { pbox6x0, pbox6x1, pbox6x2, pbox6x3, pbox6x4, pbox6x5, pbox6x6, pbox6x7, pbox6x8, pbox6x9, pbox6x10, pbox6x11, pbox6x12, pbox6x13, pbox6x14 },
                { pbox7x0, pbox7x1, pbox7x2, pbox7x3, pbox7x4, pbox7x5, pbox7x6, pbox7x7, pbox7x8, pbox7x9, pbox7x10, pbox7x11, pbox7x12, pbox7x13, pbox7x14 },
                { pbox8x0, pbox8x1, pbox8x2, pbox8x3, pbox8x4, pbox8x5, pbox8x6, pbox8x7, pbox8x8, pbox8x9, pbox8x10, pbox8x11, pbox8x12, pbox8x13, pbox8x14 },
                { pbox9x0, pbox9x1, pbox9x2, pbox9x3, pbox9x4, pbox9x5, pbox9x6, pbox9x7, pbox9x8, pbox9x9, pbox9x10, pbox9x11, pbox9x12, pbox9x13, pbox9x14 },
                { pbox10x0, pbox10x1, pbox10x2, pbox10x3, pbox10x4, pbox10x5, pbox10x6, pbox10x7, pbox10x8, pbox10x9, pbox10x10, pbox10x11, pbox10x12, pbox10x13, pbox10x14 },
                { pbox11x0, pbox11x1, pbox11x2, pbox11x3, pbox11x4, pbox11x5, pbox11x6, pbox11x7, pbox11x8, pbox11x9, pbox11x10, pbox11x11, pbox11x12, pbox11x13, pbox11x14 },
                { pbox12x0, pbox12x1, pbox12x2, pbox12x3, pbox12x4, pbox12x5, pbox12x6, pbox12x7, pbox12x8, pbox3x9, pbox12x10, pbox12x11, pbox12x12, pbox12x13, pbox12x14 },
                { pbox13x0, pbox13x1, pbox13x2, pbox13x3, pbox13x4, pbox13x5, pbox13x6, pbox13x7, pbox13x8, pbox13x9, pbox13x10, pbox13x11, pbox13x12, pbox13x13, pbox13x14 },
                { pbox14x0, pbox14x1, pbox14x2, pbox14x3, pbox14x4, pbox14x5, pbox14x6, pbox14x7, pbox14x8, pbox14x9, pbox14x10, pbox14x11, pbox14x12, pbox14x13, pbox14x14 }};
            ListBox listBox = new ListBox();
            TextBox textBoxinterval = new TextBox();
            TextBox textBoxtime = new TextBox();
            TextBox textBoxbk = new TextBox();
            TextBox textBoxwk = new TextBox();
            this.Size = new Size(862, 935);
        }
        private void pbox_Click(object sender, EventArgs e)
        {
            PictureBox[,] chesspbox = {
                { pbox0x0, pbox0x1, pbox0x2, pbox0x3, pbox0x4, pbox0x5, pbox0x6, pbox0x7, pbox0x8, pbox0x9, pbox0x10, pbox0x11, pbox0x12, pbox0x13, pbox0x14 },
                { pbox1x0, pbox1x1, pbox1x2, pbox1x3, pbox1x4, pbox1x5, pbox1x6, pbox1x7, pbox1x8, pbox1x9, pbox1x10, pbox1x11, pbox1x12, pbox1x13, pbox1x14 },
                { pbox2x0, pbox2x1, pbox2x2, pbox2x3, pbox2x4, pbox2x5, pbox2x6, pbox2x7, pbox2x8, pbox2x9, pbox2x10, pbox2x11, pbox2x12, pbox2x13, pbox2x14 },
                { pbox3x0, pbox3x1, pbox3x2, pbox3x3, pbox3x4, pbox3x5, pbox3x6, pbox3x7, pbox3x8, pbox3x9, pbox3x10, pbox3x11, pbox3x12, pbox3x13, pbox3x14 },
                { pbox4x0, pbox4x1, pbox4x2, pbox4x3, pbox4x4, pbox4x5, pbox4x6, pbox4x7, pbox4x8, pbox4x9, pbox4x10, pbox4x11, pbox4x12, pbox4x13, pbox4x14 },
                { pbox5x0, pbox5x1, pbox5x2, pbox5x3, pbox5x4, pbox5x5, pbox5x6, pbox5x7, pbox5x8, pbox5x9, pbox5x10, pbox5x11, pbox5x12, pbox5x13, pbox5x14 },
                { pbox6x0, pbox6x1, pbox6x2, pbox6x3, pbox6x4, pbox6x5, pbox6x6, pbox6x7, pbox6x8, pbox6x9, pbox6x10, pbox6x11, pbox6x12, pbox6x13, pbox6x14 },
                { pbox7x0, pbox7x1, pbox7x2, pbox7x3, pbox7x4, pbox7x5, pbox7x6, pbox7x7, pbox7x8, pbox7x9, pbox7x10, pbox7x11, pbox7x12, pbox7x13, pbox7x14 },
                { pbox8x0, pbox8x1, pbox8x2, pbox8x3, pbox8x4, pbox8x5, pbox8x6, pbox8x7, pbox8x8, pbox8x9, pbox8x10, pbox8x11, pbox8x12, pbox8x13, pbox8x14 },
                { pbox9x0, pbox9x1, pbox9x2, pbox9x3, pbox9x4, pbox9x5, pbox9x6, pbox9x7, pbox9x8, pbox9x9, pbox9x10, pbox9x11, pbox9x12, pbox9x13, pbox9x14 },
                { pbox10x0, pbox10x1, pbox10x2, pbox10x3, pbox10x4, pbox10x5, pbox10x6, pbox10x7, pbox10x8, pbox10x9, pbox10x10, pbox10x11, pbox10x12, pbox10x13, pbox10x14 },
                { pbox11x0, pbox11x1, pbox11x2, pbox11x3, pbox11x4, pbox11x5, pbox11x6, pbox11x7, pbox11x8, pbox11x9, pbox11x10, pbox11x11, pbox11x12, pbox11x13, pbox11x14 },
                { pbox12x0, pbox12x1, pbox12x2, pbox12x3, pbox12x4, pbox12x5, pbox12x6, pbox12x7, pbox12x8, pbox3x9, pbox12x10, pbox12x11, pbox12x12, pbox12x13, pbox12x14 },
                { pbox13x0, pbox13x1, pbox13x2, pbox13x3, pbox13x4, pbox13x5, pbox13x6, pbox13x7, pbox13x8, pbox13x9, pbox13x10, pbox13x11, pbox13x12, pbox13x13, pbox13x14 },
                { pbox14x0, pbox14x1, pbox14x2, pbox14x3, pbox14x4, pbox14x5, pbox14x6, pbox14x7, pbox14x8, pbox14x9, pbox14x10, pbox14x11, pbox14x12, pbox14x13, pbox14x14 }};
            PictureBox p = sender as PictureBox;
            string s = p.Name;
            string[] a = s.Split('x');
            if (chessboard == null)
            {
                MessageBox.Show("请先选择游戏模式");
                return;
            }
            if (chessboard.win != null)//判断游戏是否开始
            {
                chessboard.lb.Items.Clear();
                chessboard.lb.Items.Add("游戏已经结束，请开始下一回合游戏");
                return;
            }
            if (col == "b" && chessboard.round % 2 == 1)
            {
                if (chessboard.chess[int.Parse(a[1]), int.Parse(a[2])].color == null)
                {
                    chessboard.addchess(int.Parse(a[1]), int.Parse(a[2]), col, false);
                    //Thread.Sleep(2000);
                    //MessageBox.Show("............");
                    if (chessboard.win == null)
                        cAI.whiteAI(level, int.Parse(a[1]), int.Parse(a[2]), false);
                    //chessboard.lb.Items.Clear();
                }
                else
                    MessageBox.Show("当前位置不可落子");
                //col = "w";//
                return;
            }
            else if (col == "w" && chessboard.round % 2 == 0)
            {
                if (chessboard.chess[int.Parse(a[1]), int.Parse(a[2])].color == null)
                {
                    chessboard.addchess(int.Parse(a[1]), int.Parse(a[2]), col, false);
                    Task.Delay(3000);
                    if (chessboard.win == null)
                        cAI.blackAI(level, false);
                    //chessboard.lb.Items.Clear();
                }
                else
                    MessageBox.Show("当前位置不可落子");
                //col = "b";//
                return;
            }
            chessboard.lb.Items.Clear();
            chessboard.lb.Items.Add("无法选择");
            //chessboard.round++;//

        }

        private void 新游戏先手黑棋ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chessboard != null)
            {
                chessboard.resetpbox();
            }
            PictureBox[,] chesspbox = {
                { pbox0x0, pbox0x1, pbox0x2, pbox0x3, pbox0x4, pbox0x5, pbox0x6, pbox0x7, pbox0x8, pbox0x9, pbox0x10, pbox0x11, pbox0x12, pbox0x13, pbox0x14 },
                { pbox1x0, pbox1x1, pbox1x2, pbox1x3, pbox1x4, pbox1x5, pbox1x6, pbox1x7, pbox1x8, pbox1x9, pbox1x10, pbox1x11, pbox1x12, pbox1x13, pbox1x14 },
                { pbox2x0, pbox2x1, pbox2x2, pbox2x3, pbox2x4, pbox2x5, pbox2x6, pbox2x7, pbox2x8, pbox2x9, pbox2x10, pbox2x11, pbox2x12, pbox2x13, pbox2x14 },
                { pbox3x0, pbox3x1, pbox3x2, pbox3x3, pbox3x4, pbox3x5, pbox3x6, pbox3x7, pbox3x8, pbox3x9, pbox3x10, pbox3x11, pbox3x12, pbox3x13, pbox3x14 },
                { pbox4x0, pbox4x1, pbox4x2, pbox4x3, pbox4x4, pbox4x5, pbox4x6, pbox4x7, pbox4x8, pbox4x9, pbox4x10, pbox4x11, pbox4x12, pbox4x13, pbox4x14 },
                { pbox5x0, pbox5x1, pbox5x2, pbox5x3, pbox5x4, pbox5x5, pbox5x6, pbox5x7, pbox5x8, pbox5x9, pbox5x10, pbox5x11, pbox5x12, pbox5x13, pbox5x14 },
                { pbox6x0, pbox6x1, pbox6x2, pbox6x3, pbox6x4, pbox6x5, pbox6x6, pbox6x7, pbox6x8, pbox6x9, pbox6x10, pbox6x11, pbox6x12, pbox6x13, pbox6x14 },
                { pbox7x0, pbox7x1, pbox7x2, pbox7x3, pbox7x4, pbox7x5, pbox7x6, pbox7x7, pbox7x8, pbox7x9, pbox7x10, pbox7x11, pbox7x12, pbox7x13, pbox7x14 },
                { pbox8x0, pbox8x1, pbox8x2, pbox8x3, pbox8x4, pbox8x5, pbox8x6, pbox8x7, pbox8x8, pbox8x9, pbox8x10, pbox8x11, pbox8x12, pbox8x13, pbox8x14 },
                { pbox9x0, pbox9x1, pbox9x2, pbox9x3, pbox9x4, pbox9x5, pbox9x6, pbox9x7, pbox9x8, pbox9x9, pbox9x10, pbox9x11, pbox9x12, pbox9x13, pbox9x14 },
                { pbox10x0, pbox10x1, pbox10x2, pbox10x3, pbox10x4, pbox10x5, pbox10x6, pbox10x7, pbox10x8, pbox10x9, pbox10x10, pbox10x11, pbox10x12, pbox10x13, pbox10x14 },
                { pbox11x0, pbox11x1, pbox11x2, pbox11x3, pbox11x4, pbox11x5, pbox11x6, pbox11x7, pbox11x8, pbox11x9, pbox11x10, pbox11x11, pbox11x12, pbox11x13, pbox11x14 },
                { pbox12x0, pbox12x1, pbox12x2, pbox12x3, pbox12x4, pbox12x5, pbox12x6, pbox12x7, pbox12x8, pbox12x9, pbox12x10, pbox12x11, pbox12x12, pbox12x13, pbox12x14 },
                { pbox13x0, pbox13x1, pbox13x2, pbox13x3, pbox13x4, pbox13x5, pbox13x6, pbox13x7, pbox13x8, pbox13x9, pbox13x10, pbox13x11, pbox13x12, pbox13x13, pbox13x14 },
                { pbox14x0, pbox14x1, pbox14x2, pbox14x3, pbox14x4, pbox14x5, pbox14x6, pbox14x7, pbox14x8, pbox14x9, pbox14x10, pbox14x11, pbox14x12, pbox14x13, pbox14x14 }};
            chessboard = new ChessBoard(chesspbox, listBox);
            cAI = new ChessAI(chessboard);
            col = "b";
        }
        private void 新游戏后手白棋ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chessboard != null)
            {
                chessboard.resetpbox();
            }
            PictureBox[,] chesspbox = {
                { pbox0x0, pbox0x1, pbox0x2, pbox0x3, pbox0x4, pbox0x5, pbox0x6, pbox0x7, pbox0x8, pbox0x9, pbox0x10, pbox0x11, pbox0x12, pbox0x13, pbox0x14 },
                { pbox1x0, pbox1x1, pbox1x2, pbox1x3, pbox1x4, pbox1x5, pbox1x6, pbox1x7, pbox1x8, pbox1x9, pbox1x10, pbox1x11, pbox1x12, pbox1x13, pbox1x14 },
                { pbox2x0, pbox2x1, pbox2x2, pbox2x3, pbox2x4, pbox2x5, pbox2x6, pbox2x7, pbox2x8, pbox2x9, pbox2x10, pbox2x11, pbox2x12, pbox2x13, pbox2x14 },
                { pbox3x0, pbox3x1, pbox3x2, pbox3x3, pbox3x4, pbox3x5, pbox3x6, pbox3x7, pbox3x8, pbox3x9, pbox3x10, pbox3x11, pbox3x12, pbox3x13, pbox3x14 },
                { pbox4x0, pbox4x1, pbox4x2, pbox4x3, pbox4x4, pbox4x5, pbox4x6, pbox4x7, pbox4x8, pbox4x9, pbox4x10, pbox4x11, pbox4x12, pbox4x13, pbox4x14 },
                { pbox5x0, pbox5x1, pbox5x2, pbox5x3, pbox5x4, pbox5x5, pbox5x6, pbox5x7, pbox5x8, pbox5x9, pbox5x10, pbox5x11, pbox5x12, pbox5x13, pbox5x14 },
                { pbox6x0, pbox6x1, pbox6x2, pbox6x3, pbox6x4, pbox6x5, pbox6x6, pbox6x7, pbox6x8, pbox6x9, pbox6x10, pbox6x11, pbox6x12, pbox6x13, pbox6x14 },
                { pbox7x0, pbox7x1, pbox7x2, pbox7x3, pbox7x4, pbox7x5, pbox7x6, pbox7x7, pbox7x8, pbox7x9, pbox7x10, pbox7x11, pbox7x12, pbox7x13, pbox7x14 },
                { pbox8x0, pbox8x1, pbox8x2, pbox8x3, pbox8x4, pbox8x5, pbox8x6, pbox8x7, pbox8x8, pbox8x9, pbox8x10, pbox8x11, pbox8x12, pbox8x13, pbox8x14 },
                { pbox9x0, pbox9x1, pbox9x2, pbox9x3, pbox9x4, pbox9x5, pbox9x6, pbox9x7, pbox9x8, pbox9x9, pbox9x10, pbox9x11, pbox9x12, pbox9x13, pbox9x14 },
                { pbox10x0, pbox10x1, pbox10x2, pbox10x3, pbox10x4, pbox10x5, pbox10x6, pbox10x7, pbox10x8, pbox10x9, pbox10x10, pbox10x11, pbox10x12, pbox10x13, pbox10x14 },
                { pbox11x0, pbox11x1, pbox11x2, pbox11x3, pbox11x4, pbox11x5, pbox11x6, pbox11x7, pbox11x8, pbox11x9, pbox11x10, pbox11x11, pbox11x12, pbox11x13, pbox11x14 },
                { pbox12x0, pbox12x1, pbox12x2, pbox12x3, pbox12x4, pbox12x5, pbox12x6, pbox12x7, pbox12x8, pbox12x9, pbox12x10, pbox12x11, pbox12x12, pbox12x13, pbox12x14 },
                { pbox13x0, pbox13x1, pbox13x2, pbox13x3, pbox13x4, pbox13x5, pbox13x6, pbox13x7, pbox13x8, pbox13x9, pbox13x10, pbox13x11, pbox13x12, pbox13x13, pbox13x14 },
                { pbox14x0, pbox14x1, pbox14x2, pbox14x3, pbox14x4, pbox14x5, pbox14x6, pbox14x7, pbox14x8, pbox14x9, pbox14x10, pbox14x11, pbox14x12, pbox14x13, pbox14x14 }};
            chessboard = new ChessBoard(chesspbox, listBox);
            cAI = new ChessAI(chessboard);
            col = "w";
            Random r = new Random();
            chessboard.addchess(r.Next(4, 10), r.Next(4, 10), "b", false);
        }
        private void begin_Click(object sender, EventArgs e)
        {
            int time, tr, bk, wk;
            if (int.TryParse(textBoxtime.Text, out time) == false)
            {
                MessageBox.Show("输入的重复次数有误，请重新输入");
                textBoxtime.Clear();
                return;
            }
            if (int.TryParse(textBoxinterval.Text, out tr) == false)
            {
                MessageBox.Show("输入的等待时长有误，请重新输入");
                textBoxinterval.Clear();
                return;
            }
            if (int.TryParse(textBoxbk.Text, out bk) == false)
            {
                MessageBox.Show("输入的黑棋难度等级有误，请重新输入");
                textBoxbk.Clear();
                return;
            }
            if (int.TryParse(textBoxwk.Text, out wk) == false)
            {
                MessageBox.Show("输入的白棋难度等级有误，请重新输入");
                textBoxwk.Clear();
                return;
            }
            string winer = null;
            int bw = 0, ww = 0, ping = 0;
            PictureBox[,] chesspbox = {
                { pbox0x0, pbox0x1, pbox0x2, pbox0x3, pbox0x4, pbox0x5, pbox0x6, pbox0x7, pbox0x8, pbox0x9, pbox0x10, pbox0x11, pbox0x12, pbox0x13, pbox0x14 },
                { pbox1x0, pbox1x1, pbox1x2, pbox1x3, pbox1x4, pbox1x5, pbox1x6, pbox1x7, pbox1x8, pbox1x9, pbox1x10, pbox1x11, pbox1x12, pbox1x13, pbox1x14 },
                { pbox2x0, pbox2x1, pbox2x2, pbox2x3, pbox2x4, pbox2x5, pbox2x6, pbox2x7, pbox2x8, pbox2x9, pbox2x10, pbox2x11, pbox2x12, pbox2x13, pbox2x14 },
                { pbox3x0, pbox3x1, pbox3x2, pbox3x3, pbox3x4, pbox3x5, pbox3x6, pbox3x7, pbox3x8, pbox3x9, pbox3x10, pbox3x11, pbox3x12, pbox3x13, pbox3x14 },
                { pbox4x0, pbox4x1, pbox4x2, pbox4x3, pbox4x4, pbox4x5, pbox4x6, pbox4x7, pbox4x8, pbox4x9, pbox4x10, pbox4x11, pbox4x12, pbox4x13, pbox4x14 },
                { pbox5x0, pbox5x1, pbox5x2, pbox5x3, pbox5x4, pbox5x5, pbox5x6, pbox5x7, pbox5x8, pbox5x9, pbox5x10, pbox5x11, pbox5x12, pbox5x13, pbox5x14 },
                { pbox6x0, pbox6x1, pbox6x2, pbox6x3, pbox6x4, pbox6x5, pbox6x6, pbox6x7, pbox6x8, pbox6x9, pbox6x10, pbox6x11, pbox6x12, pbox6x13, pbox6x14 },
                { pbox7x0, pbox7x1, pbox7x2, pbox7x3, pbox7x4, pbox7x5, pbox7x6, pbox7x7, pbox7x8, pbox7x9, pbox7x10, pbox7x11, pbox7x12, pbox7x13, pbox7x14 },
                { pbox8x0, pbox8x1, pbox8x2, pbox8x3, pbox8x4, pbox8x5, pbox8x6, pbox8x7, pbox8x8, pbox8x9, pbox8x10, pbox8x11, pbox8x12, pbox8x13, pbox8x14 },
                { pbox9x0, pbox9x1, pbox9x2, pbox9x3, pbox9x4, pbox9x5, pbox9x6, pbox9x7, pbox9x8, pbox9x9, pbox9x10, pbox9x11, pbox9x12, pbox9x13, pbox9x14 },
                { pbox10x0, pbox10x1, pbox10x2, pbox10x3, pbox10x4, pbox10x5, pbox10x6, pbox10x7, pbox10x8, pbox10x9, pbox10x10, pbox10x11, pbox10x12, pbox10x13, pbox10x14 },
                { pbox11x0, pbox11x1, pbox11x2, pbox11x3, pbox11x4, pbox11x5, pbox11x6, pbox11x7, pbox11x8, pbox11x9, pbox11x10, pbox11x11, pbox11x12, pbox11x13, pbox11x14 },
                { pbox12x0, pbox12x1, pbox12x2, pbox12x3, pbox12x4, pbox12x5, pbox12x6, pbox12x7, pbox12x8, pbox12x9, pbox12x10, pbox12x11, pbox12x12, pbox12x13, pbox12x14 },
                { pbox13x0, pbox13x1, pbox13x2, pbox13x3, pbox13x4, pbox13x5, pbox13x6, pbox13x7, pbox13x8, pbox13x9, pbox13x10, pbox13x11, pbox13x12, pbox13x13, pbox13x14 },
                { pbox14x0, pbox14x1, pbox14x2, pbox14x3, pbox14x4, pbox14x5, pbox14x6, pbox14x7, pbox14x8, pbox14x9, pbox14x10, pbox14x11, pbox14x12, pbox14x13, pbox14x14 }};
            for (int i = 0; i < time; i++)
            {

                chessboard = new ChessBoard(chesspbox, listBox);
                cAI = new ChessAI(chessboard);
                winer = cAI.AItest(bk, wk, tr);
                if (winer == "black")
                    bw++;
                else if (winer == "white")
                    ww++;
                else
                    ping++;
                winer = null;
                chessboard.resetpbox();
                //MessageBox.Show("第" + (i+1).ToString()+"次测试结束");
            }
            MessageBox.Show("bw:" + bw.ToString() + " ww:" + ww.ToString() + " ping:" + ping.ToString());
        }

        private void 调试模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (test == false)
            {
                this.Size = new Size(1062, 935);
                test = true;
                调试模式ToolStripMenuItem.Checked = true;
            }
            else
            {
                this.Size = new Size(862, 935);
                test = false;
                调试模式ToolStripMenuItem.Checked = false;
            }
        }

        private void 简单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            一般ToolStripMenuItem.Checked = false;
            困难ToolStripMenuItem.Checked = false;
            level = 1;
        }

        private void 一般ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            困难ToolStripMenuItem.Checked = false;
            简单ToolStripMenuItem.Checked = false;
            level = 2;
        }

        private void 困难ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            一般ToolStripMenuItem.Checked = false;
            简单ToolStripMenuItem.Checked = false;
            level = 3;
        }


    }
}
