using DynamicData;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class DungeonTask
{
	public static MoveDirection[] FindShortestPath(Map map)
	{
		return new MoveDirection[0];
	}

	public static MoveDirection[] PathIntoMoveInstructions(List<Point> path)
	{
		return path
			.Select(x => x - path.Skip(path.IndexOf(x) + 1).FirstOrDefault())
			.SkipLast(1)
			.Select(x => offsetToDirection[x])
			.ToArray();
	}
    private static List<List<Point>> GetPaths(Map map)
    {
        var pathsToChests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests)
            .Select(x => x.ToList())
            .ToList();
        var pathsToExit = BfsTask.FindPaths(map, map.Exit, map.Chests)
            .Select(x => x.ToList())
            .Reverse()
            .ToList();
        var paths = 
        return paths;
    }

    private static readonly Dictionary<Point, MoveDirection> offsetToDirection = new()
    {
        { new Point(0, -1), MoveDirection.Up },
        { new Point(0, 1), MoveDirection.Down },
        { new Point(-1, 0), MoveDirection.Left },
        { new Point(1, 0), MoveDirection.Right }
    };
}