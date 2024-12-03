using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AOC24.Days;


internal class Program
{
    private static void Main(string[] args)
    {
        bool is_test = args.Length > 0;
        string file = is_test ? "test.txt" : "input.txt";

        Day03.Part02(file);
    }
}

