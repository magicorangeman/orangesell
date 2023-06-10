using System;
using System.Linq;

namespace Solution;

class Program
{
    public static int CollectChests( List<int> chests, int time)
    {
        var result = 0;
        var possible = chests.Take(time).ToList();
        var sequences = new List<bool[]>();
        GenerateSequence(new bool[possible.Count], 0, sequences);
        foreach (var sequence in sequences)
        {
            var temp = 0;
            var estimate = time;
            for (int i = 0; i < sequence.Length && estimate > i; i++)
            {
                if (sequence[i])
                {
                    temp += possible[i];
                    estimate -= i+1;
                }
            }
            if (temp > result) result = temp;
        }

        return result;
    }

    public static void GenerateSequence(bool[] subset, int position, List<bool[]> result )
    {
        if (position == subset.Length)
        {
            result.Add(subset.ToArray());
            return;
        } 
        subset[position] = false;
        GenerateSequence(subset, position + 1, result);
        subset[position] = true;
        GenerateSequence(subset, position + 1, result);
    }

    public static void Main()
    {
        Console.WriteLine(CollectChests(new List<int> { 1, 4, 2 }, 3));
        Console.WriteLine(CollectChests(new List<int> { 7, 8, 9 }, 2));
    }
}