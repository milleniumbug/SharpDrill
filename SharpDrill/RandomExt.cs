using System;
using System.Collections.Generic;

namespace SharpDrill
{
    public static class RandomExt
    {
        public static void Shuffle<TElement>(this Random rand, IList<TElement> list)
        {
            int n = list.Count;
            while(n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T Choice<T>(this Random rand, IReadOnlyList<T> list)
        {
            return list[rand.Next(list.Count)];
        }
    }
}
