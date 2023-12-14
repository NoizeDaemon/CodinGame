using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/*  
 *                1/n   =    1/x       +   1/y         | x = n+q, y = n+p
 *                1/n   =    1/(n+q)   +   1/(n+p)     | * n(n+p)(n+q)
 *         (n+p)(n+q)   =    n(n+p)    +   n(n+q)
 * n^2 + np + nq + pq   =    n^2 + np  +   n^2 + nq    | -n^2 -np -nq
 *                n^2   =    pq
 */

class Solution
{
    public static void Main(string[] args)
    {
        long n = long.Parse(Console.ReadLine());

        foreach ((long p, long q) in GetPairedFactors(n * n))
        {
            Console.WriteLine($"1/{n} = 1/{n + q} + 1/{n + p}");
        }
    }

    public static IEnumerable<(long p, long q)> GetPairedFactors(long n)
    {
        for (long p = 1, q, r; p * p <= n; p++)
        {
            (q, r) = Math.DivRem(n, p);
            if (r == 0)
            {
                yield return (p, q);
            }
        }
    }
}


