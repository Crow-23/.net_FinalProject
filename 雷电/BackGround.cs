using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    //游戏背景，继承game父类
    
    class BackGround:GameObj
    {
        //导入背景图片
        private static Image imageBG = Resources.backgroud;//背景图资源的静态成员，便于构造函数访问
        public SoundPlayer gameBG = new SoundPlayer(@"sound\game_music.wav"); 
        public BackGround(int x,int y,int speed):base(imageBG.Width,imageBG.Height,x, y,0,speed,Direction.Down)//不需要hp
        { }

        //重写绘制图像，让图片处于滚动状态
        public override void Draw(Graphics g)
        {
            this.Y += this.Speed;//当前图像的纵坐标+当前图标的移动速度
            //如果背景图移动到0.0的时候，回到最初状态，不然会露出窗体本来的背景
            if(this.Y == 0)
            {
                this.Y = -300;//窗体高度
            }
            //用g++参数，不停绘制在窗体中
            g.DrawImage(imageBG, this.X, this.Y);
        }

        public override void PlaySound()
        {
            gameBG.PlayLooping();
        }
    }
}
