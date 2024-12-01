using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day9
{
    class Program
    {
        public static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int[][] heightMap = new int[input.Length][];
            List<int> basinSizes = new();
            //parse input to 2d array
            for (var line = 0; line < input.Length; line++)
            {
                heightMap[line] = new int[input[line].Length];
                for (int i = 0; i < input[line].Length; i++)
                {
                    heightMap[line][i] = int.Parse(input[line][i].ToString());
                }
            }
            //calculate risksum
            int risksum = 0;
            for (int i = 0; i < heightMap.Length; i++)
            {
                for (int j = 0; j < heightMap[i].Length; j++)
                {
                    var risk = RiskLevel(heightMap, i, j);
                    if(risk > 0) {
                        risksum += risk;
                        basinSizes.Add(BasinSize(ref heightMap, i, j));
                    }
                }
            }
            Console.WriteLine("Part 1: " + risksum);
            var basinProduct = basinSizes.OrderByDescending(i => i).Take(3).Aggregate(1, (x,y) => x * y);
            Console.WriteLine("Part 2: " + basinProduct);
        }
        public static int RiskLevel(int[][] map, int i, int j)
        {
            if (map[i][j] >= 9)
                return 0;
            if (i > 0 && map[i][j] >= map[i - 1][j])
            {
                return 0;
            }
            if (i < map.Length - 1 && map[i][j] >= map[i + 1][j])
            {
                return 0;
            }
            if (j > 0 && map[i][j] >= map[i][j - 1])
            {
                return 0;
            }
            if (j < map[0].Length - 1 && map[i][j] >= map[i][j + 1])
            {
                return 0;
            }
            //this is lowpoint
            return map[i][j] + 1;
        }
        public static int BasinSize(ref int[][] map, int i, int j)
        {
            int size = 1;
            //mark this visited
            map[i][j] = 10;
            if (i > 0 && map[i - 1][j] < 9)
            {
                size += BasinSize(ref map, i - 1, j);
            }
            if (i < map.Length - 1 && map[i + 1][j] < 9)
            {
                size += BasinSize(ref map, i + 1, j);
            }
            if (j > 0 && map[i][j - 1] < 9)
            {
                size += BasinSize(ref map, i, j - 1);
            }
            if (j < map[0].Length - 1 && map[i][j + 1] < 9)
            {
                size += BasinSize(ref map, i, j + 1);
            }
            return size;
        }
    }
}
