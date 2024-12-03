using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{
    internal class Day03
    {
        public static void Part01()
        {
            var Input = File.ReadAllLines("test.txt");

            foreach (var line in Input)
            {
                var split_line = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

        }
        public static void Part02()
        {
            var Input = File.ReadAllLines("message.txt");

            foreach (var line in Input)
            {
                var split_line = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
