using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days
{
    internal class Day02
    {
        public static void Part01()
        {
            var Input = File.ReadAllLines("test.txt");
            var safe_sum = 0;

            foreach (var line in Input)
            {
                bool safe = true;
                List<int> list = [];
                var split_line = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var number in split_line)
                    list.Add(int.Parse(number));

                if (!Ordered(list))
                    continue;

                for (int i = 0; i < list.Count; i++)
                {
                    if (i < list.Count - 1 && (Math.Abs(list[i] - list[i + 1]) > 3 || Math.Abs(list[i] - list[i + 1]) == 0))
                    {
                        safe = false;
                        break;
                    }
                }

                if (safe)
                    safe_sum++;
            }

            Console.WriteLine(safe_sum);
        }
        public static bool Ordered(List<int> list)
        {
            var asc = list.OrderBy(list => list).ToList();
            var dsc = list.OrderByDescending(list => list).ToList();

            if (list.SequenceEqual(asc) || list.SequenceEqual(dsc))
                return true;

            return false;
        }

        public static bool List_Validator(List<int> list)
        {
            if (!Ordered(list))
                return false;

            for (int i = 0; i < list.Count; i++)
                if (i < list.Count - 1 && (Math.Abs(list[i] - list[i + 1]) > 3 || Math.Abs(list[i] - list[i + 1]) == 0))
                    return false;

            return true;
        }
        public static void Part02()
        {
            var Input = File.ReadAllLines("message.txt");
            var safe_sum = 0;

            foreach (var line in Input)
            {
                List<int> list = [];
                var split_line = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var number in split_line)
                    list.Add(int.Parse(number));

                if (List_Validator(list))
                {
                    safe_sum++;
                    continue;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    var list_copy = new List<int>(list);
                    list_copy.RemoveAt(i);
                    if (List_Validator(list_copy))
                    {
                        safe_sum++;
                        break;
                    }
                }
            }
            Console.WriteLine(safe_sum);
        }
    }
}
