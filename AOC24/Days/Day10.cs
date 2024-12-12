using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{

    internal class Day10
    {
        static Dictionary<(int x, int y), HashSet<(int x, int y)>> UniqueZeroNine;
        static Dictionary<(int x, int y), int> zeronine;
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

        public static void DFS(string[] input, (int x, int y) current, char required, (int x, int y) start)
        {
            if (!InBounds(current) || input[current.y][current.x] != required)
            {
                return;
            }

            if (required == '9')
            {
                if (!UniqueZeroNine.ContainsKey(start))
                {
                    UniqueZeroNine[start] = [];
                }
                if (zeronine.TryGetValue(start, out int value))
                {
                    zeronine[start] = ++value;
                }
                else
                {
                    zeronine[start] = 1;
                }
                UniqueZeroNine[start].Add(current);
                return;
            }

            foreach (var direction in Directions)
            {
                var next = (current.x + direction.x, current.y + direction.y);
                DFS(input, next, (char)(required + 1), start);
            }
        }

        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            MapLimits = (Input[0].Length, Input.Length);
            zeronine = [];
            UniqueZeroNine = [];

            List<(int x, int y)> Visited = [];

            for (int i = 0; i < MapLimits.y; i++)
            {
                for (int j = 0; j < MapLimits.x; j++)
                {
                    if (Input[i][j] == '0')
                    {
                        foreach (var direction in Directions)
                            DFS(Input, (j + direction.x, i + direction.y), '1', (j, i));
                    }
                }
            }
            var result = 0;
            foreach (var node in UniqueZeroNine)
                result += node.Value.Count;
            Console.WriteLine(result);
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            MapLimits = (Input[0].Length, Input.Length);
            zeronine = [];
            UniqueZeroNine = [];

            for (int i = 0; i < MapLimits.y; i++)
            {
                for (int j = 0; j < MapLimits.x; j++)
                {
                    if (Input[i][j] == '0')
                    {
                        foreach (var direction in Directions)
                            DFS(Input, (j + direction.x, i + direction.y), '1', (j, i));
                    }
                }
            }
            var result = 0;
            foreach (var node in zeronine)
                result += node.Value;

            Console.WriteLine(result);
        }
    }
}