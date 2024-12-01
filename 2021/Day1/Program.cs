using System;
using System.IO;
using System.Collections.Generic;

namespace AoC_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 1, part 1: " + MeasureDepthIncreases("inputs/day_1.txt"));
            Console.WriteLine("Day 1, part 2: " + MeasureSlidingWindowDepthIncreases("inputs/day_1.txt"));
        }

        // Day 1, part 1 (1564)
        private static int MeasureDepthIncreases(string input) {
            var counter = 0;
            var lines = File.ReadAllLines(input);
            for(int i = 0; i < lines.Length-1; i++) {
                if(int.Parse(lines[i]) < int.Parse(lines[i+1]))
                    counter++;
            }
            return counter;
        }

        // Day 1, part 2 (1611)
        // this cheats a bit since it should sum 3 values (window) 
        // and check and compares it against the next window, 
        // but since the windows overlap it only compares the parts that won't overlap
        // ...if this is needed in another day and the windows are defined
        // differently, I have to make this properly,
        private static int MeasureSlidingWindowDepthIncreases(string input) {
            var counter = 0;
            var lines = File.ReadAllLines(input);
            for(int i = 0; i < lines.Length-3; i++) {
                if(int.Parse(lines[i+3]) > int.Parse(lines[i]))
                    counter++;
            }
            return counter;
        }
    }
}
