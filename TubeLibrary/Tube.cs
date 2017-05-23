using System.Collections.Generic;
using System.Linq;

namespace TubeLibrary
{
    public class Tube
    {
        Dictionary<string, HashSet<string>> stations = null;

        public Tube(IReader stationReader)
        {
            stations = stationReader.GetStations();
        }

                public IEnumerable<string> GetStationsNHopsAway(string startStation, int hops)
        {
            var visited = new HashSet<string>();
            IEnumerable<string> returnList = new List<string>();

            if (!stations.ContainsKey(startStation))
                return returnList;

            var queue = new Queue<string>();
            var depthqueue = new Queue<int>();
            var stationDepthTracker = new Dictionary<int, HashSet<string>>();

            queue.Enqueue(startStation);
            depthqueue.Enqueue(0);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                var currentDepth = depthqueue.Dequeue();

                if (currentDepth == (hops + 1))
                {
                    break;
                }

                if (visited.Contains(vertex))
                    continue;

                if (stationDepthTracker.ContainsKey(currentDepth))
                    stationDepthTracker[currentDepth].Add(vertex);
                else
                    stationDepthTracker[currentDepth] = new HashSet<string> { vertex };

                visited.Add(vertex);

                foreach (var neighbor in stations[vertex].Except(visited))
                {
                    queue.Enqueue(neighbor);
                    depthqueue.Enqueue(currentDepth + 1);
                }
            }

            return BuildStationList(stationDepthTracker, hops);
        }

        
        private IEnumerable<string> BuildStationList(IDictionary<int, HashSet<string>> depthTracker, int hops)
        {
            if (!depthTracker.ContainsKey(hops))
                return new List<string>();

            var hopStations = depthTracker[hops].Select(x => x);
            var lessThanHopStations = depthTracker.Where(x => x.Key < hops).SelectMany(x => x.Value).Select(x=>x);

            return hopStations.Except(lessThanHopStations).AsQueryable().OrderBy(x => x).ToList();
        }
    }
}
