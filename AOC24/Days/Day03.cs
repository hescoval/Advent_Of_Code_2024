using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace AOC24.Days
{
    internal class Day03
    {
        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var full_input = string.Join("", Input);

            string pattern = @"mul\((\d+),(\d+)\)";

            var total = 0;

            Regex regex = new (pattern);
            MatchCollection matches = regex.Matches(full_input);

            foreach (Match match in matches)
            {
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                total += x * y;
            }

            Console.WriteLine(total);
        }

        public static bool FindSwitch(MatchCollection switches, int index)
        {
            string last_match = "";
            foreach(Match toggle in switches)
            {
                if (toggle.Index > index)
                    break;
                last_match = toggle.Value;
            }


            if (last_match == "do()" || last_match == "")
            {
                return true;
            }
            return false;
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var full_input = string.Join("", Input);

            string regx = @"mul\((\d+),(\d+)\)";
            string regx_switches = @"do\(\)|don't\(\)";

            var total = 0;

            Regex regex = new (regx);
            Regex switch_regex = new (regx_switches);
            MatchCollection instruction_matches = regex.Matches(full_input);
            MatchCollection switch_matches = switch_regex.Matches(full_input);

            foreach (Match match in instruction_matches)
            {
                var enabled = FindSwitch(switch_matches, match.Index);

                if (enabled)
                {
                    var x = int.Parse(match.Groups[1].Value);
                    var y = int.Parse(match.Groups[2].Value);
                    total += x * y;
                }
            }
            Console.WriteLine(total);
        }
    }
}
