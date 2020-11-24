using CCWin.SkinClass;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using 雷电.Properties;

namespace 雷电
{
    //菜单窗体
    public partial class Menue_Form : Form
    {
        public bool over = false;
        public Menue_Form()
        {
            InitializeComponent();
            InitializeGameData();
        }

        //初始化数据
        //此处需修改，用于载入游戏数据
        public void InitializeGameData()
        {

            /*
             * 初始化商店 购买与否标志
             */


            /*
             * 初始化玩家装备的皮肤、武器以及金币等
             */
             //玩家金币
            Obj.PlayerProperty = 500;
            //默认武器
            PlanePlayer.WeaponType = new Weapon(Resources.fire01, 0, true, 1);



        }

        private void menue_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        //使子窗口显示在父窗口之上
        [DllImport("User32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr parent);

        //开始游戏按钮
        private void Start_Click(object sender, EventArgs e)
        {
            if (game == null || game.IsDisposed)
            {
                game = new Game_Form();
            }
           // game.InitializeGame();//初始化游戏
            game.Show();
            SetParent(game.Handle, this.Handle);
        }

        //游戏商店按钮
        private void sup_mar_Click(object sender, EventArgs e)
        {
            if (market == null || market.IsDisposed)
            {
                market = new Market_Form();
            }
            market.Show();
            SetParent(market.Handle, this.Handle);
        }


        public Game_Form GetStaus()
        {
            return game;
        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (introduction == null || introduction.IsDisposed)
            {
                introduction = new Introduction_Form();
            }
            introduction.Show();
            SetParent(introduction.Handle, this.Handle);
        }
    }
}
