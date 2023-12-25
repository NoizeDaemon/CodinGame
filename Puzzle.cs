using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

public class Player
{
    public enum Move { SPEED, SLOW, JUMP, WAIT, UP, DOWN }
    public enum Direction { NONE = 0, UP = -1, DOWN = 1 }

    public readonly struct Conditions
    {
        public Conditions(int V)
        {
            MinBikeCount = V;

            var inputs = Enumerable.Range(0, 4).Select(_ => Console.ReadLine()).ToArray();
            BridgeLength = inputs[0].Length;
            _isPassable = new bool[4, BridgeLength];

            for (int y = 0; y < 4; y++)
            {
                Console.Error.WriteLine(inputs[y].ToString());
                for (int x = 0; x < BridgeLength; x++)
                {
                    _isPassable[y, x] = inputs[y][x].Equals('.');
                }
            }
        }
        public readonly int MinBikeCount { get; }
        public readonly int BridgeLength { get; }

        private readonly bool[,] _isPassable;

        private bool IsPassable(int y, int x)
        {
            return x < BridgeLength ? _isPassable[y, x] : true;
        }

        public bool TryGetNewBikeY(Move move, int speed, int x, ref int y)
        {
            Direction direction = move switch
            {
                Move.UP => Direction.UP,
                Move.DOWN => Direction.DOWN,
                _ => Direction.NONE
            };

            if (direction == Direction.NONE)
            {
                for (int d = move == Move.JUMP ? speed : 1; d <= speed; d++)
                {
                    if (!IsPassable(y, x + d)) return false;
                }
            }
            else
            {
                for (int d = 1; d <= speed; d++)
                {
                    if (!IsPassable(y, x + d) && d != speed) return false;
                    if (!IsPassable(y + (int)direction, x + d)) return false;
                }
            }
            y += (int)direction;
            return true;
        }
    }

    class GameState
    {
        public int Speed { get; set; }
        public int X { get; set; }
        public int[] BikeYs { get; set; }
        public int MoveCount { get; set; }
        public bool IsViable { get; set; }
        public GameState Parent { get; }
        public Move MoveFromParent { get; set; }

        public GameState(int M)
        {
            X = 0;
            MoveCount = 0;
            IsViable = true;
            Speed = int.Parse(Console.ReadLine());
            Console.Error.WriteLine(Speed);
            BikeYs = new int[M];

            for (int i = 0; i < M; i++)
            {
                BikeYs[i] = int.Parse(Console.ReadLine().Split(' ')[1]);
                Console.Error.WriteLine(BikeYs[i]);
            }
        }

        public GameState(GameState prev, Move move, Conditions cond)
        {
            MoveCount = prev.MoveCount + 1;
            MoveFromParent = move;
            Parent = prev;
            Speed = move switch
            {
                Move.SPEED => prev.Speed + 1,
                Move.SLOW => prev.Speed - 1,
                _ => prev.Speed,
            };

            var newBikeYs = new List<int>();
            foreach (var y in prev.BikeYs)
            {
                int newY = y;
                if (cond.TryGetNewBikeY(move, Speed, prev.X, ref newY))
                {
                    newBikeYs.Add(newY);
                }
            }
            BikeYs = newBikeYs.ToArray();

            IsViable = BikeYs.Length >= cond.MinBikeCount;
            X = prev.X + Speed;
        }
    }


    public static async Task Main()
    {
        ConcurrentBag<GameState> solutions = new();
        ConcurrentStack<GameState> statesToCheck = new();

        int M = int.Parse(Console.ReadLine()); // the amount of motorbikes to control
        int V = int.Parse(Console.ReadLine()); // the minimum amount of motorbikes that must survive
        var cond = new Conditions(V);
        statesToCheck.Push(new GameState(M));

        var allMoves = (int[])Enum.GetValues(typeof(Move));

        Action checkStates = () =>
        {
            if (statesToCheck.TryPop(out var currentParent))
            {
                if (currentParent.X >= cond.BridgeLength)
                {
                    solutions.Add(currentParent);
                }
                else
                {
                    Parallel.ForEach<int>(allMoves, m =>
                    {
                        var move = (Move)m;
                        if (currentParent.Speed <= 1 && move == Move.SLOW) return;
                        if (currentParent.Speed == 0 && move != Move.SPEED) return;
                        if (currentParent.BikeYs.Min() == 0 && move == Move.UP) return;
                        if (currentParent.BikeYs.Max() == 3 && move == Move.DOWN) return;

                        var newState = new GameState(currentParent, move, cond);

                        if (newState.IsViable) statesToCheck.Push(newState);
                    });
                }
            }
        };

        while (solutions.Count < 1000)
        {
            var tasks = new Task[100];
            Array.Fill(tasks, Task.Factory.StartNew(checkStates));
            await Task.WhenAll(tasks);
        }
        

        Stack<Move> path = new Stack<Move>();

        GameState lastState = solutions.OrderBy(s => s.MoveCount).First();


        while (lastState.Parent != null)
        {
            path.Push(lastState.MoveFromParent);
            lastState = lastState.Parent;
        }

        string move = path.Pop().ToString();
        Console.WriteLine(move);

        while (true)
        {
            int S = int.Parse(Console.ReadLine());
            for (int i = 0; i < M; i++)
            {
                var inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                int X = inputs[0]; 
                int Y = inputs[1];
                bool A = inputs[2] > 0;
            }




            if (path.TryPop(out var m)) move = m.ToString();
            else move = "SPEED";
            Console.WriteLine(move);
        }
    }
}