using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    class Program
    {
        static Character player = new Character() { hp = 50, armour = 0, damage = 0, mana = 500 };
        static Character boss = new Character() { hp = 55, armour = 0, damage = 8 };
        static int manaSpent = 0;
        static int turn = 0;

        //part 2 1369 lowest so far, too high (1066 too low) (not 1156, 1272, 1309)
        static void Main(string[] args)
        {
            bool? play = null;

            while(play == null)
            {
                turn++;
                Console.WriteLine("Turn " + turn + " start");
                play = TakeTurn();
                Console.WriteLine("Turn " + turn + " end");
            }

            Console.WriteLine(play + " " + manaSpent);
            Console.ReadLine();
        }

        public static bool? TakeTurn()
        {
            bool rechargeActive = false;
            bool shieldActive = false;
            bool poisonActive = false;

            Console.WriteLine("Player HP: " + player.hp + " Mana: " + player.mana);
            Console.WriteLine("Boss HP: " + boss.hp);

            player.hp--;
            Console.WriteLine("Player loses 1 hp");
            if (player.hp <= 0) { Console.WriteLine("Player ran out of HP"); return false; }

            for (int i = 0; i < boss.effects.Count; i++)
            {
                Effect effect = boss.effects[i];

                boss.hp += effect.hpChange;
                poisonActive = true;

                Console.WriteLine("Boss loses 3hp to poison");

                effect.timer--;
                if (effect.timer <= 0)
                {
                    poisonActive = false;
                    boss.effects.Remove(effect);
                }
            }

            if (boss.hp <= 0) { Console.WriteLine("Boss ran out of HP"); return true; }
            
            player.armour = 0;
            for (int i = 0; i < player.effects.Count; i++)
            {
                Effect effect = player.effects[i];

                if (effect.manaChange != 0)
                {
                    player.mana += effect.manaChange;
                    rechargeActive = true;
                    Console.WriteLine("Player gains " + effect.manaChange + " mana.");
                }

                if (effect.armour != 0)
                {
                    player.armour = effect.armour;
                    shieldActive = true;
                }

                effect.timer--;
                if (effect.timer <= 0)
                {
                    if (effect.manaChange != 0) rechargeActive = false;
                    if (effect.armour != 0) shieldActive = false;
                    player.effects.Remove(effect);
                }
            }

            Console.WriteLine("Player HP: " + player.hp + " Mana: " + player.mana);
            Console.WriteLine("Boss HP: " + boss.hp);

            switch(turn)
            {
                case 1:
                case 4:
                case 7:
                    Console.WriteLine("Player cast Poison");
                    boss.effects.Add(new Effect() { hpChange = -3, timer = 6 });
                    player.mana -= 173;
                    manaSpent += 173;
                    break;

                case 2:
                case 6:
                    Console.WriteLine("Player cast Recharge");
                    player.effects.Add(new Effect() { manaChange = 101, timer = 5 });
                    player.mana -= 229;
                    manaSpent += 229;
                    break;

                //case 3:
                //case 6:
                case 5:
                    Console.WriteLine("Player cast Shield");
                    player.effects.Add(new Effect() { armour = 7, timer = 5 });
                    player.mana -= 113;
                    manaSpent += 113;
                    break;

                case 3:
                case 8:
                    Console.WriteLine("Player cast Drain");
                    boss.TakeDamage(2);
                    player.hp += 2;
                    player.mana -= 73;
                    manaSpent += 73;
                    break;

                case 9:
                    Console.WriteLine("Player cast Magic Missile");
                    boss.TakeDamage(4);
                    player.mana -= 53;
                    manaSpent += 53;
                    break;

                default:
                    if (!shieldActive && player.hp < 25 && (player.mana > 400 || player.hp < 10) && boss.hp > 4)
                    {
                        Console.WriteLine("Player cast Shield");
                        player.effects.Add(new Effect() { armour = 7, timer = 5 });
                        player.mana -= 113;
                        manaSpent += 113;
                    }
                    else if (player.hp < 6 && boss.hp > 4 && (player.hp < 3 || player.mana > 300))
                    {
                        Console.WriteLine("Player cast Drain");
                        boss.TakeDamage(2);
                        player.hp += 2;
                        player.mana -= 73;
                        manaSpent += 73;
                    }
                    else if (!rechargeActive && boss.hp > 10)
                    {
                        Console.WriteLine("Player cast Recharge");
                        player.effects.Add(new Effect() { manaChange = 101, timer = 5 });
                        player.mana -= 229;
                        manaSpent += 229;
                    }
                    else if (!poisonActive && boss.hp > 10 && player.hp > 10)
                    {
                        Console.WriteLine("Player cast Poison");
                        boss.effects.Add(new Effect() { hpChange = -3, timer = 6 });
                        player.mana -= 173;
                        manaSpent += 173;
                    }
                    else
                    {
                        Console.WriteLine("Player cast Magic Missile");
                        boss.TakeDamage(4);
                        player.mana -= 53;
                        manaSpent += 53;
                    }
                    break;
            }

            //player goes first
            

            if (player.mana <= 0) { Console.WriteLine("Player ran out of mana"); return false; }
            if (boss.hp <= 0) { Console.WriteLine("Boss ran out of HP"); return true; }

            Console.WriteLine("Player HP: " + player.hp + " Mana: " + player.mana);
            Console.WriteLine("Boss HP: " + boss.hp);
            for (int i = 0; i < boss.effects.Count; i++)
            {
                Effect effect = boss.effects[i];

                boss.hp += effect.hpChange;
                poisonActive = true;

                Console.WriteLine("Boss loses 3hp to poison");

                effect.timer--;
                if (effect.timer <= 0)
                {
                    poisonActive = false;
                    boss.effects.Remove(effect);
                }
            }

            if (boss.hp <= 0) { Console.WriteLine("Boss ran out of HP"); return true; }

            player.armour = 0;
            for (int i = 0; i < player.effects.Count; i++)
            {
                Effect effect = player.effects[i];

                if (effect.manaChange != 0)
                {
                    player.mana += effect.manaChange;
                    rechargeActive = true;
                    Console.WriteLine("Player gains " + effect.manaChange + " mana.");
                }

                if (effect.armour != 0)
                {
                    player.armour = effect.armour;
                    shieldActive = true;
                }

                effect.timer--;
                if (effect.timer <= 0)
                {
                    if (effect.manaChange != 0) rechargeActive = false;
                    if (effect.armour != 0) shieldActive = false;
                    player.effects.Remove(effect);
                }
            }

            Console.WriteLine("Player HP: " + player.hp + " Mana: " + player.mana);
            Console.WriteLine("Boss HP: " + boss.hp);

            Console.WriteLine("Boss attacks");
            player.TakeDamage(boss.damage);
            if (player.hp <= 0) { Console.WriteLine("Player ran out of HP"); return false; }

            Console.WriteLine("Player HP: " + player.hp + " Mana: " + player.mana);
            Console.WriteLine("Boss HP: " + boss.hp);

            return null;
        }

        internal class Character
        {
            public int hp = 0;
            public int mana = 0;
            public int damage = 0;
            public int armour = 0;
            public List<Effect> effects = new List<Effect>();

            public void TakeDamage(int dmg)
            {
                hp -= Math.Max(1, dmg - armour);
            }
        }

        internal class Effect
        {
            public int hpChange = 0;
            public int manaChange = 0;
            public int armour = 0;
            public int timer = 0;
        }
    }
}
