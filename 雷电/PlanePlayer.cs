using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;//导入MOVE函数用到的命名空间
using 雷电.Properties;

namespace 雷电
{
    [Serializable]
    class PlanePlayer:Plane
    {
        //导入玩家飞机图片
        public static Image imagePlayer = Resources.planeplayer;

        //飞机子弹类型，与飞行中得到的buff有关
        public int BulletType
        {
            get;set;
        }

        public int SkillType
        {
            get; set;
        }


        //飞机武器类型
        public static Weapon WeaponType
        {
            get;set;
        }
        
        public Image ImgPlayer
        {
            set
            {
                imagePlayer = value;
            }
            get
            {
                return imagePlayer;
            }
        }
        

        //调用父类的构造函数
        public PlanePlayer(int x, int y, int hp, int speed, Direction _D) : base(imagePlayer, x, y, hp, speed, _D)
        { }
       
       //重写GanmeObj的抽象函数Draw,绘制玩家飞机
        public override void Draw(Graphics g)
        {
            g.DrawImage(imagePlayer, this.X,this.Y,this.Width,this.Heigth);
        }
        //玩家飞机跟随鼠标移动
        public void Move(MouseEventArgs e)//将MouseMove的MouseEventArgs参数调用过来
        {
            this.X = e.X - this.Width / 2;//鼠标的横坐标赋值给当前飞机的横坐标
            this.Y = e.Y - this.Heigth / 2;
        }

        //武器子弹计数，用于双倍火力buff的计数
        //补给中子弹有一定数量，打完即止
        
        static int bulletCount = 0;
        //玩家射击操作
        public int StartFire = 0;
        public void Fire()
        {
            if(StartFire == 1)
            {
                if (this.BulletType == Obj.normalFire)
                {
                    //在窗口中初始化玩家子弹
                    Obj.GetObj().AddGameObj(new PlayerBullet(this, 15));

                }
                else
                {
                    //双倍子弹操作
                    Obj.GetObj().AddGameObj(new PlayerBullet(this, 20));
                    Obj.GetObj().AddGameObj(new PlayerBullet(this, 10));
                    if (bulletCount < 30)
                        bulletCount++;
                    else
                    {
                        bulletCount = 0;
                        this.BulletType = Obj.normalFire;
                    }
                    Obj.GetObj().AddGameObj(new PlayerBullet(this, 10));
                }
            }
        }

        //未完成
        /*public void UseSkill()
        {
            Obj.GetObj().AddGameObj(new PlayerSkill(this.X, this.Y,this.SkillType));
        }*/

        public override void Explosion()
        {
            Obj.GetObj().AddGameObj(new PlayerExplosion(this.X,this.Y));
        }
        
        public override Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width-25, this.Heigth-40);//取得比原矩形稍小的横纵宽高，提高游戏体验
        }

    }
}
