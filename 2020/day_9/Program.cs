using System;
using System.IO;
using System.Text;

namespace day_9
{
    class Program
    {
        public static void Main()
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            const int preamble = 25;
            Int64 weakness = Part1(lines, preamble);
            Console.WriteLine("Part 1: " + weakness);
            Console.WriteLine("Part 2: " + Part2(lines, weakness));
        }
        public static Int64 Part1(string[] lines, int preamble)
        {
            for (int i = preamble; i < lines.Length; i++)
            {
                bool preamleHasSum = false;
                for (int j = i - preamble; j < i; j++)
                {
                    for (int o = i - preamble; o < i; o++)
                    {
                        if (j != o && Int64.Parse(lines[j]) + Int64.Parse(lines[o]) == Int64.Parse(lines[i]))
                        {
                            preamleHasSum = true;
                            break;
                        }
                    }
                    if (preamleHasSum)
                        break;
                }
                if (!preamleHasSum)
                {
                    return Int64.Parse(lines[i]);
                }
            }
            throw new Exception("Weakness not found!");
        }
        public static Int64 Part2(string[] lines, Int64 weakness)
        {
            for (int i = 0; i < lines.Length - 1; i++)
            {
                Int64 counter = Int64.Parse(lines[i]);
                Int64 rangeMin = counter;
                Int64 rangeMax = counter;
                for (int j = i + 1; j < lines.Length; j++)
                {
                    var tmp = Int64.Parse(lines[j]);
                    if(tmp < rangeMin)
                        rangeMin = tmp;
                    if(tmp > rangeMax)
                        rangeMax = tmp;
                    counter += tmp;
                    if (counter == weakness)
                    {
                        return rangeMin + rangeMax;
                    }
                    else if (counter > weakness)
                    {
                        break;
                    }
                }
            }
            throw new Exception("Encryption weakness not found!");
        }

    }
}
