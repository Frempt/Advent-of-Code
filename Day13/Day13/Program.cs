using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static string[] input = { "Alice gain 54 Bob", "Alice lose 81 Carol", "Alice lose 42 David", "Alice gain 89 Eric", "Alice lose 89 Frank", "Alice gain 97 George", "Alice lose 94 Mallory", "Bob gain 3 Alice", "Bob lose 70 Carol", "Bob lose 31 David", "Bob gain 72 Eric", "Bob lose 25 Frank", "Bob lose 95 George", "Bob gain 11 Mallory", "Carol lose 83 Alice", "Carol gain 8 Bob", "Carol gain 35 David", "Carol gain 10 Eric", "Carol gain 61 Frank", "Carol gain 10 George", "Carol gain 29 Mallory", "David gain 67 Alice", "David gain 25 Bob", "David gain 48 Carol", "David lose 65 Eric", "David gain 8 Frank", "David gain 84 George", "David gain 9 Mallory", "Eric lose 51 Alice", "Eric lose 39 Bob", "Eric gain 84 Carol", "Eric lose 98 David", "Eric lose 20 Frank", "Eric lose 6 George", "Eric gain 60 Mallory", "Frank gain 51 Alice", "Frank gain 79 Bob", "Frank gain 88 Carol", "Frank gain 33 David", "Frank gain 43 Eric", "Frank gain 77 George", "Frank lose 3 Mallory", "George lose 14 Alice", "George lose 12 Bob", "George lose 52 Carol", "George gain 14 David", "George lose 62 Eric", "George lose 18 Frank", "George lose 17 Mallory", "Mallory lose 36 Alice", "Mallory gain 76 Bob", "Mallory lose 34 Carol", "Mallory gain 37 David", "Mallory gain 40 Eric", "Mallory gain 18 Frank", "Mallory gain 7 George" };
        static void Main(string[] args)
        {
            int output = 0;

            List<List<string>> pairings = new List<List<string>>();
            List<string> attendees = new List<string>();
            List<SeatingArrangement> arrangements = new List<SeatingArrangement>();

            foreach (string input in input)
            {
                string[] split = input.Split(' ');

                string personA = split[0];
                string personB = split[3];
                int change = int.Parse(split[2]);

                if (split[1] == "lose") change *= -1;

                if (!attendees.Contains(personA)) attendees.Add(personA);
                if (!attendees.Contains(personB)) attendees.Add(personB);

                arrangements.Add(new SeatingArrangement { personA = personA, personB = personB, happinessChange = change });
            }

            attendees.Add("James");

            foreach(string attendee in attendees)
            {
                SeatingArrangement seating1 = new SeatingArrangement();
                seating1.personA = "James";
                seating1.personB = attendee;
                seating1.happinessChange = 0;
                arrangements.Add(seating1);

                SeatingArrangement seating2 = new SeatingArrangement();
                seating2.personB = "James";
                seating2.personA = attendee;
                seating2.happinessChange = 0;
                arrangements.Add(seating2);
            }

            pairings = Permutations(attendees);

            //output = int.MaxValue;
            output = int.MinValue;

            foreach (List<string> pairing in pairings)
            {
                int happiness = 0;

                for (int i = 0; i < pairing.Count; i++)
                {
                    int prevSeat = i - 1;
                    int nextSeat = i + 1;

                    if (prevSeat < 0) prevSeat = pairing.Count - 1;
                    if (nextSeat >= pairing.Count) nextSeat = 0;
                    foreach (SeatingArrangement arrangement in arrangements)
                    {
                        if (arrangement.personA == pairing[i] && (arrangement.personB == pairing[prevSeat] || arrangement.personB == pairing[nextSeat]))
                        {
                            happiness += arrangement.happinessChange;
                        }
                    }
                }

                //output = Math.Min(output, distance);
                output = Math.Max(output, happiness);
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

    class SeatingArrangement : IComparable
    {
        public string personA;
        public string personB;
        public int happinessChange;

        public int CompareTo(object obj)
        {
            return happinessChange.CompareTo(obj);
        }
    }
}
