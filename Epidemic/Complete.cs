using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Epidemic
{
    public class Complete : IGraph
    {
        private readonly List<int> _hits = new List<int>();

        private int _remaining;

        public bool Remaining => _remaining != 0;

        private readonly IRandom _random;

        public int Size { get; }

        public Complete(IRandom random, int size)
        {
            _random = random;
            Size = size;
            for (int i = 0; i < Size; ++i)
                _hits.Add(0);
        }

        public void Clear()
        {
            _remaining = Size;
            for (int i = 0; i < Size; ++i)
                _hits[i] = 0;
        }

        public void AddEdge(int index)
        {
            ++_hits[index];
            if (_hits[index] == 2)
            {
                --_remaining;
            }
        }

        public int Next(int ultimate, int penultimate)
        {
            var remaining = _remaining;
            if (penultimate != -1 && _hits[penultimate] != 2)
            {
                remaining -= 2;
            }
            else if (ultimate != -1)
            {
                --remaining;
            }

            if (remaining == 0)
            {
                _remaining = 0;
                return -1;
            }

            var index = _random.Next(remaining);

            Debug.WriteLine($"{index}");
            for (int i = 0, j = 0; i < _hits.Count; ++i)
            {
                if (_hits[i] == 2 || i == ultimate || i == penultimate)
                    continue;

                if (j == index)
                    return i;

                ++j;
            }

            throw new ArgumentOutOfRangeException();
        }

        public bool Edge(int v1, int v2)
        {
            return v1 != v2;
        }
    }
}
