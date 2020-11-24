using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 雷电
{
    [Serializable]
    struct EnemyAppear//一个向量，用来记录一个敌机出现的时间，位置和其类型
    {
        public EnemyAppear(int time,int x,int y,int type)
        {
            Time = time;X = x;Y = y;Type = type;
        }
        public int Time;//单位100ms
        public int X;
        public int Y;
        public int Type;
    }
    [Serializable]
    class GameLevel
    {

        public List<EnemyAppear> listenemyAppears = new List<EnemyAppear>();//创建列表储存所有敌机出现向量
        public void SortByTime()//将列表按时间排序，以便程序按时间生成敌机
        {
            listenemyAppears = listenemyAppears.OrderBy(o => o.Time).ToList();
        }
    }
}
