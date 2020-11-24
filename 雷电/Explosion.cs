using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 雷电
{
    abstract class Explosion:GameObj //不调用父类的抽象函数
    {
        //调用父类的构造函数
        public Explosion(int x,int y) : base(x,y)
        {
        }

    }
}
