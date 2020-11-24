using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 雷电.Properties;

namespace 雷电
{
    //存放所有游戏的对象，通过OBJ类与窗体进行交互
    [Serializable]
    class Obj
    {
        //停止和请求开始标志，实现游戏暂停用
        public static bool IsPause = false;
        public static bool QuestContinue = false;
        public static bool IsDead = false;//上次退出游戏时是否死亡

        //游戏是否在运行标志
        public static bool GameRun = true;
        public int time = 0;//游戏时间

        //最大生命
        private int MaxHP = 100;

        public static int PlayerProperty
        {
            get;set;
        }

        //武器buff，普通火力和双倍火力
        public static int normalFire = 1;
        public static int doubleFire = 2;

        //构造函数 
        private Obj(){}

        private static Obj _obj = null;//

        public static Obj GetObj()//取得 对象
        {
            if(_obj == null)
            {
                _obj = new Obj();
            }
            return _obj;
        }

        //存取背景图像
        public BackGround BG
        {
            get;
            set;
        }

        //存储玩家飞机对象
        public PlanePlayer PP
        {
            get;set;
        }

        public PlayerBullet PB
        {
            get;set;
        }

        //存储玩家子弹
        List<PlayerBullet> listPlayerBullet = new List<PlayerBullet>();

        //存储敌人飞机
        public List<PlaneEnemy> listplaneEnemies = new List<PlaneEnemy>();//要在form1类中调用

        //存储玩家爆炸
        List<PlayerExplosion>listplayerExplosions = new List<PlayerExplosion>();

        //存储敌人爆炸对象
        List<EnemyExplosion> listenemyExplosions = new List<EnemyExplosion>();

        //存储敌人子弹
       public List<EnemyBullet> listenemyBullets = new List<EnemyBullet>(); //存档需要使用

        //存储分数
        public Scores myScores = new Scores(0);

        //存储玩家飞机技能
        List<PlayerSkill> listplayerSkill = new List<PlayerSkill>();

        //存储Medkit
        List<Supplies> listSupllies = new List<Supplies>();

        public List<Thread> threadsBGM = new List<Thread>();

        List<Rocks> listRocks = new List<Rocks>();


        //将创建的游戏对象添加到窗体中
        public void AddGameObj(GameObj ob)//不确定要添加哪个对象，所以添加父类
        {
            if (ob is BackGround)
            {
                this.BG = ob as BackGround;
            }
            else if (ob is PlanePlayer)//判断传入对象是否是玩家飞机
            {
                this.PP = ob as PlanePlayer;//转换成玩家飞机，赋值给玩家属性
            }
            else if (ob is PlayerBullet)//添加玩家子弹
            {
                listPlayerBullet.Add(ob as PlayerBullet);
            }
            else if (ob is PlaneEnemy)//将ob对象赋值给PlaneEnemy,加载到敌人飞机的集合当中
            {
                listplaneEnemies.Add(ob as PlaneEnemy);
            }
            else if(ob is EnemyExplosion)//将ob对象赋值给 EnemyExplosio,加载到敌人飞机爆炸的集合当中
            {
                listenemyExplosions.Add(ob as EnemyExplosion);
            }
            else if(ob is EnemyBullet)
            {
                listenemyBullets.Add(ob as EnemyBullet);//添加敌人子弹到集合中
            }
            else if(ob is PlayerExplosion)
            {
                listplayerExplosions.Add(ob as PlayerExplosion);
            }
            else if(ob is Supplies)
            {
                listSupllies.Add(ob as Supplies);
            }
            else if(ob is Rocks)
            {
                listRocks.Add(ob as Rocks);
            }
            else if(ob is PlayerSkill)
            {
                listplayerSkill.Add(ob as PlayerSkill);
            }
        }

        public void AddThreadBGM(Thread t)
        {
            threadsBGM.Add(t);
        }

        public void RemoveThreadBGM(Thread t)
        {
            threadsBGM.Remove(t);
        }

        //移除游戏对象
        public void RemoveGemaObj(GameObj ob)
        {
            if (ob is PlayerBullet)//移除窗体外的玩家飞机子弹
            {
                listPlayerBullet.Remove(ob as PlayerBullet);
            }
            else if (ob is PlaneEnemy) //移除窗体外的敌人飞机
            {
                listplaneEnemies.Remove(ob as PlaneEnemy);
            }
            else if(ob is EnemyExplosion)
            {
                listenemyExplosions.Remove(ob as EnemyExplosion);
            }
            else if(ob is EnemyBullet)
            {
                listenemyBullets.Remove(ob as EnemyBullet);
            }
            else if(ob is PlayerExplosion)
            {
                listplayerExplosions.Remove(ob as PlayerExplosion);
            }
            else if(ob is Supplies)
            {
                listSupllies.Remove(ob as Supplies);
            }
            else if(ob is Rocks)
            {
                listRocks.Remove(ob as Rocks);
            }
            else if (ob is PlayerSkill)
            {
                listplayerSkill.Remove(ob as PlayerSkill);
            }
        }

        //碰撞检测
        public void Crash()
        {
            //判断玩家子弹是否打中敌人
            for (int i = 0; i < listPlayerBullet.Count; i++)
            {
                
                for(int j = 0;j < listplaneEnemies.Count; j++)
                {
                    if (listPlayerBullet[i].GetRectangle().IntersectsWith(listplaneEnemies[j].GetRectangle()))//判断玩家子弹矩形是否和敌人飞机矩形相交
                    {
                        listplaneEnemies[j].HP -= listPlayerBullet[i].Power;//当子弹射击到目标时，HP减去武器伤害值
                        listplaneEnemies[j].Explosion();//调用敌人类的爆炸函数，判断敌人是否死亡
                        listPlayerBullet.RemoveAt(i);//销毁射击到敌人身上的子弹
                        break;
                    }
                }
            }
            //判断敌人子弹是否打中玩家
            for(int i = 0; i < listenemyBullets.Count; i++)
            {
                if (listenemyBullets[i].GetRectangle().IntersectsWith(this.PP.GetRectangle()))//判断敌人子弹矩形是否和玩家矩形相交
                {
                    PP.HP -= listenemyBullets[i].Power;
                    if (PP.HP < 0)
                    {
                        this.PP.Explosion();
                        Game_Die die = new Game_Die();
                        die.Show();
                        Obj.IsPause = true;
                    }
                        listenemyBullets.RemoveAt(i);//销毁子弹                    
                        break;
                    
                }
            }
            //判断玩家是否撞到敌人飞机
            for(int i =0;i < listplaneEnemies.Count; i++)
            {
                if (listplaneEnemies[i].GetRectangle().IntersectsWith(this.PP.GetRectangle()))
                {
                    listplaneEnemies[i].HP = listplaneEnemies[i].HP - 10;
                    PP.HP -= 10;
                    if (PP.HP <= 0)
                    {
                        this.PP.Explosion();
                        Game_Die die = new Game_Die();
                        die.Show();
                        Obj.IsPause = true;
                    }
                        listplaneEnemies[i].Explosion();
                        break;
                    
                }
            }
            //判断供给是否与玩家接触
            for(int i=0;i<listSupllies.Count;i++)
            {
                if(listSupllies[i].GetRectangle().IntersectsWith(this.PP.GetRectangle()))
                {
                    if (listSupllies[i] is Medkit)
                    {
                        this.PP.HP += (listSupllies[i] as Medkit).MedHp;
                        if (PP.HP > MaxHP)
                        {
                            PP.HP = MaxHP;
                        }
                        listSupllies.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        //此处放武器buff相关处理代码
                        this.PP.BulletType = doubleFire;
                        listSupllies.RemoveAt(i);
                        break;
                    }
                }
            }


        }

