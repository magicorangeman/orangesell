using System.Collections.Generic;

namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var pointsInWindow = new Queue<DataPoint>();
            var potentialMaxs = new LinkedList<DataPoint>();
            foreach (var point in data)
            {
                pointsInWindow.Enqueue(point);
                for (int i = potentialMaxs.Count - 1; i >= 0; i--)
                {
                    if (point.OriginalY > potentialMaxs.Last.Value.OriginalY)
                        potentialMaxs.RemoveLast();
                    else break;
                }
                potentialMaxs.AddLast(point);
                if (pointsInWindow.Count > windowWidth)
                    if (pointsInWindow.Dequeue() == potentialMaxs.First.Value)
                        potentialMaxs.RemoveFirst();
                yield return point.WithMaxY(potentialMaxs.First.Value.OriginalY);
            }
        }
    }
}