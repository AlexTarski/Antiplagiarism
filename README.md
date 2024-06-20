# Antiplagiarism

This project represents the use of DP concepts in detecting plagiarism in documents.

LevenshteinCalculator class takes a list of documents as input and returns
a list of pair comparisons of all documents between each other.
To implement this comparison, a modified Levenshtein algorithm was used:
it analyzes not the character sequence but the sequence of tokens - lexical items.

For example, the line "force = mass * acceleration" consists of 5 tokens: 'force', '=', 'mass', '*', 'acceleration'.

The input document is represented by a token sequence.

If two tokens are different, the cost of replacing is calculated by the GetTokenDistance method of TokenDistanceCalculator class.
Cost of add/delete equals 1.

Calculate method in the LongestCommonSubsequentCalculator class computes the longest common subsequence (LCS)
in input documents (represented by a list of tokens).

After launching the project, documents in SuspiciousSources folder would be checked for plagiarism.
On the console you will see the Levenshtein distance between each pair of documents.
Also in the root directory of the project would be a html report file.
In that file you will see common parts of documents founded by algorithms.
