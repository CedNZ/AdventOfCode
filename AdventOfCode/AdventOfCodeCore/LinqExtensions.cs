﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeCore
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> OverlappingSets<T>(this IEnumerable<T> source, int setSize)
        {
            while (source.Any())
            {
                yield return source.Take(setSize);
                source = source.Skip(1);
            }
        }

        public static IEnumerable<List<T>> CartesianPairs<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                var others = source.Where(s => !s.Equals(item));
                foreach (var otherItem in others)
                {
                    yield return new List<T>{ item, otherItem };
                }
            }
        }
    }
}
