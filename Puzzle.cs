using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Player
{
    const float g = -3.711f;
    const float vSpeedLimit = -40.0f;

    public static float GetSpeed(float startingSpeed, float acceleration, float timeInSeconds = 1.0f)
    {
        return acceleration * timeInSeconds + startingSpeed;
    }

    static void Main(string[] args)
    {
        var surfacePoints = new (int X, int Y)[int.Parse(Console.ReadLine())];
        (int MinX, int MaxX, int Y) landingZone = (0, 0, 0);

        for (int i = 0; i < surfacePoints.Length; i++)
        {
            var inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            surfacePoints[i] = (inputs[0], inputs[1]);
            
            if (i > 1 && surfacePoints[i - 1].Y == surfacePoints[i].Y)
            {
                landingZone = (surfacePoints[i - 1].X, surfacePoints[i].X, surfacePoints[i].Y);
            }
        }

        int hDelta = -1;
        float targetPower = -1;
        int targetPowerFloor = -1;
        int targetPowerCeiling = -1;
        int turnPower = -1;

        float targetPowerUsed = 0;
        int actualPowerUsed = 0;

        while (true)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            (int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power) = (input[0], input[1], input[2], input[3], input[4], input[5], input[6]);
 

            if (hDelta == -1)
            {
                hDelta = Y - landingZone.Y;
                targetPower = vSpeedLimit * vSpeedLimit / (2 * -hDelta) - g;
                targetPowerFloor = (int)Math.Floor(targetPower);
                targetPowerCeiling = (int)Math.Ceiling(targetPower);
            }

            Console.Error.WriteLine($"s0: {hDelta}, p: {targetPower}");

            targetPowerUsed += (float)targetPower;
            actualPowerUsed += power;

            Console.Error.WriteLine($"Target: {targetPowerUsed}, Actual: {actualPowerUsed}");

            turnPower = actualPowerUsed < targetPowerUsed ? targetPowerCeiling : targetPowerFloor;

            // OLD -> Fuel: 2951

            //int minPower = Math.Max(power - 1, 0);
            //int maxPower = Math.Min(power + 1, 4);

            //Console.Error.WriteLine($"Min: {minPower}, Max: {maxPower}");

            //foreach (int p in Enumerable.Range(minPower, maxPower - minPower + 1)) Console.Error.WriteLine($"{p}, {Math.Abs(GetSpeed(vSpeed, g + (float)p) - vSpeedLimit)}");

            //turnPower = vSpeed + g > vSpeedLimit ? 0 : Enumerable.Range(minPower, maxPower - minPower + 1)
            //                                                       .OrderBy(p => Math.Abs(GetSpeed(vSpeed, g + (float)p) - vSpeedLimit))
            //                                                       .First();


            Console.WriteLine($"0 {turnPower}");
        }
    }
}