using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day19
{
    class Program
    {
        static string[] input = { "Al => ThF", "Al => ThRnFAr", "B => BCa", "B => TiB", "B => TiRnFAr", "Ca => CaCa", "Ca => PB", "Ca => PRnFAr", "Ca => SiRnFYFAr", "Ca => SiRnMgAr", "Ca => SiTh", "F => CaF", "F => PMg", "F => SiAl", "H => CRnAlAr", "H => CRnFYFYFAr", "H => CRnFYMgAr", "H => CRnMgYFAr", "H => HCa", "H => NRnFYFAr", "H => NRnMgAr", "H => NTh", "H => OB", "H => ORnFAr", "Mg => BF", "Mg => TiMg", "N => CRnFAr", "N => HSi", "O => CRnFYFAr", "O => CRnMgAr", "O => HP", "O => NRnFAr", "O => OTi", "P => CaP", "P => PTi", "P => SiRnFAr", "Si => CaSi", "Th => ThCa", "Ti => BP", "Ti => TiTi", "e => HF", "e => NAl", "e => OMg" };
        static string startMolecule = "CRnSiRnCaPTiMgYCaPTiRnFArSiThFArCaSiThSiThPBCaCaSiRnSiRnTiTiMgArPBCaPMgYPTiRnFArFArCaSiRnBPMgArPRnCaPTiRnFArCaSiThCaCaFArPBCaCaPTiTiRnFArCaSiRnSiAlYSiThRnFArArCaSiRnBFArCaCaSiRnSiThCaCaCaFYCaPTiBCaSiThCaSiThPMgArSiRnCaPBFYCaCaFArCaCaCaCaSiThCaSiRnPRnFArPBSiThPRnFArSiRnMgArCaFYFArCaSiRnSiAlArTiTiTiTiTiTiTiRnPMgArPTiTiTiBSiRnSiAlArTiTiRnPMgArCaFYBPBPTiRnSiRnMgArSiThCaFArCaSiThFArPRnFArCaSiRnTiBSiThSiRnSiAlYCaFArPRnFArSiThCaFArCaCaSiThCaCaCaSiRnPRnCaFArFYPMgArCaPBCaPBSiRnFYPBCaFArCaSiAl";

        static void Main(string[] args)
        {
            int output = 0;

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (string str in input)
            {
                string[] split = str.Split(' ');

                dictionary.Add(split[0], split[2]);
            }

            IOrderedEnumerable<KeyValuePair<string, string>> replacements =
                    dictionary.OrderByDescending(i => i.Value.Length).ThenBy(i => i.Key.Length);

            string target = "e";

            string temp = startMolecule;

            while (temp != target)
            {
                foreach (KeyValuePair<string, string> replacement in replacements)
                {
                    output++;

                    temp = temp.Replace(replacement.Value, replacement.Key);

                    if (temp == target) break;
                }
            }

            Console.WriteLine(output);
            Console.ReadLine();
        }

        static int Part1()
        {
            List<string> molecules = new List<string>();

            foreach (string str in input)
            {
                string[] split = str.Split(' ');

                Regex regex = new Regex(split[0]);
                foreach (Match match in regex.Matches(startMolecule))
                {
                    string newMolecule = startMolecule;

                    newMolecule = newMolecule.Remove(match.Index, match.Length);
                    newMolecule = newMolecule.Insert(match.Index, split[2]);

                    if (!molecules.Contains(newMolecule))
                    {
                        molecules.Add(newMolecule);
                    }
                }
            }

            return molecules.Count;
        }
    }
}
