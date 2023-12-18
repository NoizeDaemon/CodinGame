using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Player
{
    const float g = 3.711f;

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
                                                        .Select(s => (int.Parse(s[0]), int.Parse(s[1])));

        // game loop
        while (true)
        {
            //inputs = Console.ReadLine().Split(' ');
            //int X = int.Parse(inputs[0]);
            //int Y = int.Parse(inputs[1]);
            //int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            //int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            //int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            //int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            //int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

            var input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            (int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power) lander = (input[0], input[1], input[2], input[3], input[4], input[5], input[6]);

            int deltaH = lander.Y - surface.Single(p => p.X == lander.X).Y;


            if ()

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4).
            Console.WriteLine("0 3");
        }
    }
}