using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    class BulletBuff:Supplies
    {
        //武器buff,现在暂且叫其双倍火力
        private static Image imageBb=Resources.things_gold;
        public BulletBuff(Plane pe,int speed,Direction _DX,Direction _DY):base(imageBb,pe.X,pe.Y,speed)
        {
            this.direcX = _DX;
            this.direcY = _DY;
        }
    }
}
