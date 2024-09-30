using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        if (InvalidSequence(sequence))
            throw new ArgumentException(
                $"'{sequence}' is an invalid sequence.", "sequence");

        return $"{sequence}ACGT"
            .GroupBy(
                nucleotide => nucleotide
            )
            .ToDictionary(
                group => group.Key,
                group => group.Count() - 1
            );
    }
    private static bool InvalidSequence(string sequence)
    {
        var pattern = "[^ACGT]";
        return Regex.IsMatch(sequence, pattern);
    }
}