using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
 

namespace 雷电
{
    //子弹父类
    [Serializable]
    class Bullet:GameObj
    {
        private Image imageBullet;//字段用来存储子弹图片
        private double angle=0;

        //子弹威力
        public int Power
        {
            get;
            set;
        }
        //调用父类构造函数
        ////需要确定子弹是哪个飞机发射的
        ////获取子弹的xy轴和高宽度，确定子弹要在飞机中前的位置发射
        //飞机朝上子弹向上，飞机朝下，子弹向下
        public Bullet(PlanePlayer pb,Image img,int speed,int power) : base(img.Width, img.Height, pb.X + pb.Width/2,pb.Y + pb.Heigth/2,0,speed,pb._D)
        {
            this.imageBullet = img;
            this.Power = power;
        }

        //
        //敌机子弹构造函数
        public Bullet(PlaneEnemy eb, Image img, int speed, int power, Direction direction) : base(img.Width, img.Height, eb.X + eb.Width * 40 / 100, eb.Y + eb.Heigth * 35 / 100, 0, speed, direction)
        {
            this.imageBullet = img;
            this.Power = power;
        }

        public Bullet(PlaneEnemy eb, Image img, int speed, int power, double angle) : base(img.Width, img.Height, eb.X + eb.Width * 40 / 100, eb.Y + eb.Heigth * 35 / 100, 0, speed, Direction.Up)
        {
            this.imageBullet = img;
            this.Power = power;
            this.angle = angle;
        }
        public Bullet(PlaneEnemy eb, Image img, int speed, int power, double angle,int dx,int dy) : base(img.Width, img.Height, eb.X + dx, eb.Y + dy, 0, speed, Direction.Up)
        {
            this.imageBullet = img;
            this.Power = power;
            this.angle = angle;
        }

        //重写父类的抽象成员
        public override void Draw(Graphics g)
        {
            if (this._D == Direction.Up && angle != 0)
                this.Move(angle);
            else
                this.Move();//调用子弹飞行的规则
            g.DrawImage(imageBullet, this.X, this.Y);
        }

        //重写父类的Move函数，让子弹不停留在窗体边缘
        public override void Move()
        {
           
            switch (this._D)
            {
                case Direction.Up:
                    this.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                case Direction.DownLeft:
                    this.Y += (int)(this.Speed * Math.Sin(Math.PI / 3));
                    this.X -= (int)(this.Speed * Math.Cos(Math.PI / 3));
                    break;
                case Direction.LeftDown:
                    this.Y += (int)(this.Speed * Math.Sin(Math.PI / 6));
                    this.X -= (int)(this.Speed * Math.Cos(Math.PI / 6));
                    break;
                case Direction.DownRight:
                    this.Y += (int)(this.Speed * Math.Sin(Math.PI / 3));
                    this.X += (int)(this.Speed * Math.Cos(Math.PI / 3));
                    break;
                case Direction.RightDown:
                    this.Y += (int)(this.Speed * Math.Sin(Math.PI / 6));
                    this.X += (int)(this.Speed * Math.Cos(Math.PI / 6));
                    break;
                case Direction.RightUp:
                    this.Y -= (int)(this.Speed * Math.Sin(Math.PI / 6));
                    this.X += (int)(this.Speed * Math.Cos(Math.PI / 6));
                    break;
                case Direction.UpRight:
                    this.Y -= (int)(this.Speed * Math.Sin(Math.PI / 3));
                    this.X += (int)(this.Speed * Math.Cos(Math.PI / 3));
                    break;
                case Direction.UpLeft:
                    this.Y -= (int)(this.Speed * Math.Sin(Math.PI / 3));
                    this.X -= (int)(this.Speed * Math.Cos(Math.PI / 3));
                    break;
                case Direction.LeftUp:
                    this.Y -= (int)(this.Speed * Math.Sin(Math.PI / 6));
                    this.X -= (int)(this.Speed * Math.Cos(Math.PI / 6));
                    break;
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X += this.Speed;
                    break;
            }
            /*
            if(this.Y<=0)//超出上窗口，让子弹消失，移除子弹对象
            {
                this.Y = -50;
            }
            if(this.Y >= 850)
            {
                this.Y = 1000;
            }
            */

        }

        public void Move(double Angle)
        {
            this.X += (int)(this.Speed * Math.Cos(Angle));
            this.Y -= (int)(this.Speed * Math.Sin(Angle));
        }
    }
}
