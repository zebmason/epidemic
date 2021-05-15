using System.IO;
using Epidemic;

namespace SIRStudy
{
    class Program
    {
        static void Complete(string filePath, float discard)
        {
            var random = new Random(0);
            var population = 1000;
            var paths = new Paths(new Complete(random, population));
            var state = new State(population, true, discard);

            var sir = SIR.Study(paths, state, new FirstOne(), 100);
            sir.Save(filePath, $"# Complete graph of size {population} discarding {100 * discard}%");
        }

        static void Main(string[] args)
        {
            var directory = Directory.GetParent(Path.GetDirectoryName(typeof(Program).Assembly.Location)).Parent.Parent.ToString();
            for (int i = 1; i < 11; ++i)
            {
                Complete(Path.Combine(directory, $"complete{i}.csv"), i * 0.1f);
            }

            Complete(Path.Combine(directory, $"complete01.csv"), 0.01f);
            Complete(Path.Combine(directory, $"complete05.csv"), 0.05f);
        }
    }
}
