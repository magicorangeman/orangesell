using System.Collections.Generic;

namespace Rivals;

public class RivalsTask
{
	public static IEnumerable<OwnedLocation> AssignOwners(Map map)
	{
        var owned = new HashSet<Point>();
        var queue = new Queue<OwnedLocation>();

        for(var i = 0; i < map.Players.Length; i++)
            queue.Enqueue(new OwnedLocation(i, map.Players[i], 0));

        while (map.Maze.Length != owned.Count && queue.Count != 0)
        {
            var playerTurn = queue.Dequeue();
            if (!map.InBounds(playerTurn.Location)) continue;
            if (map.Maze[playerTurn.Location.X, playerTurn.Location.Y] != MapCell.Empty) continue;
            if (owned.Contains(playerTurn.Location)) continue;
            foreach (var nextTurn in NextTurns(playerTurn))
                queue.Enqueue(nextTurn);
            owned.Add(playerTurn.Location);
            yield return playerTurn;
        }
    }

    public static IEnumerable<Point> StepsFromPoint(Point prev)
    {
        for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
                if (dx == 0 || dy == 0)
                    yield return new Point(prev.X + dx, prev.Y + dy);
    }

    public static IEnumerable<OwnedLocation> NextTurns(OwnedLocation prevTurn)
    {
        var nextLocations = StepsFromPoint(prevTurn.Location);
        foreach (var nextStep in nextLocations)
            yield return new OwnedLocation(prevTurn.Owner, nextStep, prevTurn.Distance + 1);
    }
}