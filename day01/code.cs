using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.day01
{
    internal class day01
    {

        public static void Part01()
        {
            var Input = File.ReadAllLines("test.txt");
            List<int> listA = new();
            List<int> listB = new();

            //Parsing
            foreach(var line in Input)
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
            List<int> listA = new();
            List<int> listB = new();


            //Parsing
            foreach (var line in Input)
            {
                var split_line = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                listA.Add(int.Parse(split_line[0]));
                listB.Add(int.Parse(split_line[1]));
            }


            var total = 0;
            for (int i = 0; i < listA.Count; i++)
            { 
                var count = listB.Count(x => x == listA[i]);
                total += listA[i] * count;
            }

            Console.WriteLine(total);
        }
    }
}
