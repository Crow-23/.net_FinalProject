using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    class Medkit:Supplies
    {
        private static Image imageMk = Resources.pill_green;
        public int MedHp = 200;
        public Medkit(Plane pe,int speed,Direction _DX,Direction _DY):base(imageMk,pe.X,pe.Y,speed)
        {
            this.direcX = _DX;
            this.direcY = _DY;
        }
    }
}
