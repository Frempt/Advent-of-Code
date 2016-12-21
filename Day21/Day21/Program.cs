using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static Character player = new Character() { hp = 100, armour = 0, damage = 0 };
        static Character boss = new Character() { hp = 100, armour = 2, damage = 8 };
        static List<Item> weaponShop = new List<Item>();
        static List<Item> armourShop = new List<Item>();
        static List<Item> ringShop = new List<Item>();
        static string[] inputWeapons = { "8 4 0", "10 5 0", "25 6 0", "40 7 0", "74 8 0" };
        static string[] inputArmour = { "13 0 1", "31 0 2", "53 0 2", "75 0 4", "102 0 5" };
        static string[] inputRings = { "25 1 0", "50 2 0", "100 3 0", "20 0 1", "40 0 2", "80 0 3" };

        static void Main(string[] args)
        {
            int output = int.MinValue;

            foreach (string str in inputWeapons) weaponShop.Add(new Item() { cost = int.Parse(str.Split(' ')[0]), damage = int.Parse(str.Split(' ')[1]) });
            foreach (string str in inputArmour) armourShop.Add(new Item() { cost = int.Parse(str.Split(' ')[0]), armour = int.Parse(str.Split(' ')[2]) });
            foreach (string str in inputRings) ringShop.Add(new Item() { cost = int.Parse(str.Split(' ')[0]), damage = int.Parse(str.Split(' ')[1]), armour = int.Parse(str.Split(' ')[2]) });

            foreach(Item weapon in weaponShop)
            {
                int totalCost = weapon.cost;
                player.damage = weapon.damage;
                player.armour = weapon.armour;

                if(totalCost > output) if (!WillPlayerWin()) output = Math.Max(totalCost, output);

                foreach(Item armour in armourShop)
                {
                    totalCost = weapon.cost + armour.cost;
                    player.damage = weapon.damage;
                    player.armour = armour.armour;

                    //if (totalCost > output) continue;
                    if (totalCost > output) if (!WillPlayerWin()) output = Math.Max(totalCost, output);

                    foreach(Item ring1 in ringShop)
                    {
                        totalCost = weapon.cost + armour.cost + ring1.cost;
                        player.armour = armour.armour + ring1.armour;
                        player.damage = weapon.damage + ring1.damage;

                        //if (totalCost > output) continue;
                        if (totalCost > output) if (!WillPlayerWin()) output = Math.Max(totalCost, output);

                        foreach (Item ring2 in ringShop)
                        {
                            totalCost = weapon.cost + armour.cost + ring1.cost + ring2.cost;
                            player.armour = armour.armour + ring1.armour + ring2.armour;
                            player.damage = weapon.damage + ring1.damage + ring2.damage;

                            //if (totalCost > output) continue;
                            if (totalCost > output) if (!WillPlayerWin()) output = Math.Max(totalCost, output);
                        }
                    }
                }

                foreach (Item ring1 in ringShop)
                {
                    totalCost = weapon.cost + ring1.cost;
                    player.armour = ring1.armour;
                    player.damage = weapon.damage + ring1.damage;

                    //if (totalCost > output) continue;
                    if (totalCost > output) if (!WillPlayerWin()) output = Math.Max(totalCost, output);

                    foreach (Item ring2 in ringShop)
                    {
                        totalCost = weapon.cost + ring1.cost + ring2.cost;
                        player.armour = ring1.armour + ring2.armour;
                        player.damage = weapon.damage + ring1.damage + ring2.damage;

                        //if (totalCost > output) continue;
                        if (totalCost > output) if (!WillPlayerWin()) output = Math.Max(totalCost, output);
                    }
                }
            }

            Console.WriteLine(output);
            Console.ReadLine();
        }

        public static bool WillPlayerWin()
        {
            while (true)
            {
                //player goes first
                boss.TakeDamage(player.damage);
                if (boss.hp <= 0)
                {
                    boss.hp = 100;
                    player.hp = 100;
                    return true;
                }

                player.TakeDamage(boss.damage);
                if (player.hp <= 0)
                {
                    boss.hp = 100;
                    player.hp = 100;
                    return false;
                }
            }
        }

        internal class Character
        {
            public int hp = 0;
            public int damage = 0;
            public int armour = 0;

            public void TakeDamage(int dmg)
            {
                hp -= Math.Max(1, dmg - armour);
            }
        }

        internal class Item
        {
            public int cost = 0;
            public int damage = 0;
            public int armour = 0;
        }
    }
}
