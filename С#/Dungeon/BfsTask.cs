using System.Collections.Generic;

namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
	{
		var chestsLeft = new HashSet<Point>(chests);
		var visited = new HashSet<Point>();
		var queue = new Queue<SinglyLinkedList<Point>>();
		queue.Enqueue(new SinglyLinkedList<Point>(start));

		while(chestsLeft.Count != 0 && queue.Count != 0) 
		{
			var point = queue.Dequeue();
            if (!map.InBounds(point.Value)) continue;
            if (map.Dungeon[point.Value.X, point.Value.Y] != MapCell.Empty) continue;
            if (visited.Contains(point.Value)) continue;
            visited.Add(point.Value);
            if (chestsLeft.Contains(point.Value))
			{
				yield return point;
                chestsLeft.Remove(point.Value);
            }

			foreach(var step in StepsFromPoint(point))
				queue.Enqueue(step);
        }
	}

	public static IEnumerable<SinglyLinkedList<Point>> StepsFromPoint(SinglyLinkedList<Point> prev)
	{
		for (var dy = -1; dy <= 1; dy++)
			for (var dx = -1; dx <= 1; dx++)
				if (dx == 0 || dy == 0)
					yield return new SinglyLinkedList<Point>(
						new Point(prev.Value.X + dx, prev.Value.Y + dy), prev);
    }
}