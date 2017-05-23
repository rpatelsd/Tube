using System.Collections.Generic;
using TubeLibrary;

namespace TubeTest
{
    public class FakeData : IReader
    {
        public Dictionary<string, HashSet<string>> GetStations()
        {
            return new Dictionary<string, HashSet<string>> {
                { "A", new HashSet<string> {"A","D","B", "C" } },
                { "C", new HashSet<string> {"A" } },
                { "D", new HashSet<string> {"A","B" } },
                { "B", new HashSet<string> {"D", "A" } },
            };
        }
    }
}
