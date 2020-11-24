using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

//不使用
namespace 雷电
{
    class BGMusic
    {
        public static SoundPlayer EnemyBomb = new SoundPlayer(@"sound\get_bomb.wav");
        public static SoundPlayer GetSupply = new SoundPlayer(@"sound\get_supply.wav");
        public static SoundPlayer PlayerBomb = new SoundPlayer(@"sound\use_bomb.wav");
    }
}
