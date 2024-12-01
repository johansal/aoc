using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace day_10
{
    public static class Program
    {
        public static void Main()
        {
            string[] lines = File.ReadAllLines("test_small.txt", Encoding.UTF8);
            List<int> voltages = lines.Select(x => int.Parse(x)).ToList();
            voltages.Sort();
            Console.WriteLine("Part1: " + Part1(voltages));
            Console.WriteLine("Part2: " + Part2(voltages));
        }
        public static int Part1(List<int> voltages)
        {
            var count1 = 0;
            var count3 = 1;
            var prev = 0;
            for (var i = 0; i < voltages.Count; i++)
            {
                if (voltages[i] - prev == 1)
                    count1++;
                else if (voltages[i] - prev == 3)
                    count3++;
                prev = voltages[i];
            }
            return count1 * count3;
        }
        public static Int64 Part2(List<int> voltages)
        {
           voltages.Insert(0,0);
           voltages.Add(voltages[^1] + 3); //^1 = last index

            //keep count of how many paths is to each index
            var paths = new Int64[voltages.Count];

            //loop all indexes and check if previous indexes were <=3 diff, if so accumulate paths of those previous indexes to this index
            //last index holds the number of total diffs
            for (var i = 0; i < paths.Length; i++) {
                if(i == 0) {
                    paths[i] = 1;
                }
                else {
                    paths[i] = 0;
                    for(var j = i - 1; j >= 0; j--) {
                        if(voltages[i] - voltages[j] <= 3) {
                            paths[i] += paths[j];
                            Console.WriteLine("pathd["+i+"]=" + paths[i]);
                        }
                        else {
                            break;
                        }
                    }
                }
            }
            return paths[^1];
        }
        /* recursive solution, doesn't work :( test with voltages.add 0 and max+3
        public static int CountNext(List<int> voltages, int index, int prev)
        {
            var currCount = 0;
            if (index == voltages.Count - 1)
            {
                return 1;
            }
            else
            {
                for (var i = index; i < voltages.Count; i++)
                {
                    if (voltages[i] - prev <= 3)
                    {
                        //Console.WriteLine(voltages[i] + " can be connected to " + prev);
                        currCount += CountNext(voltages, i + 1, voltages[i]);
                    }
                    else
                    {
                        break;
                    }
                }
                return currCount;
            }
        }*/
    }
}