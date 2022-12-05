using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace Epidemic
{
    public class Accumulator
    {
        private readonly Dictionary<int, List<int>> _store = new Dictionary<int, List<int>>();

        public int Count { get; private set; } = 0;

        public void Add(int index, int value)
        {
            if (!_store.ContainsKey(index))
            {
                _store[index] = new List<int>();
                Count = index + 1;
            }

            _store[index].Add(value);
        }

        private double Mean(int index)
        {
            var sum = 0.0;
            _store[index].ForEach(v => sum += v);
            return sum / _store[index].Count;
        }

        public (double, double) Stats(int index)
        {
            var mean = Mean(index);
            if (_store[index].Count == 1)
                return (mean, 0.0);

            var sum = 0.0;
            foreach (var v in _store[index])
            {
                var part = v - mean;
                sum += part * part;
            }

            return (mean, Math.Sqrt(sum / (_store[index].Count - 1)));
        }
    }

    public class SIR
    {
        private readonly Accumulator _susceptible = new Accumulator();
        private readonly Accumulator _infected = new Accumulator();
        private readonly Accumulator _resolved = new Accumulator();
        private int _count = 0;

        public void Add(List<int> susceptible, List<int> infected, List<int> resolved)
        {
            ++_count;
            for (int i = 0; i < susceptible.Count; ++i)
            {
                _susceptible.Add(i, susceptible[i]);
                _infected.Add(i, infected[i]);
                _resolved.Add(i, resolved[i]);
            }
        }

        public void Save(string filePath, string header)
        {
            using var file = new StreamWriter(filePath);
            file.WriteLine(header);
            if (_count == 1)
            {
                file.WriteLine("# susceptible, infected, resolved");
                for (int i = 0; i < _susceptible.Count; ++i)
                {
                    var (val, _) = _susceptible.Stats(i);
                    file.Write($"{val},");
                    (val, _) = _infected.Stats(i);
                    file.Write($"{val},");
                    (val, _) = _resolved.Stats(i);
                    file.WriteLine($"{val}");
                }
            }
            else
            {
                file.WriteLine("# susceptible, standard deviation, infected, standard deviation, resolved, standard deviation");
                for (int i = 0; i < _susceptible.Count; ++i)
                {
                    var (mean, sd) = _susceptible.Stats(i);
                    file.Write($"{mean},{sd},");
                    (mean, sd) = _infected.Stats(i);
                    file.Write($"{mean},{sd},");
                    (mean, sd) = _resolved.Stats(i);
                    file.WriteLine($"{mean},{sd}");
                }
            }
        }

        public static SIR Study(Paths paths, ProtoState state, ISeeder seeder, int repeats, IDGMLWriter dgml, int maxIterations = 10000)
        {
            var sir = new SIR();
            for (int i = 0; i < repeats; ++i)
            {
                Console.WriteLine($"Iteration {i}");
                var susceptible = new List<int>();
                var infected = new List<int>();
                var resolved = new List<int>();
                state.March(paths, seeder, susceptible, infected, resolved, maxIterations, dgml);
                sir.Add(susceptible, infected, resolved);
            }

            return sir;
        }
    }
}
