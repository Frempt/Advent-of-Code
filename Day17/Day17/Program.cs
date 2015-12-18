using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static string[] input = { "11", "30", "47", "31", "32", "36", "3", "1", "5", "3", "32", "36", "15", "11", "46", "26", "28", "1", "19", "3" };

        static void Main(string[] args)
        {
            int output = 0;

            List<int> containers = new List<int>();
            int target = 150;

            foreach(string str in input)
            {
                containers.Add(int.Parse(str));
            }

            int smallest = FindSmallestCombination(containers, 0, 0, target, int.MaxValue, 0);
            output = FindCombinations(containers, 0, 0, target, smallest, 0);

            Console.WriteLine(output);
            Console.ReadLine();
        }

        static int FindCombinations(List<int> numbers, int currentValue, int startIndex, int targetValue, int desiredSize, int combinationSize)
        {
            int combinations = 0;

            if (currentValue == targetValue && combinationSize == desiredSize) return 1;
            if (currentValue > targetValue || combinationSize > desiredSize) return 0;

            for(int i = startIndex; i < numbers.Count; i++)
            {
                combinations += FindCombinations(numbers, currentValue + numbers[i], i+1, targetValue, desiredSize, combinationSize + 1);
            }

            return combinations;
        }

        static int FindSmallestCombination(List<int> numbers, int currentValue, int startIndex, int targetValue, int smallestCombination, int combinationSize)
        {
            if (currentValue == targetValue)
            {
                return Math.Min(smallestCombination,combinationSize);
            }

            if (currentValue > targetValue) return smallestCombination;

            for (int i = startIndex; i < numbers.Count; i++)
            {
                smallestCombination = FindSmallestCombination(numbers, currentValue + numbers[i], i + 1, targetValue, smallestCombination, combinationSize + 1);
            }

            return smallestCombination;
        }
    }
}
