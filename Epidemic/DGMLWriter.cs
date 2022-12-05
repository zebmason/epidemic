using System.Collections.Generic;
using System.IO;

namespace Epidemic
{
    public class DGMLWriter : IDGMLWriter
    {
        private Dictionary<int, int> _generations = new();

        private List<(int, int)> _edges = new();

        public void AddNode(int node, int generation)
        {
            _generations[node] = generation;
        }

        public void AddEdge(int start, int finish)
        {
            _edges.Add((start, finish));
        }

        public void Save(string filePath)
        {
            using var file = new StreamWriter(filePath);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            file.WriteLine("<DirectedGraph xmlns=\"http://schemas.microsoft.com/vs/2009/dgml\">");
            file.WriteLine("  <Nodes>");
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            foreach (var id in _generations.Keys)
            {
                file.WriteLine($"    <Node Id=\"{id}\" Label=\"{letters[id]}\" Size=\"10\" />");
            }

            file.WriteLine("  </Nodes>");
            file.WriteLine("  <Links>");
            foreach (var (source, target) in _edges)
            {
                if (_generations[target] <= _generations[source])
                    continue;

                file.WriteLine($"    <Link Source=\"{source}\" Target=\"{target}\" />");
            }

            file.WriteLine("  </Links>");
            file.WriteLine("  <Properties>");
            file.WriteLine("    <Property Id=\"Label\" Label=\"Label\" Description=\"Displayable label of an Annotatable object\" DataType=\"System.String\" />");
            file.WriteLine("    <Property Id=\"Size\" DataType=\"System.String\" />");
            file.WriteLine("  </Properties>");
            file.WriteLine("</DirectedGraph>");
            file.WriteLine("");
            file.WriteLine("");
            file.WriteLine("");
            file.Close();
        }
    }
}
