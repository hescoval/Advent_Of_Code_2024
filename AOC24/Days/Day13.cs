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

    internal class Day13
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

        private static double Evaluate(double Ax, double Ay, double Bx, double By, double Px, double Py)
        {
            double ma = ((Px * By) - (Py * Bx)) / ((By * Ax) - (Bx * Ay));
            double mb = ((Px * Ay) - (Py * Ax)) / ((Bx * Ay) - (By * Ax));
            Console.WriteLine($"{ma} and {mb}");

            if(Math.Floor(ma) == ma && Math.Floor(mb) == mb)
                return ma * 3 + mb;
             
            return 0;
        }

        /*
         * 
         * Subject gives us a set of equations that we need to solve.
         * 
         * ax * ma + bx * mb = px
         * bx * ma + by * mb = py
         * 
         * Only ma and mb are unknowns.
         * 
         * Cramer's rule states that
         *                                          
         *                                          | ax bx |        | px bx |       | ax px |
         *                                      D = |   x   |   Dx = |   x   |  Dy = |   x   |
         * ma = Dx / D                              | bx by |        | py by |       | bx py |
         * mb = Dy / D                          
         * 
         * 
         * 
         */
        public static void Part01(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            double Ax = 0;
            double Ay = 0;
            double Bx = 0;
            double By = 0;
            double Px = 0;
            double Py = 0;

            double result = 0;
            for(int i = 0; i < Input.Length; i++)
            {
                var plusSplit = Input[i].Split('+');
                var equalSplit = Input[i].Split('=');

                switch(i % 4)
                {
                    case 0:
                        Ax = double.Parse(Regex.Replace(plusSplit[1], @"\D", ""));
                        Ay = double.Parse(Regex.Replace(plusSplit[2], @"\D", ""));
                        break;
                    case 1:
                        Bx = double.Parse(Regex.Replace(plusSplit[1], @"\D", ""));
                        By = double.Parse(Regex.Replace(plusSplit[2], @"\D", ""));
                        break;
                    case 2:
                        Px = double.Parse(Regex.Replace(equalSplit[1], @"\D", ""));
                        Py = double.Parse(Regex.Replace(equalSplit[2], @"\D", ""));
                        break;
                    case 3:
                        Console.WriteLine($"Variables are : {Ax}, {Ay}, {Bx}, {By}, {Px}, {Py}");
                        result += Evaluate(Ax, Ay, Bx, By, Px, Py);
                        break;
                }
                
            }
            Console.WriteLine(result);
        }

        public static void Part02(string target_file)
        {
            var Input = File.ReadAllLines(target_file);
            double Ax = 0;
            double Ay = 0;
            double Bx = 0;
            double By = 0;
            double Px = 0;
            double Py = 0;

            double result = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                var plusSplit = Input[i].Split('+');
                var equalSplit = Input[i].Split('=');

                switch (i % 4)
                {
                    case 0:
                        Ax = double.Parse(Regex.Replace(plusSplit[1], @"\D", ""));
                        Ay = double.Parse(Regex.Replace(plusSplit[2], @"\D", ""));
                        break;
                    case 1:
                        Bx = double.Parse(Regex.Replace(plusSplit[1], @"\D", ""));
                        By = double.Parse(Regex.Replace(plusSplit[2], @"\D", ""));
                        break;
                    case 2:
                        Px = double.Parse(Regex.Replace(equalSplit[1], @"\D", ""));
                        Py = double.Parse(Regex.Replace(equalSplit[2], @"\D", ""));
                        break;
                    case 3:
                        Console.WriteLine($"Variables are : {Ax}, {Ay}, {Bx}, {By}, {Px}, {Py}");
                        result += Evaluate(Ax, Ay, Bx, By, Px + 10000000000000, Py + 10000000000000);
                        break;
                }

            }
            Console.WriteLine(result);
        }
    }
}