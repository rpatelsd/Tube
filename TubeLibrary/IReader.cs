using System.Collections.Generic;

namespace TubeLibrary
{
    public interface IReader
    {
        Dictionary<string, HashSet<string>> GetStations();
    }
}