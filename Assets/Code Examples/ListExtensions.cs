using System;
using System.Collections.Generic;

namespace AbonyInt.Extensions
{
    public static class ListExtensions
    {
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Swap(list, i, rnd.Next(i, list.Count));
            }
        }

        public static T RandomItem<T>(this IList<T> list)
        {
            int i = UnityEngine.Random.Range(0, list.Count);
            return list[i];
        }
    }
}
