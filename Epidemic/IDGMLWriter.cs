namespace Epidemic
{
    public interface IDGMLWriter
    {
        void AddNode(int node, int generation);

        void AddEdge(int start, int finish);

        void Save(string filePath);
    }
}
