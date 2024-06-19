using System;
using System.Collections.Generic;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism;

public class LevenshteinCalculator
{
	private delegate double GetTokenDistance(string s, string s1);
    public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
	{
        if (documents.Count < 2)
        {
            return new List<ComparisonResult>();
        }

        if (documents.Count == 2)
        {
            return new List<ComparisonResult> {
            new(
                documents[0],
                documents[1],
                GetLevenshteinDistance(documents[0], documents[1]))};
        }
        
        var result = new List<ComparisonResult>();
        CompareAllDocs(documents, result);

        return result;
    }

    private static void CompareAllDocs (List<DocumentTokens> documents, List<ComparisonResult> result)
    {
        for (int i = 0; i < documents.Count - 1; i++)
            for (int j = i + 1; j < documents.Count; j++)
            {
                result.Add(
                    new ComparisonResult(documents[i], documents[j], GetLevenshteinDistance(documents[i], documents[j])));
            }
    }

    private static double GetLevenshteinDistance(DocumentTokens first, DocumentTokens second)
    {
        GetTokenDistance tokenDistance = TokenDistanceCalculator.GetTokenDistance;
        var opt = new double[first.Count + 1, second.Count + 1];
        for (var i = 0; i <= first.Count; ++i) opt[i, 0] = (double)i;
        for (var i = 0; i <= second.Count; ++i) opt[0, i] = (double)i;
        for (var i = 1; i <= first.Count; ++i)
            for (var j = 1; j <= second.Count; ++j)
            {
                if (first[i - 1] == second[j - 1])
                    opt[i, j] = opt[i - 1, j - 1];
                else
                {
                    opt[i, j] = Math.Min(1 + opt[i - 1, j], Math.Min(tokenDistance(first[i - 1], second[j - 1]) + opt[i - 1, j - 1], 1 + opt[i, j - 1]));
                }
            }

        return opt[first.Count, second.Count];
    }
}