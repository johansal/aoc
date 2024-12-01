using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            Console.WriteLine("Part 1: " + Part1(lines));
            Console.WriteLine("Part 2: " + Part2(lines));
        }
        private static int Part1(string[] lines)
        {
            var countedBags = new List<string>();
            var bags = new List<string>
            {
                "shiny gold"
            };

            while (bags.Count > 0)
            {
                List<string> updatedbags = new List<string>();
                for (var i = 0; i < bags.Count; i++)
                {
                    for (var j = 0; j < lines.Length; j++)
                    {
                        var bag = lines[j].Split(" bags contain ");
                        if (bag[1].Contains(bags[i]))
                        {
                            updatedbags.Add(bag[0]);
                            //Console.WriteLine(bag[0]);
                        }
                    }
                }
                countedBags.AddRange(bags);
                bags = updatedbags;
            }
            countedBags = countedBags.Distinct().ToList();
            return countedBags.Count - 1;// -1 for original shiny gold bag
        }

        private static int Part2(string[] lines)
        {
            var lList = new List<string>(lines);
            return bagContains(lList, "shiny gold", 1);
        }

        private static int bagContains(List<string> lines, string currBag, int currCount)
        {
            string line = lines.Find(x => x.Contains(currBag + " bags contain "));
            var splittedLine = line.Split(" bags contain ");
            if (splittedLine[1].Equals("no other bags."))
            {
                return 0;
            }
            else
            {
                var innerBags = splittedLine[1].Split(", ");
                int innerCount = 0;
                for(var i = 0; i < innerBags.Length; i++) {
                    var innerBag = innerBags[i].Split(" ");
                    int count = int.Parse(innerBag[0]);
                    string name = innerBag[1] + " " + innerBag[2];
                    innerCount += count + (count * bagContains(lines, name, count));
                }
                return innerCount;
            }
        }
    }
}
