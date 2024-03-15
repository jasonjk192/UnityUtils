using System.Collections.Generic;

namespace WinterCrestal.Extensions
{
    public static class ArraysExtensions
    {
        public static void ShiftRight<T>(this T[] array)
        {
            var lastIdx = array.Length - 1;
            var right = array[lastIdx];
            System.Array.Copy(array, 0, array, 1, array.Length - 1);
            array[0] = right;
        }

        public static void ShiftRight<T>(this T[] array, int count)
        {
            if (count <= 0) return;
            if (count == 1) { ShiftRight(array); return; }

            var lastIdx = array.Length - count;
            var rightArray = new T[count];
            System.Array.Copy(array, lastIdx, rightArray, 0, count);
            System.Array.Copy(array, 0, array, count, lastIdx);
            System.Array.Copy(rightArray, 0, array, 0, count);
        }

        public static void ShiftLeft<T>(this T[] array)
        {
            var lastIdx = array.Length - 1;
            var left = array[0];
            System.Array.Copy(array, 1, array, 0, lastIdx);
            array[lastIdx] = left;
        }

        public static void ShiftLeft<T>(this T[] array, int count)
        {
            if (count <= 0) return;
            if (count == 1) { ShiftLeft(array); return; }

            var lastIdx = array.Length - count;
            var leftArray = new T[count];
            System.Array.Copy(array, 0, leftArray, 0, count);
            System.Array.Copy(array, count, array, 0, lastIdx);
            System.Array.Copy(leftArray, 0, array, lastIdx, count);
        }

        public static T FindFirst<T>(this T[] array, System.Predicate<T> predicate, int offset = 0)
        {
            for (int i = offset; i < array.Length; i++)
                if (predicate(array[i]))
                    return array[i];
            return default;
        }

        public static T FindLast<T>(this T[] array, System.Predicate<T> predicate, int offset = 0)
        {
            for (int i = array.Length - 1 - offset; i >= 0; i++)
                if (predicate(array[i]))
                    return array[i];
            return default;
        }

        public static T[] FindAll<T>(this T[] array, System.Predicate<T> predicate)
        {
            List<T> list = new();
            for (int i = 0; i < array.Length; i++)
                if (predicate(array[i]))
                    list.Add(array[i]);
            return list.ToArray();
        }
    }
}

