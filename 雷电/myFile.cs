using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;


namespace 雷电
{
    //目前版本出现死档需去log路径下删除txt文件 重新启动游戏
    //存档类
    [Serializable]
    class MyFile
    {
        //分数 血量 坐标
        public int score;
        public int property;
        public int HP;
        public int X;
        public int Y;
        public int fire;//开火状态
        public List<PlaneEnemy> Enemies;//敌机状态
        public Weapon myweapon;//当前武器
        public List<EnemyBullet> listenemyBullets;//敌机子弹状态
        public GameLevel level;//同步关卡状态
        public Image imageplayer;
        public int index;
    }
}
