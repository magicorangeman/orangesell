using System;
using System.Collections.Generic;
using System.Linq;
using DocumentTokens = System.Collections.Generic.List<string>;
using Token = Antiplagiarism.TokenDistanceCalculator;

namespace Antiplagiarism;

public class LevenshteinCalculator
{
	public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
	{
        var result = new List<ComparisonResult>();
        for (int i = 0; i < documents.Count - 1; i++)
            for (int j = i + 1; j < documents.Count; j++)
                result.Add(new ComparisonResult(documents[i], documents[j], LevenshteinDistance(documents[i], documents[j])));
        return result;
	}

    public static double LevenshteinDistance(DocumentTokens first, DocumentTokens second)
    {
        double[] opt = Enumerable.Range(0, second.Count + 1).Select(Convert.ToDouble).ToArray();

        for (var i = 1; i <= first.Count; ++i)
        {
            var optPrev = opt.ToArray();
            opt[0] = i;

            for (var j = 1; j <= second.Count; ++j)
            {
                var distance = Token.GetTokenDistance(first[i - 1], second[j - 1]);
                opt[j] = (first[i - 1] == second[j - 1]) ? opt[j] = distance + optPrev[j - 1] : 
                    Math.Min(distance + optPrev[j - 1], Math.Min(1 + optPrev[j], 1 + opt[j - 1]));
            }
        }

        return opt[second.Count];
    }
}