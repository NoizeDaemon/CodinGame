using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinGame.Misc;
internal class Snippets
{
    // Math

    public static int GCD(int a, int b) => b > 0 ? GCD(b, a % b) : a;
    public static int LCM(int a, int b) => a * b / GCD(a, b);

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


    // Conversions

    public static int ConvertStringToIntFromBase(string s, int fromBase) => Convert.ToInt32(s, fromBase);
    public static string ConvertIntToStringToBase(int value, int toBase) => Convert.ToString(value, toBase);
}
