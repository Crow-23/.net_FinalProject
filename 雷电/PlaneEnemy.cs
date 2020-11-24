using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 雷电.Properties;

namespace 雷电
{
    //敌人飞机
    [Serializable]
    class PlaneEnemy : Plane
    {
        //导入所有敌人飞机的图片,要使用静态成员，不然静态函数GetImage无法访问
        private static Image Enemy1 = Resources.enemy5;
        private static Image Enemy2 = Resources.enemy3;
        private static Image Enemy3 = Resources.enemy2;
        private static Image Enemy4 = Resources.enemy4;
        private static Image Enemy5 = Resources.boss_1;
        static Random r = new Random();//随机数用于敌人飞机飞行改变
        PlanePlayer planePlayer;

        //构造函数
        public PlaneEnemy(int ET, int x, int y, PlanePlayer PP) : base(GetImage(ET), x, y, GetHP(ET), GetSpeed(ET), Direction.Down)//方向向下,传入玩家飞机位置用于发射锁定子弹
        {
            this.EnemyType = ET;//给敌人类型赋值
            planePlayer = PP;
            r = new Random( x % 7);
            AimToPlayer();
        }

        //每架飞机的属性值不同，所以 另外声明一个函数来标记敌人飞机种类
        int deltaX=0;//与玩家飞机的x差值，用于锁定玩家位置
        int deltaY=1;//与玩家飞机的y差值，用于锁定玩家位置
        private void AimToPlayer()
        {
             deltaX = planePlayer.X - this.X;
             deltaY = planePlayer.Y - this.Y;
        }
        //敌人类型，根据类型返回不同的属性
        public int EnemyType
        {
            get;
            set;
        }
        //根据敌人类型，返回不同的图片
        public static Image GetImage(int ET)
        {
            switch (ET)
            {
                case 1:
                case 11:
                    return Enemy1;
                case 2:
                    return Enemy2;
                case 3:
                    return Enemy3;
                case 4:
                    return Enemy4;
                case 5:
                    return Enemy5;
                default:
                    return Enemy1;
            }
            return null;
        }

        //根据敌人类型，返回不同的速度
        public static int GetSpeed(int ET)
        {
            switch (ET)
            {
                case 1:
                case 11:
                    return 4;
                case 2:
                    return 5;
                case 3:
                    return 15;
                case 4:
                    return 5;
                case 5:
                    return 5;
            }
            return 0;
        }

        //根据敌人类型，返回不同的生命值
        public static int GetHP(int ET)
        {
            switch (ET)
            {
                case 1:
                case 11:
                    return 15;
                case 2:
                    return 15;
                case 3:
                    return 15;
                case 4:
                    return 5;
                case 5:
                    return 1000;
            }
            return 0;
        }

        //重写绘画函数
        //根据不同类型来绘制
        public override void Draw(Graphics g)
        {
            //绘制同时开始移动
            this.Move();
            
            switch (this.EnemyType)
            {
                case 1:
                case 11:
                    g.DrawImage(Enemy1, this.X, this.Y);
                    break;
                case 2:
                    g.DrawImage(Enemy2, this.X, this.Y);
                    break;
                case 3:
                    g.DrawImage(Enemy3, this.X, this.Y);
                    break;
                case 4:
                    g.DrawImage(Enemy4, this.X, this.Y);
                    break;
                case 5:
                    g.DrawImage(Enemy5, this.X, this.Y);
                    break;
            }
            

        }
        //因为敌人飞机是向下飞的，所以需要重写父类Move函数
        
        public override void Move()
        {
           
            //判断是否超出窗体
            if (this.X <= 0)
            {
                this.X = 0;
            }
            if (this.X >= 400) //游戏背景宽度
            {
                this.X = 400;
            }
            if (this.Y <= 0)
            {
                this.Y = 0;
            }
            if (this.Y >= 700)//敌人飞机需要穿过窗体下面，并且在飞机离开窗体后移除该对象
            {
                this.Y = 1400;
            }
            switch (this.EnemyType)
            {
                case 1:
                    MoveStraight();
                    MoveLeftAndRight();                
                    break;
                case 11:
                    MoveLeftAndRight();
                    break;
                case 2:
                    MoveRandom();
                    MoveStraight();                   
                    break;
                case 3:
                    MoveToPlayer();
                    MoveSpeedUp(30,1);
                    StayLittleTime(50, 30);
                    break;
                case 4:
                    MoveStraight();
                    StayLittleTime(StayY,100);
                    break;
                case 5:
                    MoveStraight();
                    StayLittleTime(100, 5000);
                    MoveRandom();
                    break;
                default:
                    MoveStraight();
                    break;
            }
           
            /*
            if (r.Next(0, 100) > 70)
            {
                Fire();
            }
            */

        }
        private void MoveStraight()//普通飞行，直飞
        {
            //根据游戏对象当前的方向进行移动
            switch (this._D)
            {
                case Direction.Up:
                    this.Y -= this.Speed;//纵坐标-速度，之后会用timer控件记录时间，所以这里没有减去时间
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X += this.Speed;
                    break;
            }
            
        }
        int LeftFly=1;//用于MoveLeftAndRight函数变量
        private void MoveLeftAndRight()//左右横跳
        {
            //if (this.EnemyType == 1)当飞机下落到一定高度时发生改变
            //{
                //向中间飞行

                /* if(this.X >= 0 && this.X <=170)//当飞机在左边的情况时
                 {

                     this.X += 5;

                 }
                 else if (this.X >= 0 && this.X >= 190)
                 {
                     this.X -= 5;

                 }
                 else
                 {

                 }*/

                if (this.X < 10) LeftFly = 0;//飞机左右横跳，碰到边缘向另一方向飞
                else if (this.X > 390) LeftFly = 1;
                if (LeftFly == 1) this.X -= 5;
                if (LeftFly == 0) this.X += 5;
            //}
        }

        private void MoveSpeedUp(int max,int ac)//加速飞，max最高速度，ac加速度
        { 
             
            
             if ( this.Speed < max)
            {
                this.Speed += ac;
            }
        }

        
        int LeftOrRight = 0;//用于MoveRandom函数的变量，0为直飞，1为左飞，2为右飞
       
        private void MoveRandom()//随机地左右飞行
        {
            
            
            if (r.Next(40) == 1||this.X==0) LeftOrRight = (LeftOrRight + 1) % 3;
            else if (r.Next(40) == 2||this.X+GetImage(this.EnemyType).Width==500) LeftOrRight = (LeftOrRight + 2) % 3;
            if (LeftOrRight == 2)
            {
                this.X += 5;
            }
            else if(LeftOrRight == 1)
            {
                this.X -= 5;
            }


          
        }
       
        private void MoveToPlayer()//冲向玩家
        {
           
            if (deltaY > 0)
            {
                this.X += (int)(this.Speed / Math.Sqrt(deltaX * deltaX + deltaY * deltaY) * deltaX);
                this.Y += (int)(this.Speed / Math.Sqrt(deltaX * deltaX + deltaY * deltaY) * deltaY);
            }
        }

        int StayTime = 0;//用于停留时间的控制
        private void SetStayTime(int time) { if (StayTime == 0) StayTime = time; }

         static public int StayY = 700;//允许外部函数控制StayY的值来控制StayLittleTime的停留位置
         private void StayLittleTime(int Y,int staytime)//在纵坐标Y停留一段时间
        {
            SetStayTime(staytime);
            if (this.Y >= Y&&StayTime>0)
            {
                this.Y -= this.Speed;
                StayTime--;
                if (StayTime == 0) StayTime = -1;
                AimToPlayer();//为撞向玩家的飞机重新定位
            }
            
        }

