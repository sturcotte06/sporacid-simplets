namespace Sporacid.Simplets.Webapp.Tools.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension method library for IEnumerable objects.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Iterates through every objects of an enumeration and apply 
        /// an action on it.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="enumeration">An enumeration</param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Iterates through every objects of an enumeration and apply 
        /// an action on it.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="enumeration">An enumeration</param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<int, T> action)
        {
            var i = 0;
            foreach (var item in enumeration)
            {
                action(i, item);
                i++;
            }
        }

        /// <summary>
        /// Iterates through every objects of an array and apply 
        /// an action on it.
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="array">An array</param>
        /// <param name="action"></param>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (var item in array)
            {
                action(item);
            }
        }

        /// <summary>
        /// Iterates through every objects of an array and apply 
        /// an action on it.
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="array">An array</param>
        /// <param name="action"></param>
        public static void ForEach<T>(this T[] array, Action<int, T> action)
        {
            var i = 0;
            foreach (var item in array)
            {
                action(i, item);
                i++;
            }
        }

        /// <summary>
        /// Returns the index of a certain object using the default equality comparer
        /// for the type of the enumeration.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="enumeration">An enumeration</param>
        /// <param name="value">The value to find</param>
        /// <returns>The index if the value was found, or -1</returns>
        public static int IndexOf<T>(this IEnumerable<T> enumeration, T value)
        {
            return enumeration.IndexOf(value, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Returns the first index of an enumeration which match the equality
        /// selector.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="enumeration">An enumeration</param>
        /// <param name="equalitySelector">An equality selector</param>
        /// <returns>The index if the value was found, or -1</returns>
        public static int IndexOf<T>(this IEnumerable<T> enumeration, Func<T, bool> equalitySelector)
        {
            var index = 0;

            foreach (var item in enumeration)
            {
                if (equalitySelector(item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Returns the index of a certain object using the specified equality comparer.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="enumeration">An enumeration</param>
        /// <param name="value">The value to find</param>
        /// <param name="equalityComparer">The equality comparer</param>
        /// <returns>The index if the value was found, or -1</returns>
        public static int IndexOf<T>(this IEnumerable<T> enumeration, T value, EqualityComparer<T> equalityComparer)
        {
            var index = 0;
            foreach (var item in enumeration)
            {
                if (equalityComparer.Equals(item, value))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Returns the index of a certain object using the default equality comparer
        /// for the type of the array.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="array">An array</param>
        /// <param name="value">The value to find</param>
        /// <returns>The index if the value was found, or -1</returns>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return array.IndexOf(value, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Returns the first index of an enumeration which match the equality
        /// selector.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="array">An array</param>
        /// <param name="equalitySelector">An equality selector</param>
        /// <returns>The index if the value was found, or -1</returns>
        public static int IndexOf<T>(this T[] array, Func<T, bool> equalitySelector)
        {
            var index = 0;

            foreach (var item in array)
            {
                if (equalitySelector(item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Returns the index of a certain object using the specified equality comparer.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="array">An array</param>
        /// <param name="value">The value to find</param>
        /// <param name="equalityComparer">The equality comparer</param>
        /// <returns>The index if the value was found, or -1</returns>
        public static int IndexOf<T>(this T[] array, T value, EqualityComparer<T> equalityComparer)
        {
            var index = 0;
            foreach (var item in array)
            {
                if (equalityComparer.Equals(item, value))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static bool None<T>(this IEnumerable<T> enumeration)
        {
            return !enumeration.Any();
        }

        /// <summary>
        /// Shuffles an enumeration using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="enumeration">The enumeration to shuffle</param>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumeration)
        {
            return enumeration.ToList().Shuffle();
        }

        /// <summary>
        /// Shuffles a list using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">Type of the list</typeparam>
        /// <param name="list">The list to shuffle</param>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var random = new Random();
            var n = list.Count;

            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}