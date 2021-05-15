using System.Collections.Generic;

namespace Epidemic
{
    abstract public class ProtoState
    {
        protected readonly List<int> _generations;

        protected int _generation;

        protected List<int> _current = null!;

        protected readonly bool _isSIR;

        public ProtoState(int size, bool isSIR)
        {
            _generations = new List<int>(size);
            for (int i = 0; i < size; ++i)
            {
                _generations.Add(0);
            }

            _isSIR = isSIR;
        }

        private void Clear()
        {
            _generation = 1;
            _current = new List<int>();
            for (int i = 0; i < _generations.Count; ++i)
            {
                _generations[i] = 0;
            }
        }

        public void Seed(int index)
        {
            _generations[index] = 1;
            _current.Add(index);
        }

        private void Increment(int node, List<int> next)
        {
            if (_generations[node] != 0)
                return;

            _generations[node] = _generation;
            next.Add(node);
        }

        abstract protected int Discard(List<int> next);

        private int Increment(Paths paths)
        {
            ++_generation;

            var next = new List<int>();
            foreach (var node in _current)
            {
                var (n, m) = paths.Neighbours(node);
                Increment(n, next);
                Increment(m, next);
            }

            return Discard(next);
        }

        public void March(Paths paths, ISeeder seeder, List<int> susceptible, List<int> infected, List<int> resolved, int max_iterations)
        {
            Clear();
            seeder.Seed(this);
            susceptible.Add(_generations.Count - _current.Count);
            infected.Add(_current.Count);
            resolved.Add(0);

            var population = _generations.Count;
            for (int i = 0; i < max_iterations; ++i)
            {
                paths.Generate();
                var discarded = Increment(paths);
                if (discarded == 0 && _current.Count == infected[^1])
                {
                    if (_current.Count > 0 && resolved[^1] != 0)
                    {
                        resolved.Add(_isSIR ? resolved[^1] + _current.Count : 0);
                        infected.Add(0);
                        susceptible.Add(population - resolved[^1] - infected[^1]);
                    }

                    break;
                }

                resolved.Add(_isSIR ? resolved[^1]  + discarded : 0);
                infected.Add(_current.Count);
                susceptible.Add(population - resolved[^1] - infected[^1]);
            }
        }

        protected void MarkDiscarded(int discarded)
        {
            if (_isSIR)
                return;

            for (int i = 0; i < discarded; ++i)
            {
                _generations[_current[i]] = 0;
            }
        }
    }

    public class State : ProtoState
    {
        private readonly float _discard;

        public State(int size, bool isSIR, float discard) : base(size, isSIR)
        {
            _discard = discard;
        }

        protected override int Discard(List<int> next)
        {
            var discarded = _current.Count;
            if (_discard > 0.9999)
            {
                MarkDiscarded(discarded);
                _current = next;
            }
            else
            {
                discarded = (int)(_discard * discarded);
                MarkDiscarded(discarded);
                _current.RemoveRange(0, discarded);
                _current.AddRange(next);
            }

            return discarded;
        }
    }
}
