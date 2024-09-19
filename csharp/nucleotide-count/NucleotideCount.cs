using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        if (InvalidSequence(sequence))
            throw new ArgumentException(
                $"'{sequence}' is an invalid sequence.", "sequence");

        var nucleotideCount = new Dictionary<char, int> {
            {'A', 0}, {'C', 0}, {'G', 0}, {'T', 0}
        };
        foreach (var c  in sequence)
        {
            nucleotideCount[c]++;
        }
        return nucleotideCount;
    }

    private static bool InvalidSequence(string sequence)
    {
        var pattern = "[^ACGT]";
        return Regex.IsMatch(sequence, pattern);
    }
}