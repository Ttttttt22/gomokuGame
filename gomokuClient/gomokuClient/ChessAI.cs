using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A21智能五子棋04200923
{
    class chessxy
    {
        public int x, y, score;
        public chessxy(int x, int y, int score)
        {
            this.x = x;
            this.y = y;
            this.score = score;
        }

    }
    class ChessAI
    {

        public ChessBoard chessboard;
        public ChessAI(ChessBoard chessboard)
        {
            this.chessboard = chessboard;
        }
        public void blackAI(int level, bool test)
        {
            chessboard.lb.Items.Clear();
            List<chessxy> goxy = new List<chessxy>();
            int[] x = new int[10];
            int k = 10;
            x[0] = 2;
            x[1] = x[0] * k + 1;
            x[2] = x[0] * k + x[1] * k + 1;
            x[3] = x[0] * k + x[1] * k + x[2] * k + 1;
            x[4] = x[0] * k + x[1] * k + x[2] * k + x[3] + 1;
            x[5] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + 1;
            x[6] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + 1;
            x[7] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + x[6] * k + 1;
            x[8] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + x[6] * k + x[7] + 1;
            x[9] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + x[6] * k + x[7] + x[8] * k + 1;
            for (int i = 0; i < 15; i++)//x
            {
                for (int j = 0; j < 15; j++)//y
                {
                    if (chessboard.chess[i, j].color == null)
                    {
                        if (i + 1 <= 14)
                        {
                            if (chessboard.chess[i + 1, j].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i - 1 >= 0)
                        {
                            if (chessboard.chess[i - 1, j].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (j + 1 <= 14)
                        {
                            if (chessboard.chess[i, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (j - 1 >= 0)
                        {
                            if (chessboard.chess[i, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i + 1 <= 14 && j + 1 <= 14)
                        {
                            if (chessboard.chess[i + 1, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            if (chessboard.chess[i - 1, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }

                        }
                        if (i + 1 <= 14 && j - 1 >= 0)
                        {
                            if (chessboard.chess[i + 1, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }

                        }
                        if (i - 1 >= 0 && j + 1 <= 14)
                        {
                            if (chessboard.chess[i - 1, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                    }
                }
            }
            chessboard.lb.Items.Add("---------------");
            if (goxy.Count == 0)
            {
                chessboard.win = "none";
                if(test==false)
                    MessageBox.Show("游戏结束，平局");
                return;
            }
            for (int i = 0; i < goxy.Count; i++)
            {
                int whuosi = chessboard.huosi(goxy[i].x, goxy[i].y, "w"),
                    wchongsi = chessboard.chongsi(goxy[i].x, goxy[i].y, "w"),
                    whuosan = chessboard.huosan(goxy[i].x, goxy[i].y, "w"),
                    whuoer = chessboard.huoer(goxy[i].x, goxy[i].y, "w"),
                    bhuosi = chessboard.huosi(goxy[i].x, goxy[i].y, "b"),
                    bchongsi = chessboard.chongsi(goxy[i].x, goxy[i].y, "b"),
                    bhuosan = chessboard.huosan(goxy[i].x, goxy[i].y, "b"),
                    bhuoer = chessboard.huoer(goxy[i].x, goxy[i].y, "b");

                if (chessboard.judge(goxy[i].x, goxy[i].y, "w"))
                    goxy[i].score += x[8];
                if (whuosi > 0)
                    goxy[i].score += whuosi * x[6];
                if (wchongsi > 0)
                    goxy[i].score += wchongsi * x[4];
                if (whuosan > 0)
                    goxy[i].score += whuosan * x[2];
                if (whuoer > 0)
                    goxy[i].score += whuoer * x[0];
                if (chessboard.judge(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += x[9];
                if (bhuosi > 0 && !chessboard.sisijs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bhuosi * x[7];
                if (bchongsi > 0 && !chessboard.sisijs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bchongsi * x[5];
                if (bhuosan > 0 && !chessboard.sansanjs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bhuosan * x[3];
                if (bhuoer > 0)
                    goxy[i].score += bhuoer * x[1];
            }
            chessboard.lb.Items.Add("goxy:");
            goxy.Sort(delegate (chessxy a, chessxy b) { return -a.score.CompareTo(b.score); });
            for (int i = 0; i < goxy.Count; i++)
            {
                chessboard.lb.Items.Add("x:" + goxy[i].x.ToString() + " y:" + goxy[i].y.ToString() + " score:" + goxy[i].score.ToString());
            }
            chessboard.lb.Items.Add("选择了：");
            Random r = new Random();
            int rr = 0,t=0;
            if (level == 1)
            {
                rr = r.Next(1, 10);
                if (rr < 5 && rr > 2)
                    t = 1;
                else if (rr < 2)
                    t = 3;
                else
                    t = 0;
            }
            else if (level == 2)
            {
                if (rr < 4)
                    t = 1;
            }
            chessboard.lb.Items.Add("第"+t.ToString()+"加"+" x:" + goxy[t].x.ToString() + " y:" + goxy[t].y.ToString() + " score:" + goxy[t].score.ToString());
            chessboard.addchess(goxy[t].x, goxy[t].y, "b",test);
            goxy.Clear();
        }
        public chessxy _blackAI(int level, bool test)
        {
            chessboard.lb.Items.Clear();
            List<chessxy> goxy = new List<chessxy>();
            int[] x = new int[10];
            int k = 10;
            x[0] = 2;
            x[1] = x[0] * k + 1;
            x[2] = x[0] * k + x[1] * k + 1;
            x[3] = x[0] * k + x[1] * k + x[2] * k + 1;
            x[4] = x[0] * k + x[1] * k + x[2] * k + x[3] + 1;
            x[5] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + 1;
            x[6] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + 1;
            x[7] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + x[6] * k + 1;
            x[8] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + x[6] * k + x[7] + 1;
            x[9] = x[0] * k + x[1] * k + x[2] * k + x[3] + x[4] * k + x[5] + x[6] * k + x[7] + x[8] * k + 1;
            for (int i = 0; i < 15; i++)//x
            {
                for (int j = 0; j < 15; j++)//y
                {
                    if (chessboard.chess[i, j].color == null)
                    {
                        if (i + 1 <= 14)
                        {
                            if (chessboard.chess[i + 1, j].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i - 1 >= 0)
                        {
                            if (chessboard.chess[i - 1, j].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (j + 1 <= 14)
                        {
                            if (chessboard.chess[i, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (j - 1 >= 0)
                        {
                            if (chessboard.chess[i, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i + 1 <= 14 && j + 1 <= 14)
                        {
                            if (chessboard.chess[i + 1, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            if (chessboard.chess[i - 1, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }

                        }
                        if (i + 1 <= 14 && j - 1 >= 0)
                        {
                            if (chessboard.chess[i + 1, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }

                        }
                        if (i - 1 >= 0 && j + 1 <= 14)
                        {
                            if (chessboard.chess[i - 1, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                    }
                }
            }
            if (goxy.Count == 0)
            {
                chessboard.win = "none";
                if (test == false)
                    MessageBox.Show("游戏结束，平局");
                return new chessxy(0,0,0);
            }
            chessboard.lb.Items.Add("---------------");
            for (int i = 0; i < goxy.Count; i++)
            {
                int whuosi = chessboard.huosi(goxy[i].x, goxy[i].y, "w"),
                    wchongsi = chessboard.chongsi(goxy[i].x, goxy[i].y, "w"),
                    whuosan = chessboard.huosan(goxy[i].x, goxy[i].y, "w"),
                    whuoer = chessboard.huoer(goxy[i].x, goxy[i].y, "w"),
                    bhuosi = chessboard.huosi(goxy[i].x, goxy[i].y, "b"),
                    bchongsi = chessboard.chongsi(goxy[i].x, goxy[i].y, "b"),
                    bhuosan = chessboard.huosan(goxy[i].x, goxy[i].y, "b"),
                    bhuoer = chessboard.huoer(goxy[i].x, goxy[i].y, "b");

                if (chessboard.judge(goxy[i].x, goxy[i].y, "w"))
                    goxy[i].score += x[8];
                if (whuosi > 0)
                    goxy[i].score += whuosi * x[6];
                if (wchongsi > 0)
                    goxy[i].score += wchongsi * x[4];
                if (whuosan > 0)
                    goxy[i].score += whuosan * x[2];
                if (whuoer > 0)
                    goxy[i].score += whuoer * x[0];
                if (chessboard.judge(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += x[9];
                if (bhuosi > 0 && !chessboard.sisijs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bhuosi * x[7];
                if (bchongsi > 0 && !chessboard.sisijs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bchongsi * x[5];
                if (bhuosan > 0 && !chessboard.sansanjs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bhuosan * x[3];
                if (bhuoer > 0)
                    goxy[i].score += bhuoer * x[1];
            }
            chessboard.lb.Items.Add("goxy:");
            goxy.Sort(delegate (chessxy a, chessxy b) { return -a.score.CompareTo(b.score); });
            for (int i = 0; i < goxy.Count; i++)
            {
                chessboard.lb.Items.Add("x:" + goxy[i].x.ToString() + " y:" + goxy[i].y.ToString() + " score:" + goxy[i].score.ToString());
            }
            chessboard.lb.Items.Add("选择了：");
            Random r = new Random();
            int rr = 0, t = 0;
            if (level == 1)
            {
                rr = r.Next(1, 10);
                if (rr < 5 && rr > 2)
                    t = 1;
                else if (rr < 2)
                    t = 3;
                else
                    t = 0;
            }
            else if (level == 2)
            {
                if (rr < 4)
                    t = 1;
            }
            chessboard.lb.Items.Add("第" + t.ToString() + "加" + " x:" + goxy[t].x.ToString() + " y:" + goxy[t].y.ToString() + " score:" + goxy[t].score.ToString());
            chessboard.addchess(goxy[t].x, goxy[t].y, "b", test);
            chessxy rt=new chessxy(goxy[0].x, goxy[0].y, goxy[0].score);
            goxy.Clear();
            return rt;
        }
        public void whiteAI(int level, int _x, int _y, bool test)
        {
            if (chessboard.sansanjs(_x, _y, "b"))
            {
                if (test == false)
                    MessageBox.Show("白方指出黑方"+ _x.ToString()+" "+ _y.ToString()+"违反三三禁手规定，白方获胜");
                chessboard.win = "white";
                return;
            }
            if (chessboard.sisijs(_x, _y, "b"))
            {
                if (test == false)
                    MessageBox.Show("白方指出黑方"+ _x.ToString()+" "+ _y.ToString()+"违反四四禁手规定，白方获胜");
                chessboard.win = "white";
                return;
            }
            List<chessxy> goxy = new List<chessxy>();
            int[] x = new int[10];
            x[0] = 2;
            int k = 10;
            x[1] = x[0] * k + 1;
            x[2] = x[0] * k + x[1] * k + 1;
            x[3] = x[0] * k + x[1] * k + x[2] + 1;
            x[4] = x[0] * k + x[1] * k + x[2] + x[3] * k + 1;
            x[5] = x[0] * k + x[1] * k + x[2] + x[3] * k + x[4] + 1;
            x[6] = x[0] * k + x[1] * k + x[2] + x[3] * k + x[4] + x[5] * k + 1;
            x[7] = x[0] * k + x[1] * k + x[2] + x[3] * k + x[4] + x[5] * k + x[6] + 1;
            x[8] = x[0] * k + x[1] * k + x[2] + x[3] * k + x[4] + x[5] * k + x[6] + x[7] * k + 1;
            x[9] = x[0] * k + x[1] * k + x[2] + x[3] * k + x[4] + x[5] * k + x[6] + x[7] * k + x[8] + 1;
            for (int i = 0; i < 15; i++)//x
            {
                for (int j = 0; j < 15; j++)//y
                {
                    if (chessboard.chess[i, j].color == null)
                    {
                        if (i + 1 <= 14)
                        {
                            if (chessboard.chess[i + 1, j].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i - 1 >= 0)
                        {
                            if (chessboard.chess[i - 1, j].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (j + 1 <= 14)
                        {
                            if (chessboard.chess[i, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (j - 1 >= 0)
                        {
                            if (chessboard.chess[i, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i + 1 <= 14 && j + 1 <= 14)
                        {
                            if (chessboard.chess[i + 1, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            if (chessboard.chess[i - 1, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }

                        }
                        if (i + 1 <= 14 && j - 1 >= 0)
                        {
                            if (chessboard.chess[i + 1, j - 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }

                        }
                        if (i - 1 >= 0 && j + 1 <= 14)
                        {
                            if (chessboard.chess[i - 1, j + 1].color != null)
                            {
                                chessboard.lb.Items.Add("x:" + i.ToString() + " y:" + j.ToString() + " score:" + 1);
                                goxy.Add(new chessxy(i, j, 1));
                                continue;
                            }
                        }
                    }
                }
            }
            if (goxy.Count == 0)
            {
                chessboard.win = "none";
                if (test == false)
                    MessageBox.Show("游戏结束，平局");
                return;
            }
            chessboard.lb.Items.Add("---------------");
            for (int i = 0; i < goxy.Count; i++)
            {
                int bhuosi = chessboard.huosi(goxy[i].x, goxy[i].y, "b"),
                    bchongsi = chessboard.chongsi(goxy[i].x, goxy[i].y, "b"),
                    bhuosan = chessboard.huosan(goxy[i].x, goxy[i].y, "b"),
                    bhuoer = chessboard.huoer(goxy[i].x, goxy[i].y, "b"),
                    whuosi = chessboard.huosi(goxy[i].x, goxy[i].y, "w"),
                    wchongsi = chessboard.chongsi(goxy[i].x, goxy[i].y, "w"),
                    whuosan = chessboard.huosan(goxy[i].x, goxy[i].y, "w"),
                    whuoer = chessboard.huoer(goxy[i].x, goxy[i].y, "w");
                if (chessboard.judge(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += x[8];
                if (bhuosi > 0 && !chessboard.sisijs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bhuosi * x[6];
                if (bchongsi > 0 && !chessboard.sisijs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bchongsi * x[4];
                if (bhuosan > 0 && !chessboard.sansanjs(goxy[i].x, goxy[i].y, "b"))
                    goxy[i].score += bhuosan * x[2];
                if (bhuoer > 0)
                    goxy[i].score += bhuoer * x[0];
                if (chessboard.judge(goxy[i].x, goxy[i].y, "w"))
                    goxy[i].score += x[9];
                if (whuosi > 0)
                    goxy[i].score += whuosi * x[7];
                if (wchongsi > 0)
                    goxy[i].score += wchongsi * x[5];
                if (whuosan > 0)
                    goxy[i].score += whuosan * x[3];
                if (whuoer > 0)
                    goxy[i].score += whuoer * x[1];
            }
            goxy.Sort(delegate (chessxy a, chessxy b) { return -a.score.CompareTo(b.score); });
            for (int i = 0; i < goxy.Count; i++)
            {
                chessboard.lb.Items.Add("x:" + goxy[i].x.ToString() + " y:" + goxy[i].y.ToString() + " score:" + goxy[i].score.ToString());
            }
            chessboard.lb.Items.Add("选择了：");
            Random r = new Random();
            int rr = 0, t = 0;
            if (level == 1)
            {
                rr = r.Next(1, 10);
                if (rr < 5 && rr > 2)
                    t = 1;
                else if (rr < 2)
                    t = 3;
                else
                    t = 0;
            }
            else if (level == 2)
            {
                if (rr < 4)
                    t = 1;
            }
            chessboard.lb.Items.Add("第" + t.ToString() + "加" + " x:" + goxy[t].x.ToString() + " y:" + goxy[t].y.ToString() + " score:" + goxy[t].score.ToString());
            chessboard.addchess(goxy[t].x, goxy[t].y, "w", test);
            goxy.Clear();
        }
        public string AItest(int bk, int wk, int interval)
        {
            chessboard.lb.Items.Clear();
            Random r = new Random();
            int x = r.Next(4, 10), y = r.Next(4, 10);
            chessboard.addchess(x, y, "b", true);
            chessxy t;
            whiteAI(wk, x, y,true);
            while (true)
            {
                if (chessboard.win == null)
                {
                    t = _blackAI(bk, true);
                    x = t.x;
                    y = t.y;
                }
                else
                    break;
                Task.Delay(interval);
                if (chessboard.win == null)
                    whiteAI(wk, x, y, true);
                else
                    break;
            }
            return chessboard.win;
        }
    }
}
