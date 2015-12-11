using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "3113322113";
            int output = 0;

            int numTimes = 50;//40;

            List<int> inputs = new List<int>();

            foreach(char c in input)
            {
                inputs.Add(Convert.ToInt32(c.ToString()));
            }

            for(int i = 0; i < numTimes; i++)
            {
                List<int> newInput = new List<int>();

                for(int j = 0; j < inputs.Count;)
                {
                    int len = 1;

                    for(int k = j + 1; k < inputs.Count; k++)
                    {
                        if(inputs[j] == inputs[k])
                        {
                            len++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    newInput.Add(len);
                    newInput.Add(inputs[j]);

                    j += len;
                }

                inputs = newInput;
            }

            output = inputs.Count;

            Console.WriteLine(output);
            Console.ReadLine();
        }
    }
}
