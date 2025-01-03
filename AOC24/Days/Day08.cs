﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{

    internal class Day08
    {
        static (int x, int y) MapLimits { get; set; }

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

        private static bool InBounds( (int x, int y) coord)
        {
            return coord.x >= 0 && coord.x < MapLimits.x && coord.y >= 0 && coord.y < MapLimits.y;
        }

        private static (int x, int y) Delta( (int x, int y) A, (int x, int y) B)
        {
            return (A.x - B.x, A.y - B.y);
        }

        private static void CalculateAntiNodes(List< (int x, int y) > Locs, HashSet< (int x, int y) > antiNodes, bool unique)
        {
            for (int i = 0; i < Locs.Count; i++)
            {
                for (int j = i + 1; j < Locs.Count; j++)
                {
                    if(!unique)
                    {
                        antiNodes.Add(Locs[i]);
                        antiNodes.Add(Locs[j]);
                    }

                    var delta = Delta(Locs[i], Locs[j]);

                    (int x, int y) antiNode1 = (Locs[i].x + delta.x, Locs[i].y + delta.y);
                    (int x, int y) antiNode2 = (Locs[i].x + (2 * -delta.x), Locs[i].y + (2 * -delta.y));

                    while(InBounds(antiNode1))
                    {
                        antiNodes.Add(antiNode1);
                        antiNode1.x += delta.x;
                        antiNode1.y += delta.y;
                        if (unique)
                            break;
                    }

                    while(InBounds(antiNode2))
                    {
                        antiNodes.Add(antiNode2);
                        antiNode2.x += -delta.x;
                        antiNode2.y += -delta.y;
                        if (unique)
                            break;
                    }
                }
            }
        }

        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            MapLimits = (Input[0].Length, Input.Length);

            var Locs = FetchLocations(Input);
            HashSet<(int x, int y)> AntiNodes = [];

            foreach(var loc in Locs)
            {
                List<(int x, int y)> list = loc.Value;
                CalculateAntiNodes(list, AntiNodes, true);
            }

            Console.WriteLine(AntiNodes.Count);
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            MapLimits = (Input[0].Length, Input.Length);

            var Locs = FetchLocations(Input);
            HashSet<(int x, int y)> AntiNodes = [];

            foreach (var loc in Locs)
            {
                List<(int x, int y)> list = loc.Value;
                CalculateAntiNodes(list, AntiNodes, false);
            }
            Console.WriteLine(AntiNodes.Count);
        }
    }
}
