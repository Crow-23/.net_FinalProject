using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    //玩家飞机爆炸
    class PlayerExplosion:Explosion
    {
        private Image[] imgPE = {
            Resources.boom0,
            Resources.boom1,
            Resources.boom2,
            Resources.boom3
        };

        public PlayerExplosion(int x,int y):base(x,y) { }

        int TimeCount = 0;
        //重写父类绘制
        public override void Draw(Graphics g)
        {
            Music music_PE = new Music();//播放GAME OVER音效
            music_PE.Play("game_over.mp3", 1);
            g.DrawImage(imgPE[TimeCount], this.X, this.Y);//绘制玩家爆炸
            TimeCount += 1;
            if (TimeCount>=imgPE.Length)
                Obj.GetObj().RemoveGemaObj(this);//爆炸后移除玩家
        }
    }
}
