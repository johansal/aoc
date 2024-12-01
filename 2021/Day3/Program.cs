using System;
using System.IO;
using System.Collections.Generic;

namespace Day3
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(Part1("input.txt"));
        }

        private static int Part2(string input)
        {
            var linesA = new List<string>(File.ReadAllLines(input));
            var linesB = new List<string>(linesA);

            var OGR = OxygenGeneratorRating(linesA);
            var CO2SR = CO2ScrubberRating(linesB);
            return OGR * CO2SR;
        }
        public static int OxygenGeneratorRating(List<string> list)
        {
            for (var i = 0; list.Count > 1 && i < list[0].Length; i++)
            {
                list = GetIndexRating(list, i, true);
            }
            return Convert.ToInt32(list[0], 2);
        }
        public static int CO2ScrubberRating(List<string> list)
        {
            for (var i = 0; list.Count > 1 && i < list[0].Length; i++)
            {
                list = GetIndexRating(list, i, false);
            }
            return Convert.ToInt32(list[0], 2);
        }
        private static List<string> GetIndexRating(List<string> list, int i, bool isOGR)
        {
            List<string> ones = new();
            List<string> zeros = new();
            foreach (var line in list)
            {
                var c = line[i];
                if (c == '1')
                {
                    ones.Add(line);
                }
                else
                {
                    zeros.Add(line);
                }
            }
            if (isOGR)
                return ones.Count >= zeros.Count ? ones : zeros;
            return ones.Count < zeros.Count ? ones : zeros;
        }

        private static int Part1(string input)
        {
            int[] ones = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] zeros = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (var line in File.ReadAllLines(input))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    var value = int.Parse(line[i].ToString());
                    if (value == 0)
                    {
                        zeros[i]++;
                    }
                    else if (value == 1)
                    {
                        ones[i]++;
                    }
                    else
                    {
                        throw new Exception("value is " + value);
                    }
                }
            }
            return PowerConsumption(ones, zeros);
        }
        private static int PowerConsumption(int[] ones, int[] zeros)
        {
            string gammaRate = "";
            string epsilonRate = "";
            for (int i = 0; i < ones.Length; i++)
            {
                gammaRate += ones[i] > zeros[i] ? "1" : "0";
                epsilonRate += ones[i] > zeros[i] ? "0" : "1";
            }
            return Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
        }
    }
}
