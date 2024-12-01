using System;
using System.IO;
using System.Text;

namespace day_13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            int arrival = int.Parse(lines[0]);
            int minWaitTime = arrival;
            int minWaitTimeId = 0;
            string[] busIds = lines[1].Split(",");
            for (var i = 0; i < busIds.Length; i++)
            {
                if (busIds[i] != "x")
                {
                    //Console.WriteLine("n = " + busIds[i] + ", x = " + i);
                    int interval = int.Parse(busIds[i]);
                    int waitTime = interval - (arrival % interval);
                    if (waitTime < minWaitTime)
                    {
                        minWaitTime = waitTime;
                        minWaitTimeId = interval;
                    }
                }
            }
            Console.WriteLine("Part 1: " + (minWaitTime * minWaitTimeId));

            // t % busIds[0] = (t + 1) % busIds[1] = (t + 2) % busIds[2] = 0
            // (t + x) % busIds[x] = 0
            //Chinese remainder theorem with search by sieving solution
            // busIds[x] = n'x
            // when x = 0 
            // find min t, by trying 1,2,3...
            // then when x = 1
            // find min t, by starting trying from last min t and increase it by n'x-1
            // increase count can be multiplied by n'x-1 for each new x.
            // Solution could be faster if we order the original array as decr. 
            // and store original indexes for x to another array.

            long t = 1;
            long count = 1;
            for (var x = 0; x < busIds.Length; x++)
            {
                if (busIds[x] != "x")
                {
                    long n = long.Parse(busIds[x]);
                    bool found = false;
                    while(!found) 
                    {
                        found = ((t+=count) + x) % n == 0;
                    }
                    count *= n;
                }
            }
            Console.WriteLine("Part 2: " + t);
        }
    }
}
