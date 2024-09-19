using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        HashSet<int> numbers = new();
        foreach (var baseValue in multiples) {
            if (baseValue <= 0)
                continue;
            var i = 0;
            int multiple;
            while ((multiple = baseValue * i++) < max)
            {
                numbers.Add(multiple);
            }
        }
        return numbers.Sum(n => n);
    }
}