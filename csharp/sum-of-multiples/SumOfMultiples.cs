using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        return multiples
            .Where(n => n != 0)
            .SelectMany(generateMultiples)
            .Distinct()
            .Sum();

        IEnumerable<int> generateMultiples(int n)
        {
            for (int i = 0; i < max; i+=n) {
                yield return i;
            }
        }
    }
}