using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    //敌人子弹
    [Serializable]
    class EnemyBullet:Bullet
    {
        //存放敌人子弹图片
        //private static Image imgEB;

        public EnemyBullet(PlaneEnemy eb,int speed,int power,Direction direction) : base(eb, SetImage(eb.EnemyType), speed, power,direction)
        {
        }

        public EnemyBullet(PlaneEnemy eb, int speed, int power, double angle, int BulletType) : base(eb, SetImage(BulletType), speed, power, angle)
        {
        }

        public EnemyBullet(PlaneEnemy eb, int speed, int power, double angle, int BulletType,int dx,int dy) : base(eb, SetImage(BulletType), speed, power, angle, dx, dy)//指定在飞机的哪个位置发射
        {
        }

        public static Image SetImage(int type)
        {
            switch (type)
            {
                case 1: return Resources.fire11;
                case 2: return Resources.fire11;
                case 3: return Resources.fire11;
                case 4: return Resources.enemybullet1;
                case 5: return Resources.enemybullet1;
                case 6:return Resources.fire5;
                default:return Resources.fire11;
            }
        }
        //public override void Draw(Graphics g)
        //{
        //    g.DrawImage(imgEB, this.X -200 , this.Y);
        //}
    }
}
