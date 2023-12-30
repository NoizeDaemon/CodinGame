using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinGame
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Benchmarks
    {
        private const int M = 1;
        private const int V = 1;

        private static readonly Player.Conditions _cond = new Player.Conditions(V, new string[]
        {
            "..............................",
            "..............................",
            "...........0..................",
            ".............................."
        });

        private static readonly Player _player = new Player();
        private static readonly Player.GameState _rootState = new Player.GameState(M);

        [Benchmark(Baseline = true)]
        public async Task<ConcurrentBag<Player.GameState>> GetSolutions()
        {
            return await _player.GetSolutions(_cond, _rootState);
        }

        [Benchmark]
        public async Task<ConcurrentBag<Player.GameState>> GetSolutionsAlt()
        {
            return await _player.GetSolutionsAlt(_cond, _rootState);
        }
    }
}
