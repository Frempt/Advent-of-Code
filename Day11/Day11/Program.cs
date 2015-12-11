using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static List<string> alphabet = new List<string>();

        static void Main(string[] args)
        {
            string input = "vzbxxyzz"; //"vzbxkghb";

            string output = "";

            alphabet.Add("a");
            alphabet.Add("b");
            alphabet.Add("c");
            alphabet.Add("d");
            alphabet.Add("e");
            alphabet.Add("f");
            alphabet.Add("g");
            alphabet.Add("h");
            alphabet.Add("i");
            alphabet.Add("j");
            alphabet.Add("k");
            alphabet.Add("l");
            alphabet.Add("m");
            alphabet.Add("n");
            alphabet.Add("o");
            alphabet.Add("p");
            alphabet.Add("q");
            alphabet.Add("r");
            alphabet.Add("s");
            alphabet.Add("t");
            alphabet.Add("u");
            alphabet.Add("v");
            alphabet.Add("w");
            alphabet.Add("x");
            alphabet.Add("y");
            alphabet.Add("z");

            output = IncrementString(input, input.Length - 1);

            while(!IsOkay(output))
            {
                output = IncrementString(output, output.Length - 1);
            }

            Console.WriteLine(output);
            Console.ReadLine();
        }

        public static string IncrementString(string password, int charToIncrement)
        {
            for(int i = charToIncrement; i > 0; i--)
            {
                int letterIndex = alphabet.IndexOf(password[i].ToString());

                if(letterIndex == alphabet.Count - 1)
                {
                    password = password.Remove(i, 1);
                    password = password.Insert(i, alphabet.First());
                }
                else
                {
                    password = password.Remove(i, 1);
                    password = password.Insert(i, alphabet[letterIndex + 1]);

                    return password;
                }
            }

            return password;
        }

        public static bool IsOkay(string password)
        {
            if (password.Contains("i")) return false;
            if (password.Contains("o")) return false;
            if (password.Contains("l")) return false;

            Regex regex = new Regex("(.)\\1");
            if (regex.Matches(password).Count < 2) return false;

            for(int i = 0; i < password.Length - 2; i++)
            {
                string first = password[i].ToString();
                string second = password[i + 1].ToString();
                string third = password[i + 2].ToString();

                if(alphabet.IndexOf(first) == alphabet.IndexOf(second) - 1)
                {
                    if(alphabet.IndexOf(second) == alphabet.IndexOf(third) - 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
