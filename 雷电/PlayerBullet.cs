using CCWin.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    //玩家子弹
    [Serializable]
    class PlayerBullet:Bullet
    {
        internal static Image imagePB = Resources.fire01;//导入玩家子弹图片

        public PlayerBullet(PlanePlayer pb, int speed) : base(pb, GetImage(pb.BulletType), speed,3) 
        {
        }

        //返回子弹图片
        public static Image GetImage(int type)
        {
            switch(type)
            {
                case 1:return PlanePlayer.WeaponType.Img;
                case 2:return Resources.fire5;
                default:return null;
            }    
        }

        //返回子弹伤害
        public static int GetPower(int type)
        {
            switch(type)
            {
                case 1:return PlanePlayer.WeaponType.Power;
                case 2:return PlanePlayer.WeaponType.Power * 2;
                default:return 1;
            }
        }


        

    }
}
