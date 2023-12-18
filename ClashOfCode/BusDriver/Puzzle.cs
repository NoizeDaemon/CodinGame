using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ClashOfCode.BusDriver;

// Source:          https://www.codingame.com/ide/demo/90146026bc6d71023431038a757f5531c840ce
// Instructions:    Given n bus stops and the number of people who get on and off at each, return the number of people on board after the last stop (including you).

class Solution
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        var deltas = Enumerable.Range(0, n)
                               .Select(_ => Console.ReadLine().Split(' '))
                               .Select(inputs => int.Parse(inputs[0]) - int.Parse(inputs[1]));

        Console.WriteLine(deltas.Sum() + 1);
    }
}