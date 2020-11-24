using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using 雷电.Properties;

namespace 雷电
{ 
   
    //设置为可序列化的类
    public partial class Game_Form : Form
    {
        String path = @"..\save.txt";          //存档路径
        Music musicBG = new Music();//背景音乐
        public Game_Form()
        {
            InitializeComponent();
            musicBG.Play("game_music.mp3", 0);//播放背景音乐，0代表循环播放
            InitializeGame();
            if (File.Exists(path)&&!Obj.IsDead)//反序列化 读存档
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                MyFile Loadfile = formatter.Deserialize(stream) as MyFile;
                LoadMyFile(Loadfile);
                stream.Close();
            }
            else { return; }
        }

        //随机数生成，用来随机绘制敌人位置
        static Random rnd = new Random();
        PlanePlayer planePlayer = new PlanePlayer(100, 400, 50, 3, Direction.Up);

        //隐藏鼠标
        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public static extern int ShowCursor(bool bShow);

        //初始化游戏
        public void InitializeGame()
        {
            
            /*初始化游戏背景，玩家数据，子弹类型等，攻击力等
            *初始化背景，设置背景初始属性纵坐标，初始速度为4
            * 
            */
            Obj.GetObj().AddGameObj(new BackGround(0, -440, 4));
            //this.Paint += new PaintEventHandler(Game_Paint);
            Obj.GetObj().AddThreadBGM(new Thread(Obj.GetObj().BG.PlaySound));
            //初始化玩家飞机
           
            Obj.GetObj().AddGameObj(planePlayer);
            Obj.GetObj().PP.BulletType = Obj.normalFire;
            Thread.Sleep(1000);//独占性延时
            InitialPlaneEnemy();//初始化敌人飞机
            ShowCursor(false);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //当窗体重绘时，会执行当前时间（绘制背景）
            
        }

        private void Game_Paint(object sender,PaintEventArgs e)
        {
            Obj.GetObj().Draw(e.Graphics);
        }

        
        static GameLevel LevelNow = LevelDesign.Level1;

