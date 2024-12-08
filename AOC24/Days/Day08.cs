using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{

    internal class Day08
    {
        private static Dictionary<char, List<(int x, int y)>> FetchLocations(string[] input)
        {
            Dictionary<char, List<(int x, int y)>> ret = [];


            for(int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (char.IsLetterOrDigit(input[i][j]))
                    {
                        if (!ret.ContainsKey(input[i][j]))
                        {
                            ret[input[i][j]] = new List<(int x, int y)>();
                        }
                        ret[input[i][j]].Add((j, i));
                    }
                }
            }

            return ret;
        }


        private static bool InBounds( (int x, int y) coord, string[] input)
        {
            return coord.x >= 0 && coord.x < input[0].Length && coord.y >= 0 && coord.y < input.Length;
        }

        private static (int x, int y) calcNode1((int x, int y) A, (int x, int y) B)
        {
            var diff_x = A.x - B.x;
            var diff_y = A.y - B.y;
            return (A.x + diff_x, A.y + diff_y);
        }

        private static (int x, int y) calcNode2((int x, int y) A, (int x, int y) B)
        {
            var diff_x = B.x - A.x;
            var diff_y = B.y - A.y;
            return (A.x + 2 * diff_x, A.y + 2 * diff_y);
        }

        private static (int x, int y) delta1((int x, int y) A, (int x, int y) B)
        {
            return (A.x - B.x, A.y - B.y);
        }

        private static (int x, int y) delta2((int x, int y) A, (int x, int y) B)
        {
            return (B.x - A.x, B.y - A.y);
        }

        private static void CalculateAntiNodes(string[] Input, List<(int x, int y)> AntennaLocations, HashSet<(int x, int y)> antiNodes)
        {
            for(int i = 0; i < AntennaLocations.Count; i++)
            {
                for (int j = i + 1; j < AntennaLocations.Count; j++)
                {
                    var x1 = AntennaLocations[i].x;
                    var x2 = AntennaLocations[j].x;
                    var y2 = AntennaLocations[j].y;
                    var y1 = AntennaLocations[i].y;

                    var antiNode1 = calcNode1((x1, y1), (x2, y2));
                    var antiNode2 = calcNode2((x1, y1), (x2, y2));


                    if (InBounds(antiNode1, Input))
                    {
                        antiNodes.Add(antiNode1);
                    }

                    if (InBounds(antiNode2, Input))
                    {
                        antiNodes.Add(antiNode2);
                    }
                }
            }
        }

        private static void CalculateAntiNodes2(string[] Input, List<(int x, int y)> AntennaLocations, HashSet<(int x, int y)> antiNodes)
        {
            for (int i = 0; i < AntennaLocations.Count; i++)
            {
                for (int j = i + 1; j < AntennaLocations.Count; j++)
                {
                    var x1 = AntennaLocations[i].x;
                    var y1 = AntennaLocations[i].y;
                    var x2 = AntennaLocations[j].x;
                    var y2 = AntennaLocations[j].y;

                    var deltaNode1 = delta1((x1, y1), (x2, y2));
                    var deltaNode2 = delta2((x1, y1), (x2, y2));

                    var antiNode1 = (x1 + deltaNode1.x, y1 + deltaNode1.y);
                    var antiNode2 = (x1 + 2 * deltaNode2.x, y1 + 2 * deltaNode2.y);

                    while(InBounds(antiNode1, Input))
                    {
                        antiNodes.Add(antiNode1);
                        antiNode1.Item1 += deltaNode1.x;
                        antiNode1.Item2 += deltaNode1.y;
                    }

                    while(InBounds(antiNode2, Input))
                    {
                        antiNodes.Add(antiNode2);
                        antiNode2.Item1 += deltaNode2.x;
                        antiNode2.Item2 += deltaNode2.y;
                    }
                }
            }
        }

        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var AntennaLocations = FetchLocations(Input);
            HashSet<(int x, int y)> AntiNodes = [];

            foreach(var loc in AntennaLocations)
            {
                List<(int x, int y)> list = loc.Value;
                string joinedValues = string.Join(", ", list.Select(coord => $"({coord.x}, {coord.y})"));
                Console.WriteLine($"Antenna {loc.Key} at {joinedValues}");
                CalculateAntiNodes(Input, list, AntiNodes);
            }

            Console.WriteLine(AntiNodes.Count);
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);

            var AntennaLocations = FetchLocations(Input);
            HashSet<(int x, int y)> AntiNodes = [];

            foreach (var loc in AntennaLocations)
            {
                List<(int x, int y)> list = loc.Value;
                string joinedValues = string.Join(", ", list.Select(coord => $"({coord.x}, {coord.y})"));
                Console.WriteLine($"Antenna {loc.Key} at {joinedValues}");
                foreach(var node in list)
                {
                    AntiNodes.Add((node.x, node.y));
                }
                CalculateAntiNodes2(Input, list, AntiNodes);
            }

            foreach(var node in AntiNodes)
            {
                Console.WriteLine($"AntiNode at {node.x}, {node.y}");
            }
            Console.WriteLine(AntiNodes.Count);
        }
    }
}
