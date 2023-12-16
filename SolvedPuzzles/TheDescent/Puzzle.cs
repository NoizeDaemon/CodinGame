using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace SolvedPuzzles.TheDescent;

class Player
{
    public static void Main(string[] args)
    {
        while (true)
        {
            (int Index, int Height) highestMountain = (-1, -1);

            for (int i = 0; i < 8; i++)
            {
                int newMountainHeight = int.Parse(Console.ReadLine());
                if (newMountainHeight > highestMountain.Height) highestMountain = (i, newMountainHeight);
            }
            Console.WriteLine(highestMountain.Index);
        }
    }
}