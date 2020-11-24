using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 雷电
{
    public partial class Game_Die : Form
    {
        public Game_Die()
        {
            InitializeComponent();
        }

        private void Game_Die_Load(object sender, EventArgs e)
        {

        }


        /*private void label2_Click(object sender, EventArgs e)
        {

            if (game == null)
            {

                Obj.IsDead = true;
                Obj.GetObj().time = 0;
                Obj.GetObj().ClearData();
                Obj.PlayerProperty += (int)(Obj.GetObj().myScores.Score * 0.5);
                Obj.GameRun = false;
                Obj.GetObj().ClearData();
                this.Dispose(true);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }*/

        private void Game_Die_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)32)
            {
                                Obj.IsDead = true;
                Obj.GetObj().time = 0;
                Obj.GetObj().ClearData();
                Obj.PlayerProperty += (int)(Obj.GetObj().myScores.Score * 0.5);
                Obj.GameRun = false;
                Obj.GetObj().ClearData();
                this.Dispose(true);
            }
        }


    }
}
