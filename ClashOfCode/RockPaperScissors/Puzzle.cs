using System;

namespace ClashOfCode.RockPaperScissors;

class Solution
{
    public static void Main()
    {
        string[] inputs = Console.ReadLine().Split(' ');
        (string player1, string player2) state = (inputs[0], inputs[1]);

        int winner = state.player1 == state.player2 ? 0 : state switch
        {
            ("ROCK", "SCISSORS") => 1,
            ("PAPER", "ROCK") => 1,
            ("SCISSORS", "PAPER") => 1,
            _ => 2
        };

        Console.WriteLine(winner == 0 ? "DRAW" : $"PLAYER{winner}");
    }
}