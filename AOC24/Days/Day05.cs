using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{
    internal class Day05
    {

        private static void ParseInput(string Input, out Dictionary<int, List<int> > rules, out List< List<int> > calibrations)
        {
            rules = [];
            calibrations = [];

            var TwoSidedInput = Input.Split("\n\n");
            var rule_side = TwoSidedInput[0].Split("\n");
            var calibrations_side = TwoSidedInput[1].Split("\n");

            foreach (var line in rule_side)
            {
                var nums = line.Split('|').Select(int.Parse).ToList();

                if (!rules.ContainsKey(nums[0]))
                {
                    rules.Add(nums[0], [ nums[1] ]);
                }
                else
                {
                    rules[ nums[0] ].Add(nums[1]);
                }
            }

            foreach (var line in calibrations_side)
            {
                var split = line.Split(",").Select(int.Parse).ToList();
                calibrations.Add(split);
            }
        }
        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var joinedInput = string.Join("\n", Input);
            ParseInput(joinedInput, out var rules, out var calibrations);

            var sum = 0;
            foreach (var calibration in calibrations)
            {
                bool valid = true;

                for(int i = 0; i < calibration.Count && valid; i++)
                {
                    if (rules.ContainsKey(calibration[i]))
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (rules[calibration[i]].Contains(calibration[j]))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                }

                if(valid)
                {
                    sum += calibration[(calibration.Count / 2)];
                }
            }

            Console.WriteLine(sum);
        }
        private static int FixList(List<int> calibration, Dictionary<int, List<int>> rules)
        { 
            for (int i = 0; i < calibration.Count; i++)
            {
                if (rules.ContainsKey(calibration[i]))
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (rules[calibration[i]].Contains(calibration[j]))
                        {
                            var copy_list = calibration.ToList();
                            (copy_list[j], copy_list[i]) = (copy_list[i], copy_list[j]);
                            return FixList(copy_list, rules);
                        }
                    }
                }
            }
            return calibration[(calibration.Count / 2)];
        }
        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var joinedInput = string.Join("\n", Input);   

            ParseInput(joinedInput, out var rules, out var calibrations);

            var sum = 0;

            foreach (var calibration in calibrations)
            {
                bool valid = true;
                for (int i = 0; i < calibration.Count && valid; i++)
                {
                    if (rules.ContainsKey(calibration[i]))
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (rules[calibration[i]].Contains(calibration[j]))
                            {
                                sum += FixList(calibration, rules);
                                valid = false;
                                break;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(sum);
        }
    }
}
