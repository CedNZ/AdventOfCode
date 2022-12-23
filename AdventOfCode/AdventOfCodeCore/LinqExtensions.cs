using System;
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

        public static IEnumerable<T[]> Pairs<T>(this IEnumerable<T> source)
        {
            for (int i = 0; i < source.Count(); i++)
            {
                for (int j = i + 1; j < source.Count(); j++)
                {
                    yield return new[] { source.Skip(i).Take(1).Single(), source.Skip(j).Take(1).Single() };
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                from accseq in accumulator
                from item in sequence
                select accseq.Concat(new[] { item }));
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
