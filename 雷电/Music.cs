using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;


namespace 雷电
{
    public class Music
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(
            string command,      //MCI命令字符串
            string returnString, //存放反馈信息的缓冲区
            int returnSize,      //缓冲区的长度
            IntPtr hwndCallback  //回调窗口的句柄，一般为NULL
            );                   //若成功则返回0，否则返回错误码。


        private void PlayWait(string file)
        {
            /*备注
             * open device_name type device_type alias device_alias  打开设备
             * device_name　 　　　要使用的设备名，通常是文件名。
             * type device_type　　设备类型，例如mpegvideo或waveaudio，可省略。
             * alias device_alias　设备别名，指定后可在其他命令中代替设备名。
             */
            mciSendString(string.Format("open \"{0}\" alias media", file), null, 0, IntPtr.Zero);

            /*
             * play device_alias from pos1 to pos2 wait repeat  开始设备播放
             * 若省略from则从当前磁道开始播放。
             * 若省略to则播放到结束。
             * 若指明wait则等到播放完毕命令才返回。即指明wait会产生线程阻塞，直到播放完毕
             * 若指明repeat则会不停的重复播放。
             * 若同时指明wait和repeat则命令不会返回，本线程产生堵塞，通常会引起程序失去响应。
             */
            mciSendString("play media wait", null, 0, IntPtr.Zero);

            //关闭设备
            
            mciSendString("close media", null, 0, IntPtr.Zero);
        }

  
        private void PlayRepeat(string file)
        {
            mciSendString(string.Format("open \"{0}\" alias media", file), null, 0, IntPtr.Zero);
            mciSendString("play media repeat", null, 0, IntPtr.Zero);
        }

        private Thread thread;

        //播放次数0：循环播放 大于0：按指定次数播放
        public void Play(string file, int times)
        {
            //用线程主要是为了解决在播放的时候指定wait时产生线程阻塞,从而导致界面假死的现象
            thread = new Thread(() =>
            {
                if (times == 0)
                {
                    PlayRepeat(file);
                }
                else if (times > 0)
                {
                    for (int i = 0; i < times; i++)
                    {
                        PlayWait(file);
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        /// 结束播放的线程
        public void Exit()
        {
            if (thread != null)
            {
                try
                {
                    thread.Abort();
                }
                catch { }
                thread = null;
            }
        }
    }
}