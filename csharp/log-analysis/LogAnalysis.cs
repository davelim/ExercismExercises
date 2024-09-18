using System;

public static class LogAnalysis 
{
    public static string SubstringAfter(this string log, string delimiter)
    {
        int startIndex = log.IndexOf(delimiter);
        return startIndex == -1 ? log : log.Substring(startIndex+delimiter.Length);
    }

    public static string SubstringBetween(
        this string log, string delimiter1, string delimiter2)
    {
        int startIndex = log.IndexOf(delimiter1);
        int endIndex = log.IndexOf(delimiter2);
        int length = endIndex - (startIndex+delimiter1.Length);
        return log.Substring(startIndex+delimiter1.Length, length);
    }
    
    // TODO: define the 'Message()' extension method on the `string` type

    // TODO: define the 'LogLevel()' extension method on the `string` type
}