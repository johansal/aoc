using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    public static class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            List<(int x, int y, int z)> activePositions = new();
            for (int i = 0; i < 20; i++)
            {
                ParseLine(lines[i], ref activePositions);
                Console.WriteLine("Line " + i + ": " + activePositions.Count);
            }
            Console.WriteLine("Part 1: " + activePositions.Count);
        }
        public static void ParseLine(string line, ref List<(int x, int y, int z)> activePositions)
        {
            var tmp = line.Split(" ");
            bool on = tmp[0].Equals("on");
            var ranges = ParseRange(tmp[1].Split(","));

            foreach (var xPos in ranges[0])
            {
                foreach (var yPos in ranges[1])
                {
                    foreach (var zPos in ranges[2])
                    {
                        var n = (xPos, yPos, zPos);
                        if (on && !activePositions.Contains(n))
                        {
                            activePositions.Add(n);
                        }
                        else if(!on && activePositions.Contains(n)) {
                            activePositions.Remove(n);
                        }
                    }
                }
            }
        }
        public static List<IEnumerable<int>> ParseRange(string[] ranges)
        {
            List<IEnumerable<int>> ret = new();
            foreach (var range in ranges)
            {
                var tmp = range.Split("=")[1].Split("..");
                int start = int.Parse(tmp[0]);
                int end = int.Parse(tmp[1]);
                var r = Enumerable.Range(start, end - start + 1);
                ret.Add(r);
            }
            return ret;
        }

        //reactor code from 2020 day 17
        public static (List<List<List<string>>>, int) SimulateCycle(List<List<List<string>>> cube)
        {
            List<List<List<string>>> newCube = new List<List<List<string>>>();
            int activeCount = 0;
            for (int w = -1; w <= cube.Count; w++)
            {
                newCube.Add(new List<List<string>>());
                for (int z = -1; z <= cube[0].Count; z++)
                {
                    newCube[w + 1].Add(new List<string>());
                    for (int y = -1; y <= cube[0][0].Count; y++)
                    {
                        newCube[w + 1][z + 1].Add("");
                        for (int x = -1; x <= cube[0][0][0].Length; x++)
                        {
                            char c = ChangeState(x, y, z, w, cube);
                            newCube[w + 1][z + 1][y + 1] += c;
                            if (c == '#') activeCount++;
                        }
                    }
                }
            }
            return (newCube, activeCount);
        }
        public static char ChangeState(int x, int y, int z, int w, List<List<List<string>>> cube)
        {
            bool isActive = w >= 0 && w < cube.Count &&
                    z >= 0 && z < cube[w].Count &&
                    y >= 0 && y < cube[w][z].Count &&
                    x >= 0 && x < cube[w][z][y].Length && cube[w][z][y][x] == '#';
            int activeCount = 0;
            for (int i = 0; i < 81; i++)
            {
                if (i == 40) continue; //current pos
                int wd = (i % 3) - 1;
                int zd = ((i / 3) % 3) - 1;
                int yd = ((i / 9) % 3) - 1;
                int xd = (i / 27) - 1;

                if (w + wd >= 0 && w + wd < cube.Count &&
                    z + zd >= 0 && z + zd < cube[w + wd].Count &&
                    y + yd >= 0 && y + yd < cube[w + wd][z + zd].Count &&
                    x + xd >= 0 && x + xd < cube[w + wd][z + zd][y + yd].Length &&
                    cube[w + wd][z + zd][y + yd][x + xd] == '#') { activeCount++; }
            }

            return (isActive && (activeCount == 2 || activeCount == 3)) || (!isActive && activeCount == 3) ? '#' : '.';
        }
    }
}
