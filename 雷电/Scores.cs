using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 雷电
{
    public interface IGetScores
    {

        void ScorePlus(int p);
    }

    class Scores:IGetScores
    {
        public Scores(int s)
        {
            this.Score = s;
        }
        public int Score
        {
            get;set;
        }
        public void ScorePlus(int p)
        {
            this.Score += p;
        }
    }
}
