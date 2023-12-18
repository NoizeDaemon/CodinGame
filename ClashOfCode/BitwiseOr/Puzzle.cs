using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ClashOfCode.BitwiseOr;

// Source:          https://www.codingame.com/ide/demo/72820418b3446b297677aeae57b59bb91bc8f9
// Instructions:    Apply bit-wise or-Operator.

class Solution
{
    public static void Main()
    {
        (var t1, var t2) = (Console.ReadLine(), Console.ReadLine());
        (var n1, var n2) = (Convert.ToInt32(t1, 2), Convert.ToInt32(t2, 2));

        var output = Convert.ToString(n1 | n2, 2);

        Console.WriteLine(output.PadLeft(t1.Length, '0'));
    }
}