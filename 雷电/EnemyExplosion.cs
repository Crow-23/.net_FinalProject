using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;
using System.Media;
using System.Threading;


namespace 雷电
{
    class EnemyExplosion:Explosion
    {

        //用数组导入飞机爆炸的多张图片
        private Image[] imgEE1 =
        {
            Resources.boom0,
            Resources.boom1,
            Resources.boom2,
            Resources.boom3

        };

        //根据敌人飞机种类导入不同的爆炸图片
        public int ExplosionType
        {
            get;
            set;
        }
        //构造函数
        public EnemyExplosion(int x ,int y,int EE) : base(x,y)
        {
            this.ExplosionType = EE;
        }

        int TimeCount = 0;

        //绘制爆炸
        public override void Draw(Graphics g)
        {
            //根据飞机类型绘制爆炸图片到窗体
            /*
            switch (this.ExplosionType)
            {
                case 1:
                    for (int i = 0; i < imgEE1.Length; i++)
                    {
                        g.DrawImage(imgEE1[i], this.X, this.Y);
                    }
                    break;
                case 2:
                    for (int i = 0; i < imgEE1.Length; i++)
                    {
                        g.DrawImage(imgEE1[i], this.X, this.Y);
                    }
                    break;
                case 3:
                    for (int i = 0; i < imgEE1.Length; i++)
                    {
                        g.DrawImage(imgEE1[i], this.X, this.Y);
                    }
                    break;
            }
            */
            Music music_EE = new Music();//敌人爆炸音效
            switch (this.ExplosionType)
            {
                case 1:
                    g.DrawImage(imgEE1[TimeCount], this.X, this.Y);
                    music_EE.Play("enemy1_down.mp3", 1);
                    break;
                case 2:
                    g.DrawImage(imgEE1[TimeCount], this.X, this.Y);
                    music_EE.Play("enemy2_down.mp3", 1);
                    break;
                case 3:
                    g.DrawImage(imgEE1[TimeCount], this.X, this.Y);
                    music_EE.Play("enemy0_down.mp3", 1);
                    break;
                case 4:
                    g.DrawImage(imgEE1[TimeCount], this.X, this.Y);
                    music_EE.Play("enemy2_down.mp3", 1);
                    break;
            }
            TimeCount += 1;
            if(TimeCount>=imgEE1.Length)
                Obj.GetObj().RemoveGemaObj(this);
        }
    }


}
