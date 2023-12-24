using System;

namespace SolvedPuzzles.MarsLander.Episode1;

// Source: https://www.codingame.com/training/easy/mars-lander-episode-1

class Player
{
    const float g = -3.711f;
    const float vSpeedLimit = 40.0f;

    public static int GetDeltaTimeAtFullThrust(int v0, int y0)
    {
        float a = g + 4;
        return v0 * v0 - 2 * a * y0 < 0 ? 0 : (int)((-v0 + Math.Sqrt(v0 * v0 - 2 * a * y0)) / a);
    }

    public static (int vSpeed, int yDelta) GetVerticalDataAtFullThrust(float vSpeed, float yDelta)
    {
        for (int i = 1; i < 4; i++)
        {
            vSpeed += g + i;
            yDelta += vSpeed;
        }
        return ((int)vSpeed, (int)yDelta);
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

        int yDelta, vSpeedAtLanding;
        int turnPower = 0;
        bool isPoweredUp = false;

        while (true)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            (int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power) = (input[0], input[1], input[2], input[3], input[4], input[5], input[6]);

            if (!isPoweredUp)
            {
                yDelta = Y - landingZone.Y;
                var atFullThrust = GetVerticalDataAtFullThrust(vSpeed, yDelta);
                vSpeedAtLanding = (int)(vSpeed + (GetDeltaTimeAtFullThrust(atFullThrust.vSpeed, atFullThrust.yDelta)) * (g + 4));
                if (vSpeedAtLanding >= vSpeedLimit)
                {
                    turnPower = 4;
                    isPoweredUp = true;
                }
            }
            Console.WriteLine($"0 {turnPower}");
        }
    }
}