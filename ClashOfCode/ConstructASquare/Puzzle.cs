using System;
using System.Threading.Tasks;

namespace ClashOfCode.ConstructASquare;

// Construct a square made of + chars with side length s:

class Solution
{
    public static void Main(string[] args)
    {
        int s = int.Parse(Console.ReadLine());
        Parallel.For(0, s, _ => Console.WriteLine(new string('+', s)));
    }
}