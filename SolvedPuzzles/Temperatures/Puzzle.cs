using System;
using System.Linq;

namespace SolvedPuzzles.Temperatures;

// Source:          https://www.codingame.com/training/easy/temperatures
// Instructions:    Given n space-seperated temperatures, find the closest to 0, positive bias, or return 0 if n = 0

class Solution
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        var result = n == 0 ? 0 
            : Console.ReadLine().Split(' ')
                .Select(int.Parse)
                .OrderBy(Math.Abs)
                .ThenByDescending(Math.Sign)
                .First();

        Console.WriteLine(result);
    }
}