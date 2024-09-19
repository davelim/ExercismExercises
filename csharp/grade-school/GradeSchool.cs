using System;
using System.Collections.Generic;
using System.Linq;

public class GradeSchool
{
    private Dictionary<string, int> _students = new();

    public bool Add(string student, int grade)
    {
        try
        {
            _students.Add(student, grade);
            return true;
        }
        catch(ArgumentException)
        {
            return false;
        }
    }

    public IEnumerable<string> Roster()
    {
        return _students
            .OrderBy(student => student.Value)
            .ThenBy(student => student.Key)
            .Select(student => student.Key)
            .ToArray();
    }

    public IEnumerable<string> Grade(int grade)
    {
        return _students
            .Where(student => student.Value == grade)
            .Select(student => student.Key)
            .OrderBy(student => student)
            .ToArray();
    }
}