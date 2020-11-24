using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace 雷电
{
     abstract class LevelDesign
    {
        public LevelDesign()
        {
            
        }

        static int LevelCount=0;//关卡文档每处理一行该值加1
        static public GameLevel Level1 = new GameLevel();

        static public void CreateLevel1()
        {
            StreamReader ReadLevel1 = new StreamReader(@"LevelData\Level1.txt", Encoding.UTF8);//读取level1的关卡设计文档
            string content = ReadLevel1.ReadToEnd();
            string[] lines = content.Split('\n');//以换行为分隔分离文本
            int[] LevelData = new int[lines.Length*4];//每行4个数据存入数组
            int LevelDataIndex = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string pattern = @"\t";//以制表符为界分割每行
                Regex rgx = new Regex(pattern);
                lines[i] = lines[i].Trim();
                string[] DataEachLine = rgx.Split(lines[i], 4);//将每行以遇到的任意个空格为界分成四个字符串
                foreach(var data in DataEachLine)
                {
                    int IntData = 0;
                    try
                    {
                        
                        IntData = Int32.Parse(data);
                    }
                    catch { }                        
                        LevelData[LevelDataIndex] = IntData;
                        LevelDataIndex++;
                                      
                }
                //将数据转化为int类型
            }
            Level1.listenemyAppears.Clear();
            if (LevelCount < lines.Length)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    

                    if (LevelData[4 * i] > 0){
                        Level1.listenemyAppears.Add(new EnemyAppear(LevelData[4 * i], LevelData[4 * i + 1], LevelData[4 * i + 2], LevelData[4 * i + 3]));
                        LevelCount++;
                    }
                }
            }
            Level1.SortByTime();//按时间排序
            /*
            Level1.listenemyAppears.Add(new EnemyAppear(5, 225, -500, 2));
            Level1.listenemyAppears.Add(new EnemyAppear(5, 0, -500, 2));
            Level1.listenemyAppears.Add(new EnemyAppear(5, 500, -500, 2));
            Level1.listenemyAppears.Add(new EnemyAppear(20, 0, 0, 1));
            Level1.listenemyAppears.Add(new EnemyAppear(20, 500, 0, 1));          
            Level1.listenemyAppears.Add(new EnemyAppear(30, 0, 0, 1));
            Level1.listenemyAppears.Add(new EnemyAppear(30, 500, 0, 1));
            Level1.listenemyAppears.Add(new EnemyAppear(40, 0, 0, 1));
            Level1.listenemyAppears.Add(new EnemyAppear(40, 500, 0, 1));
            Level1.listenemyAppears.Add(new EnemyAppear(50, 0, 0, 1));
            Level1.listenemyAppears.Add(new EnemyAppear(50, 500, 0, 1));
            Level1.listenemyAppears.Add(new EnemyAppear(60, 0, 0, 1));           
            Level1.listenemyAppears.Add(new EnemyAppear(60, 500, 0, 1));
            Level1.SortByTime();
            */
        }

    }
}
