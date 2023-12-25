using System;
using System.Linq;
using System.Collections.Generic;

namespace SolvedPuzzles.TheLabyrinth;

// Source: https://www.codingame.com/training/hard/the-labyrinth/

public class Node
{
    public Node(int x, int y, char state)
    {
        (X, Y) = (x, y);
        State = state;
    }
    public int X { get; }
    public int Y { get; }
    public char State { get; set; }
    public bool Traversed { get; set; }
    public bool IsDeadEnd { get; set; }

    public int GetManhattenDistance(Node n) => Math.Abs(X - n.X) + Math.Abs(Y - n.Y);

    public Node Parent { get; private set; }
    public int HeuristicCost { get; private set; }
    public int CurrentCost => Parent?.CurrentCost + 1 ?? 0;
    public int TotalCost => HeuristicCost + CurrentCost;

    public void SetHeuristic(Node target) => HeuristicCost = this.GetManhattenDistance(target);
    public void SetParent(Node parent) => Parent = parent;
}

public class Maze : List<Node>
{
    public Maze(int rows, int columns, int moveLimit)
    {
        Bounds = (columns, rows);
        MoveLimit = moveLimit;
    }

    public (int X, int Y) Bounds { get; }
    public int MoveLimit { get; }

    public Node StartPoint { get; set; }
    public Node CheckPoint { get; set; }
    public bool CheckPointVisited { get; set; }
    public bool DeadEndsNeedUpdate { get; set; }

    public void AddOrUpdate(int x, int y, char state)
    {
        if (GetNodeAt(x, y) is Node node) node.State = state;
        else this.Add(new Node(x, y, state));
    }

    public void TrySetPointsOfInterest()
    {
        if (CheckPoint is null) CheckPoint = this.SingleOrDefault(n => n.State == 'C');
        if (StartPoint is null) StartPoint = this.SingleOrDefault(n => n.State == 'T');
    }

    public void UpdateDeadEnds(Node currentNode)
    {
        foreach (var nodeToCheck in this.GetNodesInView(currentNode, '.').Where(n => !n.IsDeadEnd)) CheckForDeadEnd(nodeToCheck);
    }


    private void CheckForDeadEnd(Node nodeToCheck)
    {
        var alreadyChecked = new List<Node>();
        var toBeChecked = new Queue<Node>();

        toBeChecked.Enqueue(nodeToCheck);

        while (toBeChecked.Any())
        {
            var current = toBeChecked.Dequeue();
            alreadyChecked.Add(current);
            var available = GetNeighbours(current, ".T?").Where(n => !n.Traversed && !n.IsDeadEnd);
            foreach (var neighbour in available.Except(toBeChecked).Except(alreadyChecked))
            {
                if ("?".Contains(neighbour.State)) return;
                toBeChecked.Enqueue(neighbour);
            }
        }
        foreach (var deadEndNode in alreadyChecked) deadEndNode.IsDeadEnd = true;
    }

    public Node GetNodeAt(int x, int y) => this.SingleOrDefault(n => (n.X, n.Y) == (x, y));

    public IEnumerable<Node> GetNeighbours(Node m, string states = ".TC")
    {
        return this.Where(n => m.GetManhattenDistance(n) == 1 && states.Contains(n.State));
    }

    public IEnumerable<Node> GetNodesInView(Node m, char state = '*')
    {
        return this.Where(n => Math.Abs(m.X - n.X) <= 2 && Math.Abs(m.Y - n.Y) <= 2 && (state == '*' || n.State == state));
    }

    public bool TryGetPath(Node start, Node target, out Stack<Node> path)
    {
        path = new Stack<Node>();
        if (start is null || target is null) return false;

        start.SetHeuristic(target);
        start.SetParent(null);

        var toSearch = new List<Node>() { start };
        var processed = new List<Node>();

        while (toSearch.Any())
        {
            var nexus = toSearch.OrderBy(p => p.TotalCost).ThenBy(p => p.HeuristicCost).First();
            toSearch.Remove(nexus);
            processed.Add(nexus);

            if (nexus == target)
            {
                var currentPathNode = nexus;
                while (currentPathNode != start)
                {
                    path.Push(currentPathNode);
                    currentPathNode = currentPathNode.Parent;
                }
                return true;
            }

            foreach(Node neighbour in this.GetNeighbours(nexus).Except(processed))
            {
                if (!toSearch.Contains(neighbour))
                {
                    neighbour.SetParent(nexus);
                    neighbour.SetHeuristic(target);
                    toSearch.Add(neighbour);
                }
                else if (neighbour.CurrentCost > nexus.CurrentCost + 1) 
                {
                    neighbour.SetParent(nexus);
                }
            }
        }
        return false;
    }
}

public class Rick
{
    public Rick(Maze maze)
    {
        _fuel = 1200;
        _maze = maze;
        _traversedNodes = new();
        _backTrackPath = new();
    }

    public int X { get; set; }
    public int Y { get; set; }

    private Node _currentNode => _maze.GetNodeAt(X, Y);
    private Maze _maze;
    private bool _hasViablePathToCheckPoint;
    private int _fuel;

    private Stack<Node> _traversedNodes;
    private Stack<Node> _backTrackPath;
    private Stack<Node> _pathToCheckPoint;
    private Stack<Node> _pathToStartPoint;

    public string Move()
    {
        _currentNode.Traversed = true;
        _traversedNodes.Push(_currentNode);
        _fuel--;

        if (!_hasViablePathToCheckPoint) _hasViablePathToCheckPoint = TryPathfinding();

        var nextNode = NextTarget();
        if (nextNode == _maze.CheckPoint) _maze.CheckPointVisited = true;

        if (nextNode.X < X) return "LEFT";
        if (nextNode.X > X) return "RIGHT";
        if (nextNode.Y < Y) return "UP";
        if (nextNode.Y > Y) return "DOWN";
        throw new ArgumentOutOfRangeException();
    }

    private bool TryPathfinding()
    {
        if (_maze.CheckPoint is null) return false;
        if (!_maze.TryGetPath(_currentNode, _maze.CheckPoint, out var pathToCheckPoint)) return false;
        if (!_maze.TryGetPath(_maze.CheckPoint, _maze.StartPoint, out var pathToStartPoint)) return false;
        if (pathToCheckPoint.Count + pathToStartPoint.Count > _fuel) return false;
        if (pathToStartPoint.Count > _maze.MoveLimit) return false;

        _pathToCheckPoint = pathToCheckPoint;
        _pathToStartPoint = pathToStartPoint;

        return true;
    }

    private Node NextTarget()
    {
        if (!_hasViablePathToCheckPoint) return Explore();
        if (!_maze.CheckPointVisited) return MoveToCheckPoint();
        return MoveToStartPoint();
    }

    private Node Explore()
    {
        _maze.UpdateDeadEnds(_currentNode);

        if (_backTrackPath.Any()) return _backTrackPath.Pop();

        var possibleMoves = _maze.GetNeighbours(_currentNode).Where(n => !n.Traversed && !n.IsDeadEnd);
        if (possibleMoves.Any())
        {
            return possibleMoves.OrderByDescending(n => _maze.GetNodesInView(n, '?').Count()).First();
        }

        SetUpBackTrack();
        return Explore();
    }

    private void SetUpBackTrack()
    {
        var targetNode = _currentNode;
        while (targetNode.IsDeadEnd)
        {
            targetNode = _traversedNodes.Pop();
        }
        _ = _maze.TryGetPath(_currentNode, targetNode, out var path);
        _backTrackPath = path;
    }

    private Node MoveToCheckPoint() => _pathToCheckPoint.Pop();
    private Node MoveToStartPoint() => _pathToStartPoint.Pop();
}

public class Player
{
    public static void Main()
    {
        var inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        var maze = new Maze(rows: inputs[0], columns: inputs[1], moveLimit: inputs[2]);
        var rick = new Rick(maze);

        while (true)
        {
            inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            (rick.X, rick.Y) = (inputs[1], inputs[0]);

            for (int y = 0; y < maze.Bounds.Y; y++)
            {
                string row = Console.ReadLine();
                for (int x = 0; x < maze.Bounds.X; x++)
                {
                    maze.AddOrUpdate(x, y, state: row[x]);
                }
            }
            maze.TrySetPointsOfInterest();
            Console.WriteLine(rick.Move());
        }
    }
}