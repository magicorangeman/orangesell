using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
    public static double Median(this IEnumerable<double> items)
    {
        var list = items.ToList();
        if (list.Count == 0)
            throw new InvalidOperationException();
        if (list.Count % 2 == 0)
            return list.OrderBy(x => x).Skip(list.Count / 2 - 1).Take(2).Sum() / 2;
        return list.OrderBy(x => x).ElementAt((list.Count - 1) / 2);
    }

    public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
    {
        var queue = new Queue<T>();
        foreach (var item in items)
        {
            if (queue.Count > 0)
                yield return (queue.Dequeue(), item);
            queue.Enqueue(item);
        }
    }
}