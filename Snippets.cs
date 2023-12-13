using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinGame;
internal class Snippets
{
    public static int GCD(int a, int b) => b > 0 ? GCD(b, a % b) : a;
    public static int LCM(int a, int b) => a * b / GCD(a, b);
}
