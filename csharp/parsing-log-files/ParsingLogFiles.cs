using System;
using System.Text.RegularExpressions;

public class LogParser
{
    public bool IsValidLine(string text)
    {
        string pattern = "^\\[(TRC|DBG|INF|WRN|ERR|FTL)\\]";
        return Regex.IsMatch(text, pattern);
    }

    public string[] SplitLogLine(string text)
    {
        string pattern = "<[\\^\\*=\\-]+>";
        return Regex.Split(text, pattern);
    }

    public int CountQuotedPasswords(string lines)
    {
        string pattern = "\".*(?i)(password)(?-i).*\"";
        return Regex.Count(lines, pattern);
    }

    public string RemoveEndOfLineText(string line)
    {
        throw new NotImplementedException($"Please implement the LogParser.RemoveEndOfLineText() method");
    }

    public string[] ListLinesWithPasswords(string[] lines)
    {
        throw new NotImplementedException($"Please implement the LogParser.ListLinesWithPasswords() method");
    }
}
