using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 雷电.Properties;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace 雷电
{

    static class Market
    {

        
        public static void InitProduct()
        {

            //suit1
            suit1 = new Suit(Resources.fire2, 100, true);

            //suit2
            suit2 = new Suit(Resources.fire3, 200, false);

            //suit3
            suit3 = new Suit(Resources.fire4, 300, false);

            //suit4
            suit4 = new Suit(Resources.fire6, 300, false);

            //suit5
            suit5 = new Suit(Resources.fire6, 1000, false);

            weapon1 = new Weapon(Resources.fire01, 0, true, 1);
            weapon2 = new Weapon(Resources.fire03, 0, false, 2);
            weapon3 = new Weapon(Resources.laserRed06, 100, false, 2);
            weapon4 = new Weapon(Resources.laserGreen12, 100, false, 2);
            weapon5 = new Weapon(Resources.laserBlue06, 100, false, 2);
            weapon6 = new Weapon(Resources.fire4, 200, false, 3);
            weapon7 = new Weapon(Resources.laserGreen13, 200, false, 3);
            weapon8 = new Weapon(Resources.laserBlue01, 200, false, 3);
            weapon9 = new Weapon(Resources.fire5, 500, false, 4);
            weapon10 = new Weapon(Resources.laserGreen10, 500, false, 4);
            weapon11 = new Weapon(Resources.laserBlue16, 500, false, 4);
            weapon12 = new Weapon(Resources.star2, 0, false, 4);
            weapon13 = new Weapon(Resources.star3, 0, false, 5);
            weapon14 = new Weapon(Resources.star4, 0, false, 6);
        }

        //type=1~5,玩家飞机
        public static Suit suit1;
        public static Suit suit2;
        public static Suit suit3;
        public static Suit suit4;
        public static Suit suit5;

        //设置玩家皮肤
        //返回为-1则购买失败
        //返回为0则表明该皮肤已经购买
        //为1表明购买成功
        public static int SetSuit(int type)
        {
            switch (type)
            {
                case 1:return BuySuit(suit1);
                case 2:return BuySuit(suit2);
                case 3: return BuySuit(suit3);
                case 4: return BuySuit(suit4);
                case 5: return BuySuit(suit5);
                default: return -1;
            }
        }

        private static void SaveBuySuit(Suit s)
        {
            String path = @"..\save.txt";
            if (File.Exists(path))//反序列化 读存档
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                MyFile Loadfile = formatter.Deserialize(stream) as MyFile;
                Loadfile.imageplayer = s.Img;
                stream.Close();
                stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                formatter.Serialize(stream, Loadfile);
                stream.Close();
            }
            else { return; }

        }

        public static int BuySuit(Suit suit)
        {
            if (suit.IfBought) return 0;
            else if (Obj.PlayerProperty >= suit.Price)
            {
                PlanePlayer.imagePlayer = suit.Img;
                SaveBuySuit(suit);
                Obj.PlayerProperty -= suit.Price;
                suit.IfBought = true;
                return 1;
            }
            else
            {
                return -1;
            }
        }

        //武器从type=11开始
        public static Weapon weapon1;
        public static Weapon weapon2;
        public static Weapon weapon3;
        public static Weapon weapon4;
        public static Weapon weapon5;
        public static Weapon weapon6;
        public static Weapon weapon7;
        public static Weapon weapon8;
        public static Weapon weapon9;
        public static Weapon weapon10;
        public static Weapon weapon11;
        public static Weapon weapon12;
        public static Weapon weapon13;
        public static Weapon weapon14;

        public static int SetWeapon(int type)
        {
            switch(type)
            {
                case 11:return BuyWeapon(weapon1);
                case 12: return BuyWeapon(weapon2);
                case 13: return BuyWeapon(weapon3);
                case 14: return BuyWeapon(weapon4);
                case 15: return BuyWeapon(weapon5);
                case 16: return BuyWeapon(weapon6);
                case 17: return BuyWeapon(weapon7);
                case 18: return BuyWeapon(weapon8);
                case 19: return BuyWeapon(weapon9);
                case 20: return BuyWeapon(weapon10);
                case 21: return BuyWeapon(weapon11);
                case 22: return BuyWeapon(weapon12);
                case 23: return BuyWeapon(weapon13);
                case 24: return BuyWeapon(weapon14);
                default: return -1;
            }

        }

        //保存武器购买
        private static void SaveBuy(Weapon w)
        {
            String path = @"..\save.txt";
            if (File.Exists(path))//反序列化 读存档
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                MyFile Loadfile = formatter.Deserialize(stream) as MyFile;
                Loadfile.myweapon = w;
                stream.Close();
                stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                formatter.Serialize(stream, Loadfile);
                stream.Close();
            }
            else { return; }

        }
        public static int BuyWeapon(Weapon w)
        {
            if (w.IfBought) return 0;
            else if(Obj.PlayerProperty>=w.Price)
            {
                //PlayerBullet.imagePB = w.Img;
                //PlayerBullet.power = w.Power;
                PlanePlayer.WeaponType = w;
                SaveBuy(w);
                Obj.PlayerProperty -= w.Price;
                
                w.IfBought = true;
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
