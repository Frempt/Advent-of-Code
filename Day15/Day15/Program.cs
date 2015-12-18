using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        static string[] input = { "Frosting capacity 4 durability -2 flavor 0 texture 0 calories 5", "Candy capacity 0 durability 5 flavor -1 texture 0 calories 8", "Butterscotch capacity -1 durability 0 flavor 5 texture 0 calories 6", "Sugar capacity 0 durability 0 flavor -2 texture 2 calories 1" };

        static void Main(string[] args)
        {
            int output = 0;

            List<Ingredient> ingredients = new List<Ingredient>();

            foreach(string str in input)
            {
                string[] split = str.Split(' ');

                Ingredient ingredient = new Ingredient();
                ingredient.name = split[0];
                ingredient.capacity = int.Parse(split[2]);
                ingredient.durability = int.Parse(split[4]);
                ingredient.flavour = int.Parse(split[6]);
                ingredient.texture = int.Parse(split[8]);
                ingredient.calories = int.Parse(split[10]);

                ingredients.Add(ingredient);
            }
            int teaspoons = 100;

            //List<List<int>> partitions = GetPartitions(teaspoons, teaspoons, teaspoons, null);

            //foreach(List<int> partition in partitions)
            //{
            for(int a = 0; a < teaspoons + 1; a++)
            {
                for (int b = 0; b < teaspoons + 1 - a; b++)
                {
                    for (int c = 0; c < teaspoons + 1 - (a + b); c++)
                    {
                        for (int d = 0; d < teaspoons + 1 - (a + b + c); d++)
                        {
                            if ((a + b + c + d) != 100) continue;
                            List<int> partition = new List<int>();
                            partition.Add(a);
                            partition.Add(b);
                            partition.Add(c);
                            partition.Add(d);

                            int capacity = 0;
                            int durability = 0;
                            int flavour = 0;
                            int texture = 0;
                            int calories = 0;

                            capacity += ingredients[0].capacity * a;
                            durability += ingredients[0].durability * a;
                            flavour += ingredients[0].flavour * a;
                            texture += ingredients[0].texture * a;
                            calories += ingredients[0].calories * a;

                            capacity += ingredients[1].capacity * b;
                            durability += ingredients[1].durability * b;
                            flavour += ingredients[1].flavour * b;
                            texture += ingredients[1].texture * b;
                            calories += ingredients[1].calories * b;

                            capacity += ingredients[2].capacity * c;
                            durability += ingredients[2].durability * c;
                            flavour += ingredients[2].flavour * c;
                            texture += ingredients[2].texture * c;
                            calories += ingredients[2].calories * c;

                            capacity += ingredients[3].capacity * d;
                            durability += ingredients[3].durability * d;
                            flavour += ingredients[3].flavour * d;
                            texture += ingredients[3].texture * d;
                            calories += ingredients[3].calories * d;

                            if (calories == 500)
                            {
                                int score = (Math.Max(capacity, 0) * Math.Max(durability, 0) * Math.Max(flavour, 0) * Math.Max(texture, 0));

                                output = Math.Max(score, output);
                            }
                        }
                    }
                }
            }  

            Console.WriteLine(output);
            Console.ReadLine();
        }

        public static List<List<int>> GetPartitions(int n, int max, int fullTarget, List<int> previous)
        {
            List<List<int>> partitions = new List<List<int>>();

            if(n != 0)
            {
                for(int i = Math.Min(max, n); i >= 1; i--)
                {
                    if (previous == null)
                    {
                        List<int> tempList = new List<int>();
                        tempList.Add(i);
                        partitions.AddRange(GetPartitions(n - i, i, fullTarget, tempList));
                    }
                    else
                    {
                        if (previous.Sum() < fullTarget)
                        {
                            previous.Add(i);
                            partitions.AddRange(GetPartitions(n - i, i, fullTarget, previous));
                        }
                    }
                }
            }
            else
            {
                partitions.Add(previous);
            }

            return partitions;
        }

        public static List<List<int>> GetPermutations(List<int> values)
        {
            List<List<int>> permutations = new List<List<int>>();

            if (values.Count == 0)
            {
                return permutations; // Empty list.
            }

            int factorial = Factorial(values.Count);

            for (int i = 0; i < factorial; i++)
            {
                List<int> set = new List<int>(values);
                int k = i;
                for (int j = 2; j <= values.Count; j++)
                {
                    int other = (k % j);
                    int temp = set[j - 1];
                    set[j - 1] = set[other];
                    set[other] = temp;
                    k = k / j;
                }
                permutations.Add(set);
            }

            return permutations;
        }

        public static int Factorial(int number)
        {
            if (number <= 1)
            {
                return 1;
            }
            else
            {
                return number * Factorial(number - 1);
            }
        }
    }

    class Ingredient
    {
        public string name;
        public int capacity;
        public int durability;
        public int calories;
        public int flavour;
        public int texture;
    }
}
