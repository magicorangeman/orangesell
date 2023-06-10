using System;
using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;

public class DijkstraPathFinder
{
	public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
		IEnumerable<Point> targets)
	{
        var visited = new HashSet<Point>();
        var track = new Dictionary<Point, PathWithCost> { [start] = new PathWithCost(0, start) };
        while (true)
        {
            var toOpen = GetPointToOpen(track, visited, state);

            if (toOpen.X < 0) break;
            if (targets.Contains(toOpen)) yield return track[toOpen];

            foreach (var step in StepsFromPoint(toOpen).Where(point => state.InsideMap(point) && !state.IsWallAt(point)))
            {
                var currentCost = track[toOpen].Cost + state.CellCost[step.X, step.Y];
                if (!track.ContainsKey(step) || track[step].Cost > currentCost)
                    track[step] = new PathWithCost(currentCost, 
                        new List<Point>(track[toOpen].Path) { step }.ToArray());
            }
            visited.Add(toOpen);
        }
    }

    public static IEnumerable<Point> StepsFromPoint(Point point)
    {
        for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
                if ((dx == 0 || dy == 0) && (dx != dy))
                    yield return new Point(point.X + dx, point.Y + dy);
    }

    public static Point GetPointToOpen(Dictionary<Point, PathWithCost> track, HashSet<Point> visited, State state)
    {
        var toOpen = new Point(-1, -1);
        var bestCost = double.PositiveInfinity;
        foreach (var point in track.Select(x => x.Key).Where(x => !visited.Contains(x)))
        {
            if (!state.IsWallAt(point) && track.ContainsKey(point) && track[point].Cost < bestCost)
            {
                bestCost = track[point].Cost;
                toOpen = point;
            }
        }
        return toOpen;
    }
}