        public void EnemyAutoFire()
        {
            Random rd = new Random();
            for(int i=0;i<listplaneEnemies.Count;i++)
            {
               // if(rd.Next(100)<100)
                    listplaneEnemies[i].Fire();
            }
        }

        public void Draw(Graphics g)//绘制
        {
            BG.Draw(g);//绘制背景
            PP.Draw(g);//绘制玩家飞机
            for(int i = 0;i<listPlayerBullet.Count;i++)//把集合中的每一发子弹绘制出来
            {
                listPlayerBullet[i].Draw(g);
            }
            for(int i = 0;i < listplaneEnemies.Count;i++)//绘制敌人飞机
            {
                listplaneEnemies[i].Draw(g);
            }
            for(int i = 0;i < listenemyExplosions.Count; i++)//绘制敌人爆炸
            {
                listenemyExplosions[i].Draw(g);
            }
            for(int i= 0;i < listenemyBullets.Count; i++)//绘制敌人子弹
            {
                listenemyBullets[i].Draw(g);
            }
            for(int i = 0; i < listplayerExplosions.Count; i++)
            {
                listplayerExplosions[i].Draw(g);
            }
            DrawScore(g);//绘制分数
            for (int i = 0; i < listSupllies.Count; i++)
            {
                listSupllies[i].Draw(g);
            }
            for (int i = 0; i < listRocks.Count; i++) 
            {
                listRocks[i].Draw(g);
            }
            for (int i = 0; i < listplayerSkill.Count; i++)
            {
                listplayerSkill[i].Draw(g);
            }
        }

        public void DrawBG(Graphics g)
        { 
        }

        //敌机子弹和玩家子弹以及补给的越界检测
        public void Overrun()
        {
            //敌机子弹
            foreach(EnemyBullet eb in listenemyBullets)
            {
                if(eb.Y>850)
                {
                    RemoveGemaObj(eb);
                    break;
                }
            }
            //玩家子弹
            foreach(PlayerBullet pb in listPlayerBullet)
            {
                if(pb.Y<0)
                {
                    RemoveGemaObj(pb);
                    break;
                }
            }
            //补给
            foreach(Supplies s in listSupllies)
            {
                if(s.timeCount<=s.HP)
                {
                    if(s.X<=0)
                    {
                        s.direcX = Direction.Right;
                    }
                    else if(s.Y<=0)
                    {
                        s.direcY = Direction.Down;
                    }
                    else if(s.X>=300)
                    {
                        s.direcX = Direction.Left;
                    }
                    else if(s.Y>=500)
                    {
                        s.direcY = Direction.Up;
                    }
                }
                else
                {
                    RemoveGemaObj(s);
                    break;
                }
            }

            //判断敌机是否接触边界
           for(int i=0;i<listplaneEnemies.Count;i++)
            {
                if(listplaneEnemies[i].Y>=750)
                {
                    //pe.Y = -50;
                    listplaneEnemies[i].HP = 0;
                    listplaneEnemies[i].Explosion();
                }
            }

            //岩石是否接触边界
            foreach(Rocks r in listRocks)
            {
                if (r.Y > 950) RemoveGemaObj(r);
                break;
            }
        }

        //画分数
        public void DrawScore(Graphics g)
        {
            Font font = new Font("SANS_SERIF", 22);
            Brush brush = Brushes.White;
            g.DrawImage(Resources.score, 0, 750);
            g.DrawImage(Resources.hp, 370, 750);
            g.DrawString(" "+myScores.Score, font, brush, 20,750);
            g.DrawString(" " + PP.HP,font,brush,400,750);
        }

        //清楚单局游戏内的数据
        public void ClearData()
        {
            PP.BulletType = normalFire;
            listenemyBullets.Clear();
            listplaneEnemies.Clear();
            listPlayerBullet.Clear();
            listRocks.Clear();
            listenemyExplosions.Clear();
            listplayerExplosions.Clear();
            listSupllies.Clear();
            myScores.Score = 0;
            
        }
    }
}
