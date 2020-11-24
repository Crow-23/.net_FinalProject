using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 雷电
{
    //飞机的父类
    [Serializable]
    abstract class Plane:GameObj
    {
        private Image imagePlane;//用于存储飞机图片

        public Plane(Image img, int x, int y, int hp,int speed,Direction _D):base(img.Width ,img.Height,x,y,hp,speed,_D)
        {
            this.imagePlane = img;
        }

        //判断飞机是否爆炸的抽象函数，具体规则由子类实现
        public abstract void Explosion();
    }
}
