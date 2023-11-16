using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace A21智能五子棋04200923
{
    class Chess
    {
        public string color { get; set; }
        public int x { get; set; }
        public int y
        { get; set; }
        public Chess(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.color = null;
        }
        public void clearpicture(PictureBox[,] pbox)
        {
            pbox[x, y].Image = null;
        }
    }
}
