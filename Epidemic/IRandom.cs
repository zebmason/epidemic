using System.Collections.Generic;

namespace Epidemic
{
    public interface IRandom
    {
        int Next(int maxValue);
    }

    public class Random : System.Random, IRandom
    {
        public Random(int seed)
            : base(seed)
        {
        }

        public new int Next(int maxValue)
        {
            return (maxValue < 2) ? 0 : base.Next(maxValue);
        }
    }

    public class MockRandom : IRandom
    {
        private readonly List<int> _numbers;

        private int _index = -1;

        public MockRandom(List<int> numbers)
        {
            _numbers = numbers;
        }

        public int Next(int maxValue)
        {
            ++_index;
            return _numbers[_index];
        }
    }
}
