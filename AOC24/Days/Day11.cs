using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{
    internal class Day11
    { 
        static double num_total = 0;
        private static bool evenDigits(double number)
        {
            return Math.Floor(Math.Log10(number) + 1) % 2 == 0;
        }

        static (int x, int y) MapLimits { get; set; }
        static List<(int x, int y)> Directions = new()
        {
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0)
        };

        private static bool InBounds((int x, int y) coord)
        {
            return coord.x >= 0 && coord.x < MapLimits.x && coord.y >= 0 && coord.y < MapLimits.y;
        }

        private static DefaultDictionary<double, double> evaluateList(Dictionary<double, double> number, int blinks)
        {
            DefaultDictionary<double, double> ret = [];

            foreach (var x in number)
            {
                {
                    if (x.Key == 0)
                    {
                        ret[1] += x.Value;
                    }
                    else if (evenDigits(x.Key))
                    {
                        string to_string = x.Key.ToString();
                        int half = to_string.Length / 2;

                        string first_half = to_string.Substring(0, half);
                        string second_half = to_string.Substring(half);

                        var num1 = double.Parse(first_half);
                        var num2 = double.Parse(second_half);

                        ret[num1] += x.Value;
                        ret[num2] += x.Value;
                    }
                    else
                    {
                        ret[x.Key * 2024] += x.Value;
                    }
                }
            }

            return ret;
        }

        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            List<double> InputNumbers = Input[0].Split(" ").Select(double.Parse).ToList();
            DefaultDictionary<double, double> stones = [];
            int num_of_blinks = 25;

            foreach (var number in InputNumbers)
            {
                stones[number] += 1;
            }

            for (int i = 0; i < num_of_blinks; i++)
            {
                stones = evaluateList(stones, i);
            }

            Console.WriteLine(stones.Values.Sum());
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            List<double> InputNumbers = Input[0].Split(" ").Select(double.Parse).ToList();
            DefaultDictionary<double, double> stones = [];
            int num_of_blinks = 75;

            foreach (var number in InputNumbers)
            {
                stones[number] += 1;
            }

            for (int i = 0; i < num_of_blinks; i++)
            {
                stones = evaluateList(stones, i);
            }

            Console.WriteLine(stones.Values.Sum());
        }
    }
}