        int FireTime = 0;//开火次数，用于控制特定次数的开火
        //敌人射击
        public void Fire()
        {
            FireTime++;
            int dir = FindPlayerPlane();
            switch (this.EnemyType)
            {
                case 1://向下方的五个方向发射子弹
                case 11:
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 15, 1, Direction.LeftDown));
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 15, 1, Direction.DownLeft));
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 15, 1, Direction.Down));
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 15, 1, Direction.DownRight));
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 15, 1, Direction.RightDown));
                    break;
                case 2:
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 15, 1, Direction.Down));
                    break;
                case 3:
                    //Obj.GetObj().AddGameObj(new EnemyBullet(this, 15, 1, Direction.Down));3类飞机不开火，撞击！
                    break;
                case 4://向玩家方向发射三颗分散子弹
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 2, (Direction)dir));
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 2, (Direction)((dir + 1) % 12)));
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 2, (Direction)((dir + 11 ) % 12)));
                    break;
                case 5:
                    if (FireTime % 13 == 4)
                    {
                        FireSpin();
                    }
                    else if (FireTime % 13 == 1)
                    {
                        FireToFront();
                    }
                    else if (FireTime % 13 == 7)
                    {
                        FireTwoLine();
                    }
                    else if(FireTime % 13 == 9)
                    {
                        for(int i = 0; i < 80; i++)
                        {
                            FireRandom();
                            Delay(50);
                        }
                    }
                    break;
                default:
                    Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, Direction.Down));
                    break;

            }
            //Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1));//当前子弹速度10，力量1
        }

       
        public override void PlaySound()
        {
            
        }

       /* public Direction GetBulletDirection() {
            switch (this.EnemyType)
            {
                case 1: break;
                case 2: break;
                case 3: break;
                case 4: break;
            }
        }*/

        //重写飞机爆炸
        public override void Explosion()
        {
           if(this.HP <= 0)//当生命值小于0将其移除
            {
                if (r.Next(100) > 80)
                {
                    //医疗包
                    Obj.GetObj().AddGameObj(new Medkit(this, 5, Direction.Right, Direction.Down));
                }
                if(r.Next(100)<20)
                {
                    //武器buff
                    Obj.GetObj().AddGameObj(new BulletBuff(this, 5, Direction.Left, Direction.Down));
                }
                Obj.GetObj().RemoveGemaObj(this);
                //播放爆炸图片集合
                Obj.GetObj().AddGameObj(new EnemyExplosion(this.X, this.Y,this.EnemyType));
                //增加得分
                Obj.GetObj().myScores.ScorePlus(this.EnemyType);
            }
        }

        //判断玩家飞机相对开火飞机的方向
        private int FindPlayerPlane()
        {
            AimToPlayer();
            int absX = Math.Abs(deltaX);
            int absY = Math.Abs(deltaY);
            Direction dir=Direction.Down;
            //根据deltaX和deltaY的值判断玩家飞机相对开火飞机在哪个象限，再根据absX-absY的正负判断离哪个轴更近
            if (deltaX > 20)
            {
                if (deltaY > 20)
                {
                    if (absX - absY >= 0) dir = Direction.RightDown;
                    else dir = Direction.DownRight;
                }
                else if (deltaY >= -20&& deltaX <= 20) dir = Direction.Right;
                else if (deltaY < -20)
                {
                    if (absX - absY >= 0) dir = Direction.RightUp;
                    else dir = Direction.UpRight;
                }
            }
            else if(deltaX <= 20&&deltaX >= -20)
            {
                if (deltaY > 0)
                {
                    dir = Direction.Down;
                }
                else if (deltaY == 0) dir = Direction.Down;
                else if (deltaY < 0)
                {
                    dir = Direction.Up;
                }
            }
            else if (deltaX < -20)
            {
                if (deltaY > 20)
                {
                    if (absX - absY >= 0) dir = Direction.LeftDown;
                    else dir = Direction.DownLeft;
                }
                else if (deltaY >= -20 && deltaX <= 20) dir = Direction.Left;
                else if (deltaY < -20)
                {
                    if (absX - absY >= 0) dir = Direction.LeftUp;
                    else dir = Direction.UpLeft;
                }
            }
            return (int)dir;
        }

        private static void Delay(int milliSecond)//一个延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                Application.DoEvents();//可执行某无聊的操作
            }
        }

        private void FireSpin()
        {
            int dir = 0;
            for (int i = 0; i < 10; i++)
            {
                for (dir = 0; dir < 12; dir++)
                {
                    if (this.HP > 0)
                    {
                        Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, (Direction)dir));
                        Delay(50);
                    }
                }
            }
        }

        private void FireToFront()
        {
            int i;

            for (i = 0; i < 20; i++)
            {
                FireRandom();
                Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, Math.PI * 29 / 18, 1, 165, 110));
                Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, Math.PI * 25 / 18, 1, 30, 110));
                Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, Math.PI * 3 / 2, 1, 90, 173));
                Delay(300);
            }

        }

        private void FireTwoLine()
        {
            int i;

            for (i = 0; i < 20; i++)
            {
                FireRandom();
                Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, Math.PI * 3 / 2, 6, 0, 67));
                Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, Math.PI * 3 / 2, 6, 180, 67));
                Delay(200);
            }
        }

        private void FireRandom()
        {
             Obj.GetObj().AddGameObj(new EnemyBullet(this, 10, 1, Math.PI * (r.NextDouble()+1), 4));
        
        }
    }
}
