using System;
using System.Collections.Generic;
using System.Linq;

namespace day_15
{
    public static class Program
    {
        public static void Main()
        {
            Solve(2020, 1);
            Solve(30000000, 2);
        }

        private static void Solve(int l, int part)
        {
            int[] input = { 7, 14, 0, 17, 11, 1, 2 };
            Dictionary<int, int> spoken = new Dictionary<int, int>();
            int nextValue = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < l - 1; i++)
            {
                if (i < input.Length)
                {
                    spoken[input[i]] = i;
                }
                else
                {
                    var lastValue = nextValue;
                    nextValue = spoken.ContainsKey(nextValue) ? i - spoken[nextValue] : 0;
                    spoken[lastValue] = i;
                }
            }
            watch.Stop();
            //Find key by value from dictionary
            Console.WriteLine("Part " + part + ": " + nextValue + " (" + watch.ElapsedMilliseconds + "ms)");
        }
    }
}
