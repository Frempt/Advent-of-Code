using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static string[] input = { "jio a +19", "inc a", "tpl a", "inc a", "tpl a", "inc a", "tpl a", "tpl a", "inc a", "inc a", "tpl a", "tpl a", "inc a", "inc a", "tpl a", "inc a", "inc a", "tpl a", "jmp +23", "tpl a", "tpl a", "inc a", "inc a", "tpl a", "inc a", "inc a", "tpl a", "inc a", "tpl a", "inc a", "tpl a", "inc a", "tpl a", "inc a", "inc a", "tpl a", "inc a", "inc a", "tpl a", "tpl a", "inc a", "jio a +8", "inc b", "jie a +4", "tpl a", "inc a", "jmp +2", "hlf a", "jmp -7" };

        static void Main(string[] args)
        {
            int a = 1;
            int b = 0;

            for(int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("Instruction #" + i + " - " + input[i]);

                string[] split = input[i].Split(' ');

                if(split[0] == "inc")
                {
                    if (split[1] == "a") a++;
                    else b++;
                }
                else if(split[0] == "hlf")
                {
                    if (split[1] == "a") a /= 2;
                    else b /= 2;
                }
                else if (split[0] == "tpl")
                {
                    if (split[1] == "a") a *= 3;
                    else b *= 3;
                }
                else if (split[0] == "jmp")
                {
                    string sign = split[1].Substring(0, 1);
                    int offset = int.Parse(split[1].Replace(sign, ""));

                    if (sign == "-") offset *= -1;
                    i += offset - 1;
                }
                else if (split[0] == "jie")
                {
                    bool even = false;
                    if (split[1] == "a") even = (a % 2 == 0);
                    else even = (b % 2 == 0);

                    if (even)
                    {
                        string sign = split[2].Substring(0, 1);
                        int offset = int.Parse(split[2].Replace(sign, ""));

                        if (sign == "-") offset *= -1;
                        i += offset - 1;
                    }
                }
                else if (split[0] == "jio")
                {
                    bool run = false;
                    if (split[1] == "a") run = (a == 1);
                    else run = (b == 1);

                    if (run)
                    {
                        string sign = split[2].Substring(0, 1);
                        int offset = int.Parse(split[2].Replace(sign, ""));

                        if (sign == "-") offset *= -1;
                        i += offset - 1;
                    }
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine(a + " " + b);
            Console.ReadLine();
        }
    }
}
