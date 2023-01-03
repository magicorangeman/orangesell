using System;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var order = MakeTrivialPermutation(checkpoints.Length);
			var bestOrder = order.ToArray();
			FindBestWay(checkpoints, order, 1, bestOrder);
			return bestOrder;
		}

		private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (int i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}

		static void FindBestWay(Point[] checkpoints, int[] order, int position, int[] result)
		{
			
			if (position == order.Length)
			{
				order.CopyTo(result,0);
				return;
			}
			
			for (int i = 1; i < order.Length; i++)
			{
				var index = Array.IndexOf(order, i, 0, position);
				if (index != -1) continue;
				order[position] = i;
				var minPathLength = PointExtensions.GetPathLength(checkpoints, result);
				for (int j=0; j < position; j++)
					minPathLength -= PointExtensions.DistanceTo(checkpoints[order[j]], checkpoints[order[j+1]]);
				if (minPathLength < 0) break;
				FindBestWay(checkpoints, order, position + 1, result);
			}
		}
	}
}