namespace Epidemic
{
    public interface ISeeder
    {
        void Seed(ProtoState state);

        string Description { get; }
    }

    public class FirstOne : ISeeder
    {
        public void Seed(ProtoState state)
        {
            state.Seed(0);
        }

        public string Description => "first one";
    }

    public class FirstMany : ISeeder
    {
        private readonly int _many;

        public FirstMany(int many)
        {
            _many = many;
        }

        public void Seed(ProtoState state)
        {
            for (var i = 0; i < _many; ++i)
                state.Seed(i);
        }

        public string Description => $"first {_many}";
    }
}
