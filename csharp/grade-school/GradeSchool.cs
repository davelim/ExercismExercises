using System;
using System.Collections.Generic;
using System.Linq;

public class GradeSchool
{
    private Dictionary<int, HashSet<string>> _gradesWithStudents = new();

    public bool Add(string student, int grade)
    {
        throw new NotImplementedException("You need to implement this method.");
    }

    public IEnumerable<string> Roster()
    {
        List<string> roster = new();
        var sortedByGrades = _gradesWithStudents
            .OrderBy(pair => pair.Key)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        foreach (var grade in sortedByGrades.Values) {
            roster.AddRange(grade.OrderBy(name => name));
        }
        return roster;
    }

    public IEnumerable<string> Grade(int grade)
    {
        throw new NotImplementedException("You need to implement this method.");
    }
}