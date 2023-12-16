using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


// Source:          https://www.codingame.com/ide/demo/914383a4790fcecb6300cfb6ab110efe92ea15
// Instructions:    Given N integers, return the smallest perfect square number or 0 if none exist.

class Solution
{
    public static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        var numbers = Enumerable.Range(0, N).Select(_ => int.Parse(Console.ReadLine()));

        var result = numbers.Select(n => Math.Sqrt(n))
                            .Where(r => (int)r - r == 0)
                            .DefaultIfEmpty(0)
                            .Min();

        Console.WriteLine(result);
    }
}