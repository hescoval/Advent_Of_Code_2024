using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC24.Days
{

    internal class Day14
    {
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

        public static (int x, int y) GetFinalPos(int start_x, int start_y, int speed_x, int speed_y)
        {
            var finalX = ((start_x + (speed_x * 100)) % MapLimits.x + MapLimits.x) % MapLimits.x;
            var finalY = ((start_y + (speed_y * 100)) % MapLimits.y + MapLimits.y) % MapLimits.y;

            return (finalX, finalY);
        }

        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            MapLimits = (101, 103);
            DefaultDictionary<(int x, int y), int> FinalPos = [];

            foreach (var line in Input)
            {
                var parts = line.Split(" ");
                var cleanedLine = Regex.Replace(line, @"[^\d-]", " ");
                var splitClean = cleanedLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var start_x = int.Parse(splitClean[0]);
                var start_y = int.Parse(splitClean[1]);
                var speed_x = int.Parse(splitClean[2]);
                var speed_y = int.Parse(splitClean[3]);

                FinalPos[GetFinalPos(start_x, start_y, speed_x, speed_y)]++;
            }

            DefaultDictionary<int, int> quadrants = [];

            foreach(var kvp in FinalPos)
            {
                Console.WriteLine($"{kvp.Key.x}, {kvp.Key.y} : {kvp.Value}");
                var posx = kvp.Key.x;
                var posy = kvp.Key.y;
                if (posx == MapLimits.x / 2 || posy == MapLimits.y / 2)
                    continue;

                if (posx < MapLimits.x / 2 && posy < MapLimits.y / 2)
                    quadrants[1] += kvp.Value;
                else if (posx > MapLimits.x / 2 && posy < MapLimits.y / 2)
                    quadrants[2] += kvp.Value;
                else if (posx < MapLimits.x / 2 && posy > MapLimits.y / 2)
                    quadrants[3] += kvp.Value;
                else if (posx > MapLimits.x / 2 && posy > MapLimits.y / 2)
                    quadrants[4] += kvp.Value;
            }

            var result = 1;

            foreach (var kvp in quadrants)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value}");
                result *= kvp.Value;
            }

            Console.WriteLine(result);
        }

        public static void updatePositions(List<(int x, int y)> pos, List<(int velx, int vely)> vels)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                pos[i] = (((pos[i].x + vels[i].velx) % MapLimits.x + MapLimits.x) % MapLimits.x, ((pos[i].y + vels[i].vely) % MapLimits.y + MapLimits.y) % MapLimits.y);
            }
        }

        public static void printPositions(List<(int x, int y)> positions)
        {
            for(int i = 0; i < MapLimits.x; i++)
            {
                for(int j = 0; j < MapLimits.y; j++)
                {
                    if(positions.Contains((j, i)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            MapLimits = (101, 103);
            List<(int velx, int vely)> vels = [];
            List<(int x, int y)> positions = [];

            foreach (var line in Input)
            {
                var parts = line.Split(" ");
                var cleanedLine = Regex.Replace(line, @"[^\d-]", " ");
                var splitClean = cleanedLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var start_x = int.Parse(splitClean[0]);
                var start_y = int.Parse(splitClean[1]);
                var speed_x = int.Parse(splitClean[2]);
                var speed_y = int.Parse(splitClean[3]);

                vels.Add((speed_x, speed_y));
                positions.Add((start_x, start_y));
            }

            var loops = 0;
            while(true)
            {
                if((loops - 40) % 103 == 0 || (loops - 99) % 101 == 0)
                {
                    printPositions(positions);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"LOOP NUMBER {loops}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                updatePositions(positions, vels);
                loops++;
            }
        }
    }
}