using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{

    internal class Day09
    {
        static (int x, int y) MapLimits { get; set; }

        private static bool InBounds( (int x, int y) coord)
        {
            return coord.x >= 0 && coord.x < MapLimits.x && coord.y >= 0 && coord.y < MapLimits.y;
        }

        private static int digitSum(string line)
        {
            var sum = 0;

            for(int i = 0; i < line.Length; i += 2)
            {
                sum += line[i] - 48;
            }

            return sum;
        }
        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var line = Input[0];

            double id_left = 0;
            double real_index = 0;
            double id_right = (line.Length / 2);
            var left = 0;
            var right = line.Length - 1;
            double result = 0;
            var right_remaining = line[right] - 48;

            while(real_index < digitSum(line))
            {
                var needed = line[left] - 48;

                if (left % 2 == 0)
                {
                    while (needed > 0 && real_index < digitSum(line))
                    {
                        result += real_index * id_left;
                        needed--;
                        real_index++;
                    }
                    id_left++;
                    left++;
                }
                else
                { 
                    while(needed > 0 && real_index < digitSum(line))
                    {
                        result += id_right * real_index;
                        needed--;
                        real_index++;
                        right_remaining--;
                        if(right_remaining == 0)
                        {
                            right -= 2;
                            right_remaining = line[right] - 48;
                            id_right--;
                        }
                    }
                    left++;
                }
            }

            Console.WriteLine(result);
        }


        public static double ArithmeticSeries(int x, int amount, int value)
        {
            int y = x + amount - 1;
            var count = y - x + 1;
            var sum = count * (x + y) / 2;

            return sum * value;
        }

        public static void FetchDictionaries(string line, out Dictionary<int, (int item_id, int amount)> files, out Dictionary<int, int> blankSpaces)
        {
            files = [];
            blankSpaces = [];

            var start_pos = 0;
            var item_id = 0;

            for (int i = 0; i < line.Length; i++)
            {
                var amount = line[i] - 48;
                if (i % 2 == 0)
                {
                    if (amount > 0)
                        files.Add(start_pos, (item_id, amount));
                    item_id++;
                }
                else
                {
                    if (amount > 0)
                        blankSpaces.Add(start_pos, amount);
                }
                start_pos += amount;
            }
        }
        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            var line = Input[0];
            FetchDictionaries(line, out var files, out var blankSpaces);
            Dictionary<int, (int item_id, int amount)> final_list = [];

            double result = 0;


            foreach (var file in files.Reverse())
            {
                int blank_key = -1;
                bool remove = false;
                bool to_add = false;
                (int key, int value) to_Add = (0, 0);

                foreach (var blank in blankSpaces)
                {
                    if (blank.Key > file.Key)
                        break;
                    if (blank.Value >= file.Value.amount)
                    {
                        result += ArithmeticSeries(blank.Key, file.Value.amount, file.Value.item_id);
                        remove = true;
                        blank_key = blank.Key;
                        if (blank.Value > file.Value.amount)
                        {
                            to_add = true;
                            to_Add = (blank.Key + file.Value.amount, blank.Value - file.Value.amount);
                        }
                        break;
                    }
                }

                if (remove)
                {
                    blankSpaces.Remove(blank_key);
                    if (to_add)
                    {
                        blankSpaces.Add(to_Add.key, to_Add.value);
                    }
                }
                else
                {
                    result += ArithmeticSeries(file.Key, file.Value.amount, file.Value.item_id);
                }
            }
            Console.WriteLine(result);
        }
    }
}
