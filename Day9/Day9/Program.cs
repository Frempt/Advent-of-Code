using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static string[] inputs = { "AlphaCentauri to Snowdin = 66", "AlphaCentauri to Tambi = 28", "AlphaCentauri to Faerun = 60", "AlphaCentauri to Norrath = 34", "AlphaCentauri to Straylight = 34", "AlphaCentauri to Tristram = 3", "AlphaCentauri to Arbre = 108", "Snowdin to Tambi = 22", "Snowdin to Faerun = 12", "Snowdin to Norrath = 91", "Snowdin to Straylight = 121", "Snowdin to Tristram = 111", "Snowdin to Arbre = 71", "Tambi to Faerun = 39", "Tambi to Norrath = 113", "Tambi to Straylight = 130", "Tambi to Tristram = 35", "Tambi to Arbre = 40", "Faerun to Norrath = 63", "Faerun to Straylight = 21", "Faerun to Tristram = 57", "Faerun to Arbre = 83", "Norrath to Straylight = 9", "Norrath to Tristram = 50", "Norrath to Arbre = 60", "Straylight to Tristram = 27", "Straylight to Arbre = 81", "Tristram to Arbre = 90" };

        static void Main(string[] args)
        {
            int output = 0;

            List<List<string>> paths = new List<List<string>>();
            List<string> locations = new List<string>();
            List<Route> routes = new List<Route>();

            foreach(string input in inputs)
            {
                string[] split = input.Split(' ');

                string source = split[0];
                string destination = split[2];
                int distance = int.Parse(split[4]);

                if (!locations.Contains(source)) locations.Add(source);
                if (!locations.Contains(destination)) locations.Add(destination);

                routes.Add(new Route { destination = destination, source = source, distance = distance });
            }

            paths = Permutations(locations);

            //output = int.MaxValue;
            output = int.MinValue;

            foreach(List<string> path in paths)
            {
                int distance = 0;

                for(int i = 0; i < path.Count - 1; i++)
                {
                    foreach(Route route in routes)
                    {
                        if((route.source == path[i] && route.destination == path[i + 1]) || (route.destination == path[i] && route.source == path[i+1]))
                        {
                            distance += route.distance;
                            break;
                        }
                    }
                }

                //output = Math.Min(output, distance);
                output = Math.Max(output, distance);
            }

            Console.WriteLine(output);
            Console.ReadLine();
        }

        public static List<List<string>> Permutations(List<string> values)
        {
            List<List<string>> permutations = new List<List<string>>();

            if (values.Count == 0)
            {
                return permutations; // Empty list.
            }

            int factorial = Factorial(values.Count);

            for (int i = 0; i < factorial; i++)
            {
                List<string> set = new List<string>(values);
                int k = i;
                for (int j = 2; j <= values.Count; j++)
                {
                    int other = (k % j);
                    string temp = set[j - 1];
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

    class Route : IComparable
    {
        public string source;
        public string destination;
        public int distance;

        public int CompareTo(object obj)
        {
            return distance.CompareTo(obj);
        }
    }
}
