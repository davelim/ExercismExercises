using System;

public static class LogAnalysis 
{
    public static string SubstringAfter(this string str, string delimiter)
    {
        int startIndex = str.IndexOf(delimiter);
        return startIndex == -1 ? str : str.Substring(startIndex+delimiter.Length);
    }

    public static string SubstringBetween(
        this string str, string delimiter1, string delimiter2)
    {
        int startIndex = str.IndexOf(delimiter1);
        int endIndex = str.IndexOf(delimiter2);
        int length = endIndex - (startIndex+delimiter1.Length);
        return str.Substring(startIndex+delimiter1.Length, length);
    }
    
    public static string Message(this string log)
    {
        return log.SubstringAfter(": ");
    }

    public static string LogLevel(this string log)
    {
        return log.SubstringBetween("[", "]");
    }
}