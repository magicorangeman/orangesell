using System;
using System.Collections.Generic;

namespace Antiplagiarism;

public static class LongestCommonSubsequenceCalculator
{
	public static List<string> Calculate(List<string> first, List<string> second)
	{
		var opt = CreateOptimizationTable(first, second);
		return RestoreAnswer(opt, first, second);
	}

	private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
	{
		var opt = new int[first.Count + 1, second.Count + 1];
		for(int i = 0; i <= first.Count; ++i) opt[i, 0] = 0;
        for (int j = 0; j <= second.Count; ++j) opt[0, j] = 0;

        for (int i = 1; i <= first.Count; ++i)
            for (int j = 1; j <= second.Count; ++j)
				opt[i, j] = (first[i - 1] == second[j - 1]) ? 
					opt[i - 1, j - 1] + 1 : Math.Max(opt[i, j - 1], opt[i - 1, j]);
		return opt;
    }

	private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
	{
		var answer = new List<string>();
		var lastOptimal = new OptimalSymbol(first.Count, second.Count, opt[first.Count, second.Count]);

		for (int i = first.Count; i >= 1; --i)
			for (int j = lastOptimal.Y; j >= 1; --j)
				if (opt[i, j - 1] + 1 == opt[i, j] && opt[i, j] == lastOptimal.Value 
					&& first[i - 1].Equals(second[j - 1]))
				{
                    answer.Add(first[i - 1]);
                    lastOptimal = new OptimalSymbol(i, j, opt[i, j] - 1);
					break;
                }

		answer.Reverse();
		return answer;
    }

	public record OptimalSymbol(int X, int Y, int Value);
}