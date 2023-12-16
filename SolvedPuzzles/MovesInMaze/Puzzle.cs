using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodinGame.SolvedPuzzles.MovesInMaze;

public class Node
{
    public Node((int x, int y) position, char state)
    {
        Position = position;
        State = state;
    }
    public (int X, int Y) Position { get; }
    public char State { get; set; }
}

public class Maze : List<Node>
{
    public Maze((int w, int h) bounds)
    {
        Bounds = bounds;
    }

    public (int W, int H) Bounds { get; }

    public IEnumerable<Node> GetAvailableNeighbours(Node m)
    {
        return this.Where(n => n.State.Equals('.') && IsPeriodicNeighbour(m, n));
    }

    private bool IsPeriodicNeighbour(Node a, Node b)
    {
        int dx = Math.Abs(a.Position.X - b.Position.X);
        int dy = Math.Abs(a.Position.Y - b.Position.Y);

        if (dx == 0) return dy == 1 || dy == Bounds.H - 1;
        if (dy == 0) return dx == 1 || dx == Bounds.W - 1;
        return false;
    }
}

public static class Extensions
{
    public static char ToHexatrigesimal(this int i)
    {
        return i > 9 ? (char)(i + 55) : (char)(i + 48);
    }

    public static int FromHexatrigesimal(this char c)
    {
        int i = c;
        return i < 65 ? i - 48 : i - 55;
    }
}

public class Solution
{
    public static void Main(string[] args)
    {
        var queue = new Queue<Node>();

        string[] inputs = Console.ReadLine()!.Split(' ');
        int w = int.Parse(inputs[0]);
        int h = int.Parse(inputs[1]);
        var maze = new Maze((w, h));

        Console.Error.WriteLine("Input:");
        for (int y = 0; y < h; y++)
        {
            string line = Console.ReadLine()!;
            Console.Error.WriteLine(line);
            for (int x = 0; x < line.Length; x++)
            {
                maze.Add(new Node((x, y), line[x]));
            }
        }

        var start = maze.Single(n => n.State == 'S');
        start.State = '0';
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            var currentMove = currentNode.State.FromHexatrigesimal() + 1;
            var nextNodes = maze.GetAvailableNeighbours(currentNode);

            foreach (var node in nextNodes)
            {
                node.State = currentMove.ToHexatrigesimal();
                queue.Enqueue(node);
            }
        }

        Console.Error.WriteLine("\nOutput:");
        for (int i = 0; i < h; i++)
        {
            var line = string.Concat(maze.GetRange(i * w, w).Select(n => n.State));
            Console.WriteLine(line);
        }
    }
}

