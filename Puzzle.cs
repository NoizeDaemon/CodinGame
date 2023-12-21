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
        string[] inputs;
        int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        //for (int i = 0; i < surfaceN; i++)
        //{
        //    inputs = Console.ReadLine().Split(' ');
        //    int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
        //    int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
        //}

        IEnumerable<(int X, int Y)> surface = Enumerable.Range(0, surfaceN)
                                                        .Select(_ => Console.ReadLine().Split(' '))
                                                        .Select(s => (int.Parse(s[0]), int.Parse(s[1])))
                                                        .ToArray();

        // game loop
        while (true)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            (int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power) = (input[0], input[1], input[2], input[3], input[4], input[5], input[6]);

            int minPower = Math.Max(power - 1, 0);
            int maxPower = Math.Min(power + 1, 4);

            Console.Error.WriteLine($"Min: {minPower}, Max: {maxPower}");

            foreach(int p in Enumerable.Range(minPower, maxPower - minPower + 1)) Console.Error.WriteLine($"{p}, {Math.Abs(GetSpeed(vSpeed, g + (float)p) - vSpeedLimit)}");

            var targetPower = vSpeed + g > vSpeedLimit ? 0 : Enumerable.Range(minPower, maxPower - minPower + 1)
                                                                   .OrderBy(p => Math.Abs(GetSpeed(vSpeed, g + (float)p) - vSpeedLimit))
                                                                   .First();

            targetPower = Math.Min(fuel, targetPower);
            
            Console.WriteLine($"0 {targetPower}");
        }
    }
}