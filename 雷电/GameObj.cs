using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 雷电
{
    //方向
    enum Direction { Right=0,RightUp,UpRight,Up,UpLeft,LeftUp,Left,LeftDown,DownLeft,Down,DownRight,RightDown}
    //RightUp指的是右偏上30度，UpRight指的是上偏右30度，以此类推，以30度为一个单位变化

    //所有游戏对象的父类，封装子类共有的成员
    //有抽象成员所以标记为抽象类
    [Serializable]
    abstract class GameObj
    {
        #region 高度，宽度，横纵坐标，生命值，速度，方向
        public int Width //游戏对象的宽度
        {
            get;
            set;
        }

        public int Heigth//高度
        {
            get;
            set;
        }

        public int X//游戏对象的横坐标
        {
            get;
            set;
        }

        public int Y//游戏对象的纵坐标
        {
            get;
            set;
        }

        public int HP//生命值
        {
            get;
            set;
        }

        public int Speed//速度
        {
            get;
            set;
        }

        public Direction _D//方向
        {
            get;
            set;
        } 
        #endregion
        
        //构造函数
        public GameObj(int width,int height,int x ,int y ,int hp,int speed,Direction d)
        {
            this.Width = width;
            this.Heigth = height;
            this.X = x;
            this.Y = y;
            this.HP = hp;
            this.Speed = speed;
            this._D = d;
        }

        //构造函数重载，用与爆炸类
        public GameObj(int x , int y)
        {
            this.X = x;
            this.Y = y;
        }

        //石头
        public GameObj(int width,int height,int speed)
        {
            this.X= new Random().Next(600);
            this.Y = -50;
            this._D = Direction.Down;
            this.Width = width;
            this.Heigth = height;
            this.Speed = speed;
        }

        //绘制对象的抽象函数,使用GDI
        public abstract void Draw(Graphics g); 
       
        //碰撞检测，返回对象矩形
        public virtual Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Heigth);//取得横纵宽高
        }

        //移动规则的虚方法，每个子类的规则不同，需重写
        public virtual void Move() { }

        public virtual void PlaySound() { }
    }
}
