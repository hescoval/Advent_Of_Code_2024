using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{

    internal class Day07
    {
        private static List<string> PossiblePermutations(int length, char[] ops)
        {
            var result = new List<string>();
            GeneratePermutations("", length, result, ops);
            return result;
        }

        private static void GeneratePermutations(string prefix, int length, List<string> result, char[] ops)
        {
            if (length == 0)
            {
                result.Add(prefix);
                return;
            }

            foreach (var c in ops)
            {
                GeneratePermutations(prefix + c, length - 1, result, ops);
            }
        }

        private static bool PossibleCalculation(List<string> perms, List<double> nums, double target)
        {
            foreach (var perm in perms)
            {
                var result = nums[0];

                for(int i = 1, j = 0; i < nums.Count; i++, j++)
                {
                    switch(perm[j])
                    {
                        case '+':
                            result += nums[i];
                            break;
                        case '*':
                            result *= nums[i];
                            break;
                        case '|':
                        {
                            string result_to_string = result.ToString();
                            string next_num = nums[i].ToString();

                            result = double.Parse(result_to_string + next_num);
                        }
                            break;
                    }

                }
                if(result == target)
                {
                    return true;
                }
            }

                return false;
        }

        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            double result = 0;

            foreach (var line in Input)
            {
                var lineSplit = line.Split(':');
                var target = double.Parse(lineSplit[0]);
                var list = new List<double>();

                foreach (var item in lineSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    list.Add(double.Parse(item));
                }

                var perms = PossiblePermutations(list.Count - 1, ['*', '+']);

                if(PossibleCalculation(perms, list, target))
                {
                    result += target;
                }

            }
            Console.WriteLine(result);
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            double result = 0;

            foreach (var line in Input)
            {
                var lineSplit = line.Split(':');
                var target = double.Parse(lineSplit[0]);
                var list = new List<double>();

                foreach (var item in lineSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    list.Add(double.Parse(item));
                }

                var perms = PossiblePermutations(list.Count - 1, ['*', '+', '|']);

                if (PossibleCalculation(perms, list, target))
                {
                    result += target;
                }

            }
            Console.WriteLine(result);

        }
    }
}
