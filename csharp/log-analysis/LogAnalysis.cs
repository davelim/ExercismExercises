using System;

public static class LogAnalysis 
{
    public static string SubstringAfter(this string log, string delimiter)
    {
        int startIndex = log.IndexOf(delimiter);
        return startIndex == -1 ? log : log.Substring(startIndex+delimiter.Length);
    }

    // TODO: define the 'SubstringBetween()' extension method on the `string` type
    
    // TODO: define the 'Message()' extension method on the `string` type

    // TODO: define the 'LogLevel()' extension method on the `string` type
}