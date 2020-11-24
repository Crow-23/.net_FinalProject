using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    //未完成
    class PlayerSkill : Skill
    {
        //用数组导入多张技能图片
        private Image imgS1 = Resources.shield;

        
        public int SkillType
        {
            get;
            set;
        }
        
        public PlayerSkill(int x, int y,int ST) : base(x, y)
        {
            this.SkillType = ST;
        }
        public override void Draw(Graphics g)
        {
            switch (this.SkillType)
            {
                case 0:
                    g.DrawImage(imgS1, this.X, this.Y);
                    break;

            }
        }
    }
}
