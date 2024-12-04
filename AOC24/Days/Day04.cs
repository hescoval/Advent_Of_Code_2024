using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using System.Collections.Specialized;


namespace AOC24.Days
{
    internal class Day04
    {
        private static bool OutOfBounds(string[] input, int x, int y)
        {
            if (x < 0 || y < 0 || y >= input.Length || x >= input[y].Length)
                return true;

            return false;
        }
        private static int RecursiveSearch(string[] input, (int, int) start, (int, int) directions, int curr_s)
        {
            string target = "XMAS";
            var x = start.Item1;
            var y = start.Item2;

            if (OutOfBounds(input, x, y))
                return 0;

            var dir_x = directions.Item1;
            var dir_y = directions.Item2;

            if(input[y][x] != target[curr_s])
                return 0;

            if (curr_s == 3)
                return 1;

            return RecursiveSearch(input, (x + dir_x, y + dir_y), (dir_x, dir_y), curr_s + 1);
        }

        private static int FindMatches(string[] input, List<(int , int)> coords)
        {
            int count = 0;

            List<(int , int)> directions = [(1, 0), (-1, 0), (0, 1), (0, -1), (1, -1), (-1, -1), (-1, 1), (1, 1)];

            foreach (var (x, y) in coords)
            {
                foreach(var (dir_x, dir_y) in directions)
                {
                    count += RecursiveSearch(input, (x + dir_x, y + dir_y), (dir_x, dir_y), 1);
                }
            }
            return count;
        }

        private static List < (int, int) > FindOccur(string[] input, char target)
        {
            List<(int, int)> pairs = [];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == target)
                        pairs.Add((j, i));
                }
            }

            return pairs;
        }

        private static bool FindPattern(string[] input, (int, int) occurence)
        {
            int x = occurence.Item1;
            int y = occurence.Item2;

            if (OutOfBounds(input, x - 1, y - 1) || OutOfBounds(input, x + 1, y + 1))
                return false;

            var up_l = input[y - 1][x - 1];
            var up_r = input[y - 1][x + 1];
            var down_l = input[y + 1][x - 1];
            var down_r = input[y + 1][x + 1];

            if (up_l == 'X' || up_r == 'X' || down_l == 'X' || down_r == 'X')
                return false;

            if(up_l == 'A' || up_r == 'A' || down_l == 'A' || down_r == 'A')
                return false;

            if ((up_l == up_r) && (down_l == down_r) && up_l != down_r)
                return true;

            if((up_l == down_l) && (up_r == down_r) && up_l != up_r)
                return true;

            return false;
        }

        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            List<(int, int)> X_Coordinates = FindOccur(Input, 'X');

            Console.WriteLine(FindMatches(Input, X_Coordinates));
        }


        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            List<(int, int)> A_Coordinates = FindOccur(Input, 'A');

            var count = 0;
            foreach (var (x, y) in A_Coordinates)
            {
                if(FindPattern(Input, (x, y)))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}
