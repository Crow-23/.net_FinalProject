using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 雷电
{
    //补给类，加生命，加弹药等
    class Supplies : GameObj
    {
        private Image image;
        public int timeCount
        {
            set;get;
        }

        public Direction direcX
        {
            set; get;
        }
        public Direction direcY
        {
            set; get;
        }


        public Supplies(Image img, int x, int y, int speed) : base(img.Width, img.Height, x, y, 100, speed, Direction.Down)//hp用于代指存活时间
        {
            this.image = img;
        }

        public override void Move()
        {
            
            switch (direcX)
            {
                case Direction.Left: this.X -= Speed; break;
                case Direction.Right: this.X += Speed; break;
            }
            switch (direcY)
            {
                case Direction.Up:this.Y -= Speed;break;
                case Direction.Down: this.Y += Speed; break;
            }
            
        }

        public override void Draw(Graphics g)
        {
            this.Move();
            this.timeCount += 1;
            g.DrawImage(image, this.X, this.Y);
        }
    }
}
