using System.Windows.Forms;

namespace 雷电
{
    partial class Game_Form
    {

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer_Game_Invalidate = new System.Windows.Forms.Timer(this.components);
            this.timer_EnemyAutoFire = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer_BGMusic = new System.Windows.Forms.Timer(this.components);
            this.timer_PlayerFire = new System.Windows.Forms.Timer(this.components);
            this.timer_EnemyAppearControl = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer_Game_Invalidate
            // 
            this.timer_Game_Invalidate.Enabled = true;
            this.timer_Game_Invalidate.Interval = 50;
            this.timer_Game_Invalidate.Tick += new System.EventHandler(this.timer_Game_Invalidate_Tick);
            // 
            // timer_EnemyAutoFire
            // 
            this.timer_EnemyAutoFire.Enabled = true;
            this.timer_EnemyAutoFire.Interval = 2000;
            this.timer_EnemyAutoFire.Tick += new System.EventHandler(this.timer_EnemyAutoFire_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer_BGMusic
            // 
            this.timer_BGMusic.Tick += new System.EventHandler(this.timer_BGMusic_Tick);
            // 
            // timer_PlayerFire
            // 
            this.timer_PlayerFire.Enabled = true;
            this.timer_PlayerFire.Interval = 120;
            this.timer_PlayerFire.Tick += new System.EventHandler(this.timer_PlayerFire_Tick);
            // 
            // timer_EnemyAppearControl
            // 
            this.timer_EnemyAppearControl.Enabled = true;
            this.timer_EnemyAppearControl.Tick += new System.EventHandler(this.timer_EnemyAppearControl_Tick);
            // 
            // Game_Form
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(500, 950);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game_Form";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "1";
            this.Load += new System.EventHandler(this.Game_Form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Game_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Game_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Game_MouseMove);
            this.ResumeLayout(false);

        }

        
        #endregion

        private System.Windows.Forms.Timer timer_Game_Invalidate;
        private System.Windows.Forms.Timer timer_EnemyAutoFire;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer_BGMusic;
        private Timer timer_PlayerFire;
        private Timer timer_EnemyAppearControl;
    }
}

