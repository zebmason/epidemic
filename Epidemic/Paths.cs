using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Epidemic
{
    public class Paths
    {
        private readonly List<List<int>> _paths = new List<List<int>>();

        private readonly IGraph _graph;

        public Paths(IGraph graph)
        {
            _graph = graph;
        }

        private void Add()
        {
            var list = _paths[^1];
            var count = list.Count;
            var node = _graph.Next((count == 0) ? -1 : list[^1], (count < 2) ? -1 : list[^2]);
            if (node == -1)
            {
                if (list.Count > 0)
                {
                    list.Add(list[0]);
                }

                return;
            }

            if (count > 0)
            {
                if (count > 1)
                {
                    _graph.AddEdge(list[^1]);
                }

                if (node == list[0])
                {
                    _paths.Add(new List<int>());
                }
            }

            list.Add(node);
            _graph.AddEdge(node);
        }

        public void Last()
        {
            var list = _paths[^1];
            if (list.Count == 0)
                return;

            if (_graph.Edge(list[0], list[^1]))
            {
                list.Add(list[0]);
            }
        }

        public void Generate()
        {
            _paths.Clear();
            _graph.Clear();

            _paths.Add(new List<int>());
            var remaining = _graph.Size;
            while (_graph.Remaining)
            {
                Add();
            }

            Last();

            Debug.WriteLine(string.Empty);
        }

        public (int, int) Neighbours(int node)
        {
            foreach (var path in _paths)
            {
                var index = path.IndexOf(node);
                if (index != -1)
                {
                    var up = node;
                    if (index + 1 < path.Count)
                    {
                        up = path[index + 1];
                    }
                    else if (path[0] == path[^1] && path.Count > 1)
                    {
                        up = path[1];
                    }

                    var down = node;
                    if (index > 0)
                    {
                        down = path[index - 1];
                    }
                    else if (path[0] == path[^1] && path.Count > 1)
                    {
                        down = path[^2];
                    }

                    return (up, down);
                }
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
