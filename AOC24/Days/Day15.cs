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

    internal class Day15
    {
        static (int x, int y) MapLimits { get; set; }

        static List<(int x, int y)> Directions =
        [
            (0, -1),
            (1, 0),
            (0, 1),
            (-1, 0)
        ];

        static List<char> chars = ['^', '>', 'v', '<'];

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

        public static (int x, int y) FindGuard(char[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    if (map[i][j] == '@')
                    {
                        return (j, i);
                    }
                }
            }

            return (-1, -1);
        }

        public static bool movable((int x, int y) pos, (int x, int y) dir, char[][] map, List<(int x, int y)> to_move)
        {
            if (map[pos.y][pos.x] == '#')
            {
                return false;
            }

            if (map[pos.y][pos.x] == 'O' )
            {
                to_move.Add((pos.x, pos.y));
                if(movable((pos.x + dir.x, pos.y + dir.y), dir, map, to_move))
                    return true;
                return false;
            }
            
            return true;
        }

        public static void moveGuard(ref (int x, int y) guardPos, (int x, int y) direction, char[][] map)
        {
            var newPos = (guardPos.x + direction.x, guardPos.y + direction.y);
            List<(int x, int y)> to_move = [];

            if (movable(newPos, direction, map, to_move))
            {
                guardPos = newPos;
                foreach(var block in to_move)
                {
                    map[block.y + direction.y][block.x + direction.x] = 'O';
                }
                map[guardPos.y][guardPos.x] = '@';
                map[guardPos.y - direction.y][guardPos.x - direction.x] = '.';
            }
        }   

        public static double CountDist(char[][] map)
        {
            double result = 0;
            for(int i = 0; i < map.Length; i++)
            {
                for(int j = 0; j < map[0].Length; j++)
                {
                    if (map[i][j] == 'O' || map[i][j] == '[')
                    {
                        result += 100 * i + j;
                    }
                }
            }

            return result;
        }
        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var full_input = string.Join("\n", Input);
            var map_moves = full_input.Split("\n\n");
            var charmap = map_moves[0].Split("\n").Select(line => line.ToCharArray()).ToArray();
            var moves = string.Join("", map_moves[1]).Replace("\n", "");
            var guardPos = FindGuard(charmap);
            MapLimits = (charmap[0].Length, charmap.Length);

            foreach(var move in moves)
            {
                var direction = Directions[chars.IndexOf(move)];
                moveGuard(ref guardPos, direction, charmap);
            }   
            Console.WriteLine(CountDist(charmap));
        }

        public struct Block
        {
            public (int x, int y) left;
            public (int x, int y) right;
        }

        public static (int x, int y) sumCoords((int x, int y) one, (int x, int y) two)
        {
            return (one.x + two.x, one.y + two.y);
        }

        public static bool MovablePart2((int x, int y) pos, (int x, int y) dir, char[][] map, List<Block> to_move)
        {
            var chr = map[pos.y][pos.x];

            if (chr == '#')
                return false;

            if (chr == '[' || chr == ']')
            {
                Block move = new Block();
                if (chr == '[')
                {
                    move.left = pos;
                    move.right = (pos.x + 1 , pos.y);
                }
                else
                {
                    move.left = (pos.x - 1, pos.y);
                    move.right = pos;
                }

                to_move.Add(move);

                if (dir == (1, 0))
                {
                    if (!MovablePart2(sumCoords(move.right, dir), dir, map, to_move))
                        return false;
                }
                else if(dir == (-1, 0))
                {
                    if (!MovablePart2(sumCoords(move.left, dir), dir, map, to_move))
                        return false;
                }
                else if (!MovablePart2(sumCoords(move.left, dir), dir, map, to_move) || !MovablePart2(sumCoords(move.right, dir), dir, map, to_move))
                    return false;
            }

            return true;
        }


        public static void moveGuardPart2(ref (int x, int y) guardPos, (int x, int y) direction, char[][] map)
        {
            var newPos = (guardPos.x + direction.x, guardPos.y + direction.y);
            List<Block> to_move = [];

            if (MovablePart2(newPos, direction, map, to_move))
            {
                guardPos = newPos;
                foreach(var block in to_move)
                {
                    map[block.left.y][block.left.x] = '.';
                    map[block.right.y][block.right.x] = '.';
                }
                foreach(var block in to_move)
                {
                    map[block.left.y + direction.y][block.left.x + direction.x] = '[';
                    map[block.right.y + direction.y][block.right.x + direction.x] = ']';
                }

                map[guardPos.y][guardPos.x] = '@';
                map[guardPos.y - direction.y][guardPos.x - direction.x] = '.';
            }
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var full_input = string.Join("\n", Input);
            var map_moves = full_input.Split("\n\n");
            var charmap = map_moves[0].Split("\n").Select(line => line.Replace("#", "##").Replace("O", "[]").Replace(".", "..").Replace("@", "@.").ToCharArray()).ToArray();
            var moves = string.Join("", map_moves[1]).Replace("\n", "");
            var guardPos = FindGuard(charmap);
            MapLimits = (charmap[0].Length, charmap.Length);

            foreach (var move in moves)
            {
                var direction = Directions[chars.IndexOf(move)];
                moveGuardPart2(ref guardPos, direction, charmap);
            }

            Console.WriteLine(CountDist(charmap));
        }
    }
}