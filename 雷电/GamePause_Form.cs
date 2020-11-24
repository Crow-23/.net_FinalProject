using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace 雷电
{
    public partial class GamePause_Form : Form
    {
        
        public GamePause_Form()
        {
            
            InitializeComponent();
        }

        private void GamePause_Form_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Obj.QuestContinue = true;
            this.Dispose(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //退出游戏的操作
            //加积分
            //序列化
          /*  try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream;
                string path = @"D:\LTZJ\log\save.text";
                Menue_Form temp = new Menue_Form();
               
                stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                //Obj datalist = Obj.GetObj();
               // formatter.Serialize(stream, datalist);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message);
            }*/
            Obj.PlayerProperty += (int)(Obj.GetObj().myScores.Score*0.5);
            Obj.GameRun = false;
            Obj.GetObj().ClearData();
            this.Dispose(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Obj.GetObj().time = 0;
            Obj.GetObj().ClearData();
  
            this.Dispose(true);
        }
    }
}
