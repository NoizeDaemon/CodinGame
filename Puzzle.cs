using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    public static void Main(string[] args)
    {
        ulong n = ulong.Parse(Console.ReadLine());

        ulong x = 2 * n;
        ulong y = 2 * n;
        ulong r;

        //while (x <= y * n)
        //{
        //    (y, r) = Math.DivRem(n * x, x - n);
        //    if (r == 0) Console.WriteLine($"1/{n} = 1/{x} + 1/{y}");
        //    x++;
        //}




        for (ulong expandBy = 2; expandBy <= y * n; expandBy++)
        {
            ulong xSplit = expandBy;
            ulong ySplit = 1;
            ulong nExpanded = n * expandBy;
            while (xSplit <= ySplit)
            {
                xSplit -= ySplit++;
                (y, r) = Math.DivRem(nExpanded, ySplit);
                if (r > 0) continue;
                (x, r) = Math.DivRem(nExpanded, xSplit);
                if (r > 0) continue;
                Console.WriteLine($"1/{n} = 1/{x} + 1/{y}");
            }
        }
    }
}