using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 雷电
{
    class Suit
    {
        public Image Img
        {
            set; get;
        }
        public int Price
        {
            set; get;
        }
        public bool IfBought
        {
            set; get;
        }

        public Suit(Image img, int p, bool b)
        {
            Img = img;
            Price = p;
            IfBought = b;
        }
    }
}
