using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{
    public struct Coords
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    internal class Day06
    {
        static List<(int x, int y)> Direction = [(0, -1) , (1, 0), (0, 1), (-1 , 0)];
        static List<char> chars = ['^', '>', 'v', '<'];

        public static string ReplaceAtIndex(string input, int index, char newChar)
        {
            if (index < 0 || index >= input.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        private static bool InBounds(string[] matrix, (int x, int y) coords)
        {
            return coords.x >= 0 && coords.x < matrix[0].Length && coords.y >= 0 && coords.y < matrix.Length;
        }
        private static void find_guard(string[] input, out Coords p_coords)
        {
            p_coords = new Coords();

            for(int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (chars.Contains(input[i][j]))
                    {
                        p_coords.X = j;
                        p_coords.Y = i;
                        return;
                    }
                }
            }
        }



        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var visited = new HashSet<(int x, int y)>();

            find_guard(Input, out var p_coords);
            var direction = Direction[chars.IndexOf(Input[p_coords.Y][p_coords.X])];
            visited.Add((p_coords.X, p_coords.Y));
            
            var next_pos = (p_coords.X + direction.x, p_coords.Y + direction.y);
            while (InBounds(Input, next_pos))
            {
                if (Input[next_pos.Item2][next_pos.Item1] == '#')
                {
                    direction = Direction[(Direction.IndexOf(direction) + 1) % 4];
                    next_pos = (p_coords.X + direction.x, p_coords.Y + direction.y);
                    continue;
                }
                p_coords.X += direction.x;
                p_coords.Y += direction.y;
                visited.Add((p_coords.X, p_coords.Y));

                next_pos = (p_coords.X + direction.x, p_coords.Y + direction.y);
            }

            Console.WriteLine(visited.Count);
        }

        public static bool IsLoop(Coords p_coords, (int x, int y) direction, string[] no_touching, (int x, int y) addWall)
        {
            var Input = (string[])no_touching.Clone();

            Input[addWall.y] = ReplaceAtIndex(Input[addWall.y], addWall.x, '#');
            var visited = new HashSet<(int x, int y, (int x, int y))>();

            var newDirection = Direction[(Direction.IndexOf(direction) + 1) % 4];
            var next_pos = (p_coords.X + newDirection.x, p_coords.Y + newDirection.y);

            while (InBounds(Input, next_pos))
            {
                if (Input[next_pos.Item2][next_pos.Item1] == '#')
                {
                    newDirection = Direction[(Direction.IndexOf(newDirection) + 1) % 4];
                    next_pos = (p_coords.X + newDirection.x, p_coords.Y + newDirection.y);
                    continue;
                }

                p_coords.X += newDirection.x;
                p_coords.Y += newDirection.y;

                if(!visited.Add((p_coords.X, p_coords.Y, newDirection)))
                {
                    return true;
                }

                next_pos = (p_coords.X + newDirection.x, p_coords.Y + newDirection.y);
            }

            return false;
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);

            find_guard(Input, out var p_coords);
            var direction = Direction[chars.IndexOf(Input[p_coords.Y][p_coords.X])];
            HashSet<(int, int)> visited = [];

            visited.Add((p_coords.X, p_coords.Y));
            var loops = 0;
            var next_pos = (p_coords.X + direction.x, p_coords.Y + direction.y);
            while (InBounds(Input, next_pos))
            {
                if (Input[next_pos.Item2][next_pos.Item1] == '#')
                {
                    direction = Direction[(Direction.IndexOf(direction) + 1) % 4];
                    next_pos = (p_coords.X + direction.x, p_coords.Y + direction.y);
                    continue;
                }

                if (!visited.Contains((next_pos.Item1, next_pos.Item2)) && IsLoop(p_coords, direction, Input, next_pos))
                {
                    ++loops;
                }

                p_coords.X += direction.x;
                p_coords.Y += direction.y;
                visited.Add((p_coords.X, p_coords.Y));

                next_pos = (p_coords.X + direction.x, p_coords.Y + direction.y);
            }

            Console.WriteLine(loops);
        }
    }
}
