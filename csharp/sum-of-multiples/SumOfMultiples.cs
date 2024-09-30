using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        return multiples
            .Where(n => n != 0)
            .SelectMany(n =>
                Enumerable
                .Range(0, max)
                .Where(x =>
                    x % n == 0
                )
            )
            .Distinct()
            .Sum();
    }
}