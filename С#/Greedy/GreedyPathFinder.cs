using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using Microsoft.CodeAnalysis;

namespace Greedy;

public class GreedyPathFinder : IPathFinder
{
	public List<Point> FindPathToCompleteGoal(State state)
	{
		var notCollected = new List<Point>(state.Chests);
		var fullPath = new List<Point>();
		var pathFinder = new DijkstraPathFinder();
		var position = state.Position;
		var energy = state.Energy;

		if (notCollected.Contains(position))
		{
			notCollected.Remove(position);
			state.RemoveGoal();
		}
		while (state.Goal != 0)
		{
			var path = pathFinder.GetPathsByDijkstra(state, position, notCollected).FirstOrDefault();
            if (path == null || energy < path.Cost) return new List<Point>();
            fullPath.AddRange(path.Path.Skip(1));
			position = path.End;
			energy -= path.Cost;
			notCollected.Remove(position);
			state.RemoveGoal();
        }

        return fullPath;
	}
}