using System;
using System.Text.RegularExpressions;

public class LogParser
{
    public bool IsValidLine(string text)
    {
        string pattern = @"^\[(TRC|DBG|INF|WRN|ERR|FTL)\]";
        return Regex.IsMatch(text, pattern);
    }

    public string[] SplitLogLine(string text)
    {
        string pattern = @"<[\^\*=\-]+>";
        return Regex.Split(text, pattern);
    }

    public int CountQuotedPasswords(string lines)
    {
        string pattern = "\".*(?i)(password)(?-i).*\"";
        return Regex.Count(lines, pattern);
    }

    public string RemoveEndOfLineText(string line)
    {
        string pattern = "end-of-line[0-9]+";
        return Regex.Replace(line, pattern, String.Empty);
    }

    public string[] ListLinesWithPasswords(string[] lines)
    {
        string[] linesWithPasswords = new string[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            Match match = Regex.Match(lines[i], @"(?i)(password)(?-i)\w+");
            string password = match.Success ? match.Value : "--------";
            linesWithPasswords[i] = $"{password}: {lines[i]}";
        }
        return linesWithPasswords;
    }
}
