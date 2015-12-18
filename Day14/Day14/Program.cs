using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static string[] input = { "Dancer 27 5 132", "Cupid 22 2 41", "Rudolph 11 5 48", "Donner 28 5 134", "Dasher 4 16 55", "Blitzen 14 3 38", "Prancer 3 21 40", "Comet 18 6 103", "Vixen 18 5 84" };

        static void Main(string[] args)
        {
            int output = 0;

            List<Reindeer> reindeer = new List<Reindeer>();

            foreach(string str in input)
            {
                string[] split = str.Split(' ');

                Reindeer deer = new Reindeer();
                deer.name = split[0];
                deer.velocity = int.Parse(split[1]);
                deer.stamina = int.Parse(split[2]);
                deer.recovery = int.Parse(split[3]);

                reindeer.Add(deer);
            }

            int raceDuration = 2503;

            for (int i = 0; i < raceDuration; i++)
            {
                List<Reindeer> leaders = new List<Reindeer>();

                foreach (Reindeer deer in reindeer)
                {
                    deer.timer++;
                    if(deer.flying)
                    {
                        deer.distance += deer.velocity;
                        if(deer.timer >= deer.stamina)
                        {
                            deer.timer = 0;
                            deer.flying = false;
                        }
                    }
                    else
                    {
                        if (deer.timer >= deer.recovery)
                        {
                            deer.timer = 0;
                            deer.flying = true;
                        }
                    }

                    if (leaders.Count > 0)
                    {
                        if (deer.distance > leaders[0].distance)
                        {
                            leaders.Clear();
                            leaders.Add(deer);
                        }
                        else if (deer.distance == leaders[0].distance)
                        {
                            leaders.Add(deer);
                        }
                    }
                    else
                    {
                        leaders.Add(deer);
                    }
                }

                foreach(Reindeer deer in leaders)
                {
                    deer.points++;
                }
            }

            foreach(Reindeer deer in reindeer)
            {
                output = Math.Max(output, deer.points);
            }

            Console.WriteLine(output);
            Console.ReadLine();
        }
    }

    class Reindeer
    {
        public string name;
        public int velocity;
        public int stamina;
        public int recovery;
        public int points = 0;
        public int distance = 0;
        public int timer = 0;
        public bool flying = true;
    }
}
