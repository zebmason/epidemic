namespace Epidemic
{
    public interface IGraph
    {
        void Clear();
        int Next(int ultimate, int penultimate);
        bool Remaining { get; }
        bool Edge(int v1, int v2);
        int Size { get; }
        void AddEdge(int index);
    }
}
