using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day7
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split(",").Select(x => int.Parse(x)).ToList();
            //Part1
            int median = input.Median();
            int cost = input.Sum(x => x-median <= 0 ? (median - x) : (x - median));
            Console.WriteLine("Part1: " + cost);
            //Part2, since mean is actually double, calculate fuel cost for both floor (mean) and cealing (mean2) and return cheaper option
            int mean = (int)input.Average();
            cost = input.Sum(x => x-mean <= 0 ? Enumerable.Range(1,mean - x).Sum() : Enumerable.Range(1,x - mean).Sum());
            int mean2 = mean + 1;
            int cost2 = input.Sum(x => x-mean2 <= 0 ? Enumerable.Range(1,mean2 - x).Sum() : Enumerable.Range(1,x - mean2).Sum());
            Console.WriteLine("Part2: " + Math.Min(cost, cost2));
        }

        private static int Partition<T>(this IList<T> list, int start, int end, Random rnd = null) where T : IComparable<T>
        {
            if (rnd != null)
                list.Swap(end, rnd.Next(start, end + 1));

            var pivot = list[end];
            var lastLow = start - 1;
            for (var i = start; i < end; i++)
            {
                if (list[i].CompareTo(pivot) <= 0)
                    list.Swap(i, ++lastLow);
            }
            list.Swap(end, ++lastLow);
            return lastLow;
        }

        public static T NthOrderStatistic<T>(this IList<T> list, int n, Random rnd = null) where T : IComparable<T>
        {
            return NthOrderStatistic(list, n, 0, list.Count - 1, rnd);
        }
        private static T NthOrderStatistic<T>(this IList<T> list, int n, int start, int end, Random rnd) where T : IComparable<T>
        {
            while (true)
            {
                var pivotIndex = list.Partition(start, end, rnd);
                if (pivotIndex == n)
                    return list[pivotIndex];

                if (n < pivotIndex)
                    end = pivotIndex - 1;
                else
                    start = pivotIndex + 1;
            }
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            if (i == j)   //This check is not required but Partition function may make many calls so its for perf reason
                return;
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static T Median<T>(this IList<T> list) where T : IComparable<T>
        {
            return list.NthOrderStatistic((list.Count - 1) / 2);
        }

        public static double Median<T>(this IEnumerable<T> sequence, Func<T, double> getValue)
        {
            var list = sequence.Select(getValue).ToList();
            var mid = (list.Count - 1) / 2;
            return list.NthOrderStatistic(mid);
        }
    }
}
