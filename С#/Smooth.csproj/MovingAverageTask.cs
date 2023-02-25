using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var avgPool = new Queue<DataPoint>();
            double sum = 0;
            foreach (var point in data)
            {
                avgPool.Enqueue(point);
                sum += point.OriginalY;
                if (avgPool.Count > windowWidth) sum -= avgPool.Dequeue().OriginalY;
                yield return point.WithAvgSmoothedY(sum / avgPool.Count);
            }
        }
    }
}