using System;

namespace SolvedPuzzles.PowerOfThor_Episode1;

// Source:          https://www.codingame.com/ide/puzzle/power-of-thor-episode-1
// Instructions:    Given the coordinates of Thor and his LightPower, provide move instructions in 8 directions for Thor to reach his power-up in the allotted number of moves.

class Player
{
    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        (int X, int Y) lightPosition = (int.Parse(inputs[0]), int.Parse(inputs[1]));
        (int X, int Y) thorPosition = (int.Parse(inputs[2]), int.Parse(inputs[3]));

        while (true)
        {
            int remainingTurns = int.Parse(Console.ReadLine());
            Console.WriteLine(NextMove(lightPosition, ref thorPosition));
        }
    }

    public static string NextMove((int X, int Y) lightPosition, ref (int X, int Y) thorPosition)
    {
        string nextMove = string.Empty;

        if (thorPosition.Y > lightPosition.Y)
        {
            nextMove += 'N';
            thorPosition.Y--;
        }
        else if (thorPosition.Y < lightPosition.Y)
        {
            nextMove += 'S';
            thorPosition.Y++;
        }

        if (thorPosition.X < lightPosition.X)
        {
            nextMove += 'E';
            thorPosition.X++;
        }
        else if (thorPosition.X > lightPosition.X)
        {
            nextMove += 'W';
            thorPosition.X--;
        }

        return nextMove;
    }
}