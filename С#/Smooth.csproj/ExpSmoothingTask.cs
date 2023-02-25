using System.Collections.Generic;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            DataPoint previous = null;
            DataPoint current;
            foreach (var point in data)
            {
                if (previous == null)
                    current = point.WithExpSmoothedY(point.OriginalY);
                else
                    current = point.WithExpSmoothedY(point.OriginalY * alpha + previous.ExpSmoothedY * (1 - alpha));
                yield return current;
                previous = current;
            }
        }
    }
}