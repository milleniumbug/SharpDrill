using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDrill
{
    public static class EnumerableExt
    {
        public static IEnumerable<int> Ints(int start = 0)
        {
            while(true)
            {
                yield return start++;
            }
        }

        public static IEnumerable<TElement> Repeat<TElement>(TElement element)
        {
            while(true)
            {
                yield return element;
            }
        }

        public static IEnumerable<TElement> Cycle<TElement>(this IReadOnlyCollection<TElement> elements)
        {
            while(true)
            {
                foreach(var element in elements)
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<TElement> Reversed<TElement>(this IReadOnlyList<TElement> input)
        {
            for(int i = input.Count - 1; i >= 0; i--)
            {
                yield return input[i];
            }
        }

        public static IEnumerable<(TIn1, TIn2)> Zip<TIn1, TIn2>(
            this IEnumerable<TIn1> left,
            IEnumerable<TIn2> right)
        {
            return left.Zip(right, (l, r) => (l, r));
        }

        public static TSource MinBy<TSource, TCompared>(
            this IEnumerable<TSource> elements,
            Func<TSource, TCompared> comparedCriterionSelector,
            IComparer<TCompared> comparer)
        {
            TSource currentMinimum = default(TSource);
            bool isFirst = true;
            foreach(var element in elements)
            {
                if(isFirst)
                {
                    currentMinimum = element;
                }
                else
                {
                    var comparisonResult = comparer.Compare(
                        comparedCriterionSelector(element),
                        comparedCriterionSelector(currentMinimum));
                    currentMinimum = comparisonResult < 0
                        ? element
                        : currentMinimum;
                }

                isFirst = false;
            }

            if(!isFirst)
            {
                return currentMinimum;
            }
            else
            {
                throw new ArgumentException("can't be an empty sequence", nameof(elements));
            }
        }

        public static TSource MinBy<TSource, TCompared>(
            this IEnumerable<TSource> elements,
            Func<TSource, TCompared> comparedCriterionSelector)
        {
            return MinBy(elements, comparedCriterionSelector, Comparer<TCompared>.Default);
        }

        public static TSource MaxBy<TSource, TCompared>(
            this IEnumerable<TSource> elements,
            Func<TSource, TCompared> comparedCriterionSelector,
            IComparer<TCompared> comparer)
        {
            var inverseComparer = Comparer<TCompared>.Create((lhs, rhs) => -Math.Sign(comparer.Compare(lhs, rhs)));
            return MinBy(elements, comparedCriterionSelector, inverseComparer);
        }

        public static TSource MaxBy<TSource, TCompared>(
            this IEnumerable<TSource> elements,
            Func<TSource, TCompared> comparedCriterionSelector)
        {
            return MaxBy(elements, comparedCriterionSelector, Comparer<TCompared>.Default);
        }
    }
}
