using System;
using System.Collections.Generic;
using System.Linq;

using Student = (string Name, int Grade);
public class GradeSchool
{
    private List<Student> _students = new();

    public bool Add(string student, int grade)
    {
        if (_students.Select(student => student.Name).Contains(student))
            return false;
        _students.Add((student, grade));
        return true;
    }

    public IEnumerable<string> Roster()
    {
        return _students
            .OrderBy(student => student.Grade)
            .ThenBy(student => student.Name)
            .Select(student => student.Name)
            .ToArray();
    }

    public IEnumerable<string> Grade(int grade)
    {
        return _students
            .Where(student => student.Grade == grade)
            .OrderBy(student => student.Name)
            .Select(student => student.Name)
            .ToArray();
    }
}