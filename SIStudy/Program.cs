using System.IO;

using Epidemic;

namespace SIStudy
{
    class Program
    {
        static void Complete(string filePath, int population, ISeeder seeder)
        {
            var random = new Random(0);
            var paths = new Paths(new Complete(random, population));
            var state = new State(population, false, 0.0f);

            var sir = SIR.Study(paths, state, seeder, 100);
            sir.Save(filePath, $"# Complete graph of size {population} with seeding {seeder.Description}");
        }

        static void Main(string[] args)
        {
            var directory = Directory.GetParent(Path.GetDirectoryName(typeof(Program).Assembly.Location)).Parent.Parent.ToString();
            Complete(Path.Combine(directory, "complete1000.csv"), 1000, new FirstOne());
            Complete(Path.Combine(directory, "complete1979.csv"), 1979, new FirstOne());
            Complete(Path.Combine(directory, "complete1000_100.csv"), 1000, new FirstMany(100));
            Complete(Path.Combine(directory, "complete10000.csv"), 10000, new FirstOne());
        }
    }
}
