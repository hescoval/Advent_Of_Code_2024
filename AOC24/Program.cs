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

        //Day09.Part01(file);

        Console.WriteLine();
        Console.WriteLine("----------------------");
        Console.WriteLine();

        Day09.Part02(file);
    }
}

