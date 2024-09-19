using System;
using System.Collections.Generic;
using System.Linq;

public static class RnaTranscription
{
    private static Dictionary<char, char> _dnaToRna = new Dictionary<char, char> {
        {'G', 'C'}, {'C', 'G'}, {'T', 'A'}, {'A', 'U'}
    };
    public static string ToRna(string strand)
    {
        return string.Join("",strand.Select(n => DnaToRna[n]));
    }
}