using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 雷电.Properties;

namespace 雷电
{
    public partial class Market_Form : Form
    {
        
        public Market_Form()
        {
            InitializeComponent();
            Music music_MK = new Music();//进入商店的音效
            music_MK.Play("achievement.mp3", 1);
            Market.InitProduct();
        }

        
        private void Market_Form_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void DoBuySkinWork(int type)
        {
            int temp = Market.SetSuit(type);
            if (temp == 1) MessageBox.Show("购买成功，已为您装载皮肤！");
            else if (temp == -1) MessageBox.Show("您的积分不足。。。");
            else MessageBox.Show("您已购买该皮肤，已为您装载皮肤");
        }

        private void DoBuyWeaWork(int type)
        {
            int temp = Market.SetWeapon(type);
            if (temp == 1) MessageBox.Show("购买成功，已为您装载武器！");
            else if (temp == -1) MessageBox.Show("您的积分不足。。。");
            else MessageBox.Show("您已购买该武器，已为您装载武器");
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            DoBuySkinWork(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DoBuySkinWork(2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DoBuySkinWork(3);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DoBuySkinWork(4);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DoBuySkinWork(5);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(11);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(12);
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(13);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(14);
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(15);
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(16);
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(17);
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(18);
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(19);
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(20);
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(21);
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(22);
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(23);
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            DoBuyWeaWork(24);
        }

        private void skinTabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
