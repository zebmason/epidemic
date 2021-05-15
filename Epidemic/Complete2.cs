using System.Collections.Generic;

namespace Epidemic
{
    public class Complete2 : IGraph
    {
        private readonly List<int> _remains = new List<int>();

        private int _first = -1;

        public bool Remaining => _remains.Count != 0;

        private readonly IRandom _random;

        public int Size { get; }

        public Complete2(IRandom random, int size)
        {
            _random = random;
            Size = size;
            Clear();
        }

        public void Clear()
        {
            _remains.Clear();
            for (var i = 0; i < Size; ++i)
                _remains.Add(i);
        }

        public void AddEdge(int index)
        {
        }

        public int Next(int ultimate, int penultimate)
        {
            var vertex = -1;

            var remaining = _remains.Count;
            if (remaining == 0)
            {
                return vertex;
            }

            if (_first != -1 && (_first == ultimate || _first == penultimate))
            {
                --remaining;
                if (remaining == 0)
                {
                    _remains.Remove(_first);
                    return _first;
                }
            }

            var index = _random.Next(remaining);
            vertex = _remains[index];
            if (_first != -1 && (_first == ultimate || _first == penultimate) && vertex >= _first)
            {
                vertex = _remains[index + 1];
            }

            if (ultimate == -1)
            {
                _first = vertex;
            }
            else
            {
                _remains.Remove(vertex);
            }

            return vertex;
        }

        public bool Edge(int v1, int v2)
        {
            return v1 != v2;
        }
    }
}
