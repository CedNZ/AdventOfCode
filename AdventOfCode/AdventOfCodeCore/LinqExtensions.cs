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

        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> source)
        {
            for (int i = 0; i < source.Count(); i++)
            {
                for (int j = i + 1; j < source.Count(); j++)
                {
                    yield return new (source.Skip(i).Take(1).Single(), source.Skip(j).Take(1).Single());
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

        public static IEnumerable<Tuple<T, T>> CartesianPairs<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                var others = source.Where(s => !s.Equals(item));
                foreach (var otherItem in others)
                {
                    yield return new (item, otherItem);
                }
            }
        }

        public static IEnumerable<T> SkipUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            bool found = false;
            foreach (var item in source)
            {
                if (found is false && predicate(item))
                {
                    found = true;
                }
                if (found)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            bool found = false;
            foreach (var item in source)
            {
                if (found is false && predicate(item))
                {
                    found = true;
                    break;
                }
                yield return item;
            }
        }

        public static List<List<T>> ClusterWhile<T>(this IEnumerable<T> source,  Func<T, bool> predicate, bool? orderDescending = null) 
        {
            return ClusterWhile(source, x => x, (x, _) => predicate(x), orderDescending);
        }

        public static List<List<T>> ClusterWhile<T, Tx>(this IEnumerable<T> source, Func<T, Tx> selector, Func<Tx, Tx, bool> clusterPredicate, bool? orderDescending = null)
        {
            List<List<T>> clusters = [];
            int clusterCount() => clusters.Sum(c => c.Count);
            List<T> items = orderDescending switch
            {
                true => [.. source.OrderByDescending(selector)],
                false => [.. source.OrderBy(selector)],
                null => [.. source]
            };

            while (clusterCount() != source.Count())
            {
                clusters.Add(
                    items.Skip(clusterCount())
                    .TakeWhile((x, i) => i == 0 || clusterPredicate(selector(x), selector(items[i - 1])))
                    .ToList());
            }
            return clusters;
        }

        public static long EuclideanDistance<T>(this Tuple<T, T> tuple, params List<Func<T, long>> selectors)
        {
            var squared = 0L;
            foreach (var selector in selectors)
            {
                squared += (long)Math.Pow(selector(tuple.Item1) - selector(tuple.Item2), 2);
            }

            return (long)Math.Sqrt(squared);
        }

        public static bool HasOnlyOneBitSet(this int number)
        {
            // Check if number is a power of two
            return (number != 0) && ((number & (number - 1)) == 0);
        }

        public static long CalculateLCM(this IEnumerable<long> numbers)
        {
            return numbers.Aggregate(LCM);
        }

        public static long LCM(long a, long b)
        {
            if (a == 0 && b == 0)
            {
                return 0;
            }
            // Calculate LCM using the formula: LCM(a, b) = |a * b| / GCD(a, b)
            return Math.Abs(a * b) / GCD(a, b);
        }

        public static long GCD(long a, long b)
        {
            // Calculate GCD using Euclidean algorithm
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
