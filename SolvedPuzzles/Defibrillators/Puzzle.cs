using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SolvedPuzzles.Defibrillators;

// Source:          https://www.codingame.com/training/easy/defibrillators
// Instructions:    Given a location in longitude and latitude, return the closest defibrillator from a list

class Solution
{
    static float ParseCommaSeparated(string input) => float.Parse(input.Replace(',', '.'), CultureInfo.InvariantCulture);

    static float GetDistance((float Lon, float Lat) a, (float Lon, float Lat) b)
    {
        var x = (b.Lon - a.Lon) * MathF.Cos((a.Lat + b.Lat) / 2);
        var y = (b.Lat - a.Lat);
        return MathF.Sqrt(x * x + y * y) * 6371;
    }

    public static void Main()
    {
        float lon = ParseCommaSeparated(Console.ReadLine());
        float lat = ParseCommaSeparated(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());

        List<(string Name, float Lon, float Lat)> defibrilators = new();

        for (int i = 0; i < n; i++)
        {
            var defibInfo = Console.ReadLine().Split(';');
            defibrilators.Add((defibInfo[1], ParseCommaSeparated(defibInfo[4]), ParseCommaSeparated(defibInfo[5])));
        }

        var closest = defibrilators.OrderBy(d => GetDistance((lon, lat), (d.Lon, d.Lat))).First();
        Console.WriteLine(closest.Name);
    }
}