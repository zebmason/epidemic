using System.IO;
using Epidemic;

namespace Tag
{
    class Program
    {
        static IDGMLWriter Dgml(int repeats)
        {
            if (repeats == 1)
            {
                return new DGMLWriter();
            }

            return new DGMLGhostWriter();
        }

        static void Complete(string filePath, float discard, int repeats, int seed, int population)
        {
            var random = new Random(seed);
            var paths = new Paths(new Complete(random, population));
            var state = new State(population, true, discard);

            System.Console.WriteLine($"Test {filePath}");
            var dgml = Dgml(repeats);
            var sir = SIR.Study(paths, state, new FirstOne(), repeats, dgml);
            sir.Save(filePath, $"# Complete graph of size {population} discarding {100 * discard}%");

            filePath = filePath.Replace(".csv", ".dgml");
            dgml.Save(filePath);
        }

        static void Main(string[] args)
        {
            var directory = Directory.GetParent(Path.GetDirectoryName(typeof(Program).Assembly.Location)).Parent.Parent.ToString();
            Complete(Path.Combine(directory, $"first.csv"), 1.0f, 1, 1, 31);
            Complete(Path.Combine(directory, $"second.csv"), 1.0f, 1, 13, 31);
            Complete(Path.Combine(directory, $"complete.csv"), 1.0f, 100, 54, 1023);
        }
    }
}
