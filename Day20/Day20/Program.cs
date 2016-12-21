using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {
        static int input = 33100000;

        static void Main(string[] args)
        {
            int output = 0;

            int houseNumber = 1;
            while (output == 0)
            {
                int numPresents = 0;

                numPresents = SumOfDivisors(houseNumber) * 10;

                if(numPresents >= input)
                {
                    output = houseNumber;
                }

                houseNumber++;
            }

            Console.WriteLine(output);
            Console.ReadLine();
        }

        public static int SumOfDivisors(int n)
        {
            if (n == 0 || n == 1) return n;

            int total = 1;

            for (int i = 2; (i)*(i) <= n; i++)
            {
                if (n % i == 0)
                {
                    int nOverI = n / i;

                    total += i;

                    if (i != nOverI)
                    {
                        total += nOverI;
                    }
                }
            }

            return total + n;
        }
    }
}
