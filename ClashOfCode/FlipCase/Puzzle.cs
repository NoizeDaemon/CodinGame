using System;
using System.Linq;

namespace ClashOfCode.FlipCase;

// Instructions: Flip the case of every letter in the input string.

class Solution
{
    public static void Main()
    {
        var chars = Console.ReadLine();

        var output = chars.Select(c => char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c));

        Console.WriteLine(string.Concat(output));
    }
}