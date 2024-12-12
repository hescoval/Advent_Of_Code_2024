using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{

    internal class Day12
    {
        static DefaultDictionary<char, int> Area = [];
        static DefaultDictionary<char, int> Perimeter = [];
        static HashSet<(int x, int y, (int dirx, int diry))> visited = [];
        private static bool evenDigits(double number)
        {
            return Math.Floor(Math.Log10(number) + 1) % 2 == 0;
        }

        static (int x, int y) MapLimits { get; set; }

        static List<(int x, int y)> Directions =
        [
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0)
        ];

        static List<(int x, int y)> Diags =
        [
           (1, 1),
           (1, -1),
           (-1, 1),
           (-1, -1)
        ];

        private static bool InBounds((int x, int y) coord)
        {
            return coord.x >= 0 && coord.x < MapLimits.x && coord.y >= 0 && coord.y < MapLimits.y;
        }


        private static void FloodFillArea(char[][] input, int x, int y, char target, ref int area, ref int perim)
        {
            if(!InBounds((x, y)))
            {
                perim++;
                return;
            }

            if(input[y][x] == (target - 26))
            {
                return;
            }

            if(input[y][x] != target)
            {
                perim++;
                return;
            }

            input[y][x] = (char)(target - 26);
            area++;
            foreach(var dir in Directions)
            {
                FloodFillArea(input, x + dir.x, y + dir.y, target, ref area, ref perim);
            }
        }
        private static double LookAtMap(char[][] input)
        {
            double result = 0;
            for(int i = 0; i < MapLimits.y; i++)
            {
                for (int j = 0; j < MapLimits.x; j++)
                {
                    if (!char.IsLetter(input[i][j]))
                    {
                        continue;
                    }
                    int area = 0;
                    int perim = 0;
                    FloodFillArea(input, j, i, input[i][j], ref area, ref perim);
                    result += area * perim;
                }
            }
            return result;
        }
        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var charInput = Input.Select(line => line.ToCharArray()).ToArray();
            MapLimits = (Input[0].Length, Input.Length);
            double result = LookAtMap(charInput);

            Console.WriteLine(result);
        }

        private static void GetArea(char[][] input, int x, int y, char target, ref int area)
        {
            if (!InBounds((x, y)))
            {
                return;
            }

            if (input[y][x] == (target - 26))
            {
                return;
            }

            if (input[y][x] != target)
            {
                return;
            }

            input[y][x] = (char)(target - 26);
            area++;
            foreach (var dir in Directions)
            {
                GetArea(input, x + dir.x, y + dir.y, target, ref area);
            }
        }

        private static void GetPerim(char[][] input, int x, int y, char target, DefaultDictionary<(double x, double y), int> corners, HashSet<(int x, int y)> visited)
        {
            if(!InBounds((x, y)))
            {
                return;
            }

            if(visited.Contains((x, y)))
            {
                return;
            }

            if (input[y][x] != target)
            {
                return;
            }

            foreach (var dir in Diags)
            {
                double cornerX = x + ((double)dir.x / 2);
                double cornerY = y + ((double)dir.y / 2);
                if (!InBounds((x + dir.x, y + dir.y)) || input[y + dir.y][x + dir.x] != target)
                {
                    if(corners[(cornerX, cornerY)] > 0)
                    {
                        corners[(cornerX, cornerY)] = 0;
                    }
                    else
                    {
                        corners[(cornerX, cornerY)] = 1;
                    }
                }
                if (InBounds((x + dir.x, y + dir.y)) && input[y + dir.y][x + dir.x] == target)
                {
                    if (input[y][x + dir.x] != target && input[y + dir.y][x] != target)
                    {
                        corners[(cornerX, cornerY)] = 2;
                    }
                }

            }
            visited.Add((x, y));
            foreach (var way in Directions)
            {
                GetPerim(input, x + way.x, y + way.y, target, corners, visited);
            }
        }

        public static void far(char[][]map, char find, char replace)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == find)
                    {
                        map[i][j] = replace;
                    }
                }

            }
        }
        public static double LoopMap(char[][] map)
        {
            double result = 0;
            for (int i = 0; i < MapLimits.y; i++)
            {
                for (int j = 0; j < MapLimits.x; j++)
                {
                    if (!char.IsLetter(map[i][j]))
                    {
                        continue;
                    }
                    DefaultDictionary<(double x, double y), int> corners = [];
                    HashSet<(int x, int y)> visited = [];
                    int area = 0;
                    int perim = 0;
                    GetArea(map, j, i, map[i][j], ref area);
                    GetPerim(map, j, i, map[i][j], corners, visited);
                    far(map, map[i][j], (char)(map[i][j] - 26));


                    foreach (var kvp in corners)
                    {
                        perim += kvp.Value;
                    }
                    visited.Clear();
                    corners.Clear();
                    result += area * perim;
                }
            }
            return result;
        }
        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var charInput = Input.Select(line => line.ToCharArray()).ToArray();
            MapLimits = (Input[0].Length, Input.Length);
            var result = LoopMap(charInput);

            Console.WriteLine(result);
        }
    }
}