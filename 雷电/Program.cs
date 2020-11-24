using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace 雷电
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Welcome wel = new Welcome();
            Application.Run(wel);
            Menue_Form menue = new Menue_Form();
            menue.IsMdiContainer = true;
            Application.Run(menue);
        }
    }
}
