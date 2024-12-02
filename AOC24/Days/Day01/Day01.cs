using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC24.Days.Day01
{
    internal class Day01
    {

        public static void Part01()
        {
            var Input = File.ReadAllLines("test.txt");
            List<int> listA = [];
            List<int> listB = [];

            //Parsing
            foreach (var line in Input)
            {
                var split_line = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                listA.Add(int.Parse(split_line[0]));
                listB.Add(int.Parse(split_line[1]));
            }

            listA.Sort();
            listB.Sort();

            var sum = 0;

            for (int i = 0; i < listA.Count; i++)
                sum += Math.Abs(listA[i] - listB[i]);

            Console.WriteLine(sum);
        }

        public static void Part02()
        {
            var Input = File.ReadAllLines("test.txt");
            List<int> listB = [];
            Dictionary<int, int> dic = [];

            var count = 0;

            //Parsing
            foreach (var line in Input)
            {
                var split_line = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int dig_1 = int.Parse(split_line[0]);
                int dig_2 = int.Parse(split_line[1]);

                
                if(dic.ContainsKey(dig_1))
                    dic[dig_1]++;
                else
                    dic.Add(dig_1, 1);

                listB.Add(dig_2);
            }

            foreach (var number in listB)
            {
                if(dic.ContainsKey(number))
                    count += number * dic[number];
            }

            foreach(var key in dic.Keys)
            {
                Console.WriteLine($"{key} -> {dic[key]}");
            }  

            Console.WriteLine(count);
        }
    }
}