        private void InitialPlaneEnemy()
        {
           
            LevelDesign.CreateLevel1();
          
          
            /* for (int i = 0; i < 4; i++)//初始化敌人飞机，同时
             {
                 Obj.GetObj().AddGameObj(new PlaneEnemy(rnd.Next(1, 4), rnd.Next(0, 450), -500,planePlayer));
             }

             if(rnd.Next(0,100) > 80)//大boss出现的机率
             {
                 Obj.GetObj().AddGameObj(new PlaneEnemy(4, rnd.Next(0, this.Width), -500,planePlayer));
             }*/
            
        }
        public void SaveMyFile()
        {
            MyFile newfile = new MyFile();
            newfile.HP = Obj.GetObj().PP.HP;
            newfile.X = Obj.GetObj().PP.X;
            newfile.Y = Obj.GetObj().PP.Y;
            newfile.fire = Obj.GetObj().PP.BulletType;
            newfile.score = Obj.GetObj().myScores.Score;
            newfile.property = Obj.PlayerProperty;
            newfile.myweapon = PlanePlayer.WeaponType;
            newfile.Enemies = Obj.GetObj().listplaneEnemies;
            newfile.listenemyBullets = Obj.GetObj().listenemyBullets;
            newfile.level = LevelNow;
            newfile.imageplayer = planePlayer.ImgPlayer;
            newfile.index = ListIndex;
            BinaryFormatter formatter = new BinaryFormatter();

            string path = @"..\save.txt";

            Stream stream1;
            stream1 = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            formatter.Serialize(stream1, newfile);
            stream1.Close();
            
        }
        private void LoadMyFile(MyFile f) //加载存档
        {
            Obj.GetObj().PP.X = f.X;
            Obj.GetObj().PP.Y = f.Y;
            Obj.GetObj().PP.HP = f.HP;
            Obj.GetObj().PP.StartFire = f.fire;
            Obj.GetObj().myScores.Score = f.score;
            Obj.PlayerProperty = f.property;
            PlanePlayer.WeaponType = f.myweapon;
            Obj.GetObj().listplaneEnemies = f.Enemies;
            Obj.GetObj().listenemyBullets = f.listenemyBullets;
            PlanePlayer.imagePlayer = f.imageplayer;
            LevelNow = f.level;
            ListIndex = f.index;
        }
        private void Game_Form_Load(object sender, EventArgs e)
        {
            //将图片绘制在缓冲区，解决窗体加载闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Game_MouseMove(object sender,MouseEventArgs e)
        {
            Obj.GetObj().PP.Move(e);
        }
        Music musicB = new Music();//子弹音效
        private void Game_MouseDown(object sender, MouseEventArgs e)
        {
            musicB.Play("bullet.mp3", 1);//播放子弹音效
            Obj.GetObj().PP.StartFire = 1;
            Obj.GetObj().PP.Fire();
            Obj.GetObj().PP.StartFire = 0;
        }

        private void timer_Game_Invalidate_Tick(object sender,EventArgs e)
        {
            
            this.Invalidate();//使控件的整个图面无效并重绘
            this.Update();
            int countEnemy = Obj.GetObj().listplaneEnemies.Count;//飞机数量
            if (countEnemy <= 1)
            {
                //InitialPlaneEnemy();
            }
            //碰撞检测
            Obj.GetObj().Crash();
            //越界检测
            Obj.GetObj().Overrun();
        }

        /*static int start = Environment.TickCount;
        public static bool Delay(int milliSecond)
        {
            while (Math.Abs(Environment.TickCount - start) % milliSecond==0)//毫秒
            {
                return true;
            }
            return false;
        }//延时函数*/

        private void timer_EnemyAutoFire_Tick(object sender, EventArgs e)
        {
            Obj.GetObj().EnemyAutoFire();
            if (new Random().Next(100) > 50) Obj.GetObj().AddGameObj(new Rocks(new Random().Next(7)));
        }



        int x;
        int y =0 ;
        private void Game_KeyDown(object sender,KeyEventArgs k)
        {
            
            if (k.KeyCode == Keys.Q&& y <=5)
            {
                y += 1;
                /*Obj.GetObj().PP.StartFire = 1- Obj.GetObj().PP.StartFire;//自动射击*/
                //PlayerSkill ps = new PlayerSkill(planePlayer.X, planePlayer.Y);
                planePlayer.ImgPlayer = Resources.shield;
                x = planePlayer.HP;
                planePlayer.HP = 999;


            }
            else if (k.KeyCode == Keys.E)
            {
                planePlayer.ImgPlayer = Resources.planeplayer;
                planePlayer.HP = x;
            }
            else if (k.KeyCode == Keys.Escape)
            {
                //ESC相关操作
                //相关数据存档
                ShowCursor(true);//显示鼠标
                SaveMyFile();


                GamePause_Form gp = new GamePause_Form();

                gp.Show();

                //SetParent(gp.Handle, this.Handle);

                //使时钟暂停

                Obj.IsPause = true;
                timer_EnemyAutoFire.Enabled = false;
                timer_BGMusic.Enabled = false;
                timer_Game_Invalidate.Enabled = false;
                timer_EnemyAppearControl.Enabled = false;
            }
        }

        public static void StartTimer()
        {
            
        }

        private void timer_BGMusic_Tick(object sender, EventArgs e)
        {
            foreach(Thread t in Obj.GetObj().threadsBGM)
            {
                if(t.ThreadState==ThreadState.Unstarted)
                {
                    t.Start();
                }
            }
            foreach(Thread t in Obj.GetObj().threadsBGM)
            {
                if(t.ThreadState!=ThreadState.Running)
                {
                    t.Abort();
                    Obj.GetObj().threadsBGM.Remove(t);
                    break;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!Obj.GameRun)
            {
                Obj.QuestContinue = Obj.IsPause = false;
                Obj.GameRun = true;
                this.Dispose(true);
            }
            else if(Obj.IsPause&&!Obj.QuestContinue)
            {
                timer_EnemyAutoFire.Enabled = false;
                timer_BGMusic.Enabled = false;
                timer_Game_Invalidate.Enabled = false;
                timer_EnemyAppearControl.Enabled = false;
            }
            else if(Obj.IsPause&&Obj.QuestContinue)
            {
                timer_EnemyAutoFire.Enabled = true;
                timer_BGMusic.Enabled = true;
                timer_Game_Invalidate.Enabled = true;
                timer_EnemyAppearControl.Enabled = true;

                Obj.QuestContinue = false;
                Obj.IsPause = false;
            }
        }

        //使子窗口显示在父窗口之上
        [DllImport("User32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr parent);

        private void timer_PlayerFire_Tick(object sender, EventArgs e)
        {
            Obj.GetObj().PP.Fire();
        }

        
        int ListIndex = 0;

        
        
        private void timer_EnemyAppearControl_Tick(object sender, EventArgs e) //定时器生成敌机
        {
            
            Obj.GetObj().time++;         
            while (ListIndex <= LevelNow.listenemyAppears.Count - 1&&LevelNow.listenemyAppears[ListIndex].Time <= Obj.GetObj().time)
            {


                if (LevelNow.listenemyAppears[ListIndex].Type > 0)
                {
                    Obj.GetObj().AddGameObj(new PlaneEnemy(LevelNow.listenemyAppears[ListIndex].Type, LevelNow.listenemyAppears[ListIndex].X, LevelNow.listenemyAppears[ListIndex].Y, planePlayer));
                    ListIndex++;
                }
                else if (LevelNow.listenemyAppears[ListIndex].Type == -1)
                {
                    PlaneEnemy.StayY = LevelNow.listenemyAppears[ListIndex].X;
                    ListIndex++;
                }
                
                
                break;
                
            }
            if (ListIndex >= LevelNow.listenemyAppears.Count && Obj.GetObj().listplaneEnemies.Count == 0)
            {
                Game_Die die = new Game_Die();
                
                
                Obj.IsDead = true;
                Obj.GetObj().time = 0;
                Obj.GetObj().ClearData();
                Obj.PlayerProperty += (int)(Obj.GetObj().myScores.Score * 0.5);
                Obj.GameRun = false;
                
                die.Show();
            }
        }

        
    }
}
