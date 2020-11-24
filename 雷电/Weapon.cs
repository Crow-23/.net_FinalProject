using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace 雷电
{
    [Serializable]
    class Weapon
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
            public int Power
            {
                set; get;
            }

            public Weapon(Image img, int pr, bool b, int po)
            {
                Img = img;
                IfBought = b;
                Price = pr;
                Power = po;
            }

    }
}
