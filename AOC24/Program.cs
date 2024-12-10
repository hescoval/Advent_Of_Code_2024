using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AOC24.Days;


internal class Program
{
    private static void Main(string[] args)
    {
        bool is_real = args.Length > 0;
        string file = is_real ? "input.txt" : "test.txt";

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Day10.Part01(file);
        stopwatch.Stop();
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");

        Console.WriteLine();
        Console.WriteLine("----------------------");
        Console.WriteLine();

        stopwatch = new Stopwatch();
        stopwatch.Start();
        Day10.Part02(file);
        stopwatch.Stop();
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}

