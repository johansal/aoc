using System;
using System.IO;
using System.Text;

namespace day_1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Part1:");
            Part1();
            Console.WriteLine("Part2:");
            Part2();
        }
        private static void Part1()
        {
            var inputLines = File.ReadAllLines("input.txt", Encoding.UTF8);
            for (var i = 0; i < inputLines.Length; i++)
            {
                for (var j = 0; j < inputLines.Length; j++)
                {
                    if (i != j && Int32.Parse(inputLines[i]) + Int32.Parse(inputLines[j]) == 2020)
                    {
                        Console.WriteLine(Int32.Parse(inputLines[i]) * Int32.Parse(inputLines[j]));
                        return;
                    }
                }
            }
        }
        private static void Part2()
        {
            var inputLines = File.ReadAllLines("input.txt", Encoding.UTF8);
            for (var i = 0; i < inputLines.Length; i++)
            {
                for (var j = 0; j < inputLines.Length; j++)
                {
                    var intI = Int32.Parse(inputLines[i]);
                    var intJ = Int32.Parse(inputLines[j]);
                    if (i != j && intI + intJ <= 2020)
                    {
                        for (var o = 0; o < inputLines.Length; o++)
                        {
                            var intO = Int32.Parse(inputLines[o]);
                            if (o != i && o != j && intI + intJ + intO == 2020)
                            {
                                Console.WriteLine(intI * intJ * intO);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
