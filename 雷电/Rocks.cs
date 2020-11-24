using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using 雷电.Properties;

namespace 雷电
{
    class Rocks:GameObj
    {
        public int RockType
        {
            set;get;
        }
        public Rocks(int type):base(GetImage(type).Width,GetImage(type).Height,5)
        {
            RockType = type;
        }

        public static Image GetImage(int type)
        {
            switch(type)
            {
                case 1:return Resources.meteorGrey_small1;
                case 2:return Resources.meteorGrey_med2;
                case 3:return Resources.meteorGrey_big2;

                case 4:return Resources.meteorBrown_tiny2;
                case 5:return Resources.meteorBrown_med3;
                case 6:return Resources.meteorBrown_big1;
                default:return Resources.meteorGrey_tiny1;
            }
        }

        public override void Draw(Graphics g)
        {
            Move();
            g.DrawImage(GetImage(RockType), this.X, this.Y);
        }

        public override void Move()
        {
            if(this._D==Direction.Down)
            {
                this.Y += this.Speed;
            }
        }
    }
}
