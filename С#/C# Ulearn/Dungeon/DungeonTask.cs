using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class DungeonTask
{
    public static MoveDirection[] FindShortestPath(Map map)
    {
        var path = GetShortestPath(map);
        return PathIntoMoveInstructions(path);
    }

    public static MoveDirection[] PathIntoMoveInstructions(List<Point> path)
    {
        return path
            .Zip(path.Skip(1))
            .Select(x => x.Second - x.First)
            .Select(x => Walker.ConvertOffsetToDirection(x))
            .ToArray();
    }

    private static List<Point> GetShortestPath(Map map)
    {
        if (map.Exit == null) return new List<Point>();
        int minDistance = map.Dungeon.Length * 2;
        var pathsToChests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
        if (!pathsToChests.Any())
        {
            var path = BfsTask.FindPaths(map, map.Exit, new Point[] { map.InitialPosition });
            if (path.Any()) return path.First().ToList();
            else return new List<Point>();
        }

        var pathFromExit = BfsTask.FindPaths(map, map.Exit, map.Chests);
        if (!pathFromExit.Any()) return new List<Point>();

        var fullPaths = pathsToChests.Join(pathFromExit, fromStart => fromStart.Value, fromExit => fromExit.Value,
                                      (fromStart, fromExit) => (fromStart, fromExit));
        if (!fullPaths.Any()) return new List<Point>();
        var result = fullPaths.MinBy(path => path.fromStart.Length + path.fromExit.Length);
        return result.fromStart.Skip(1).Reverse().Concat(result.fromExit).ToList();
    }
}