using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            List<(int x, int y)> points = new();
            foreach (var line in input)
            {
                if (line.StartsWith('f'))
                {
                    //fold paper
                    var foldLine = line.Split(" ")[2].Split("=");
                    var axel = foldLine[0];
                    var coordinate = int.Parse(foldLine[1]);
                    List<(int x, int y)> foldedPoints = new();
                    foreach (var point in points)
                    {
                        if (axel == "x")
                        {
                            if (point.x > coordinate)
                            {
                                (int x, int y) p = new()
                                {
                                    x = coordinate - (point.x-coordinate),
                                    y = point.y
                                };
                                foldedPoints.Add(p);
                            }
                            else
                            {
                                foldedPoints.Add(point);
                            }
                        }
                        else
                        {
                            if (point.y > coordinate)
                            {
                                (int x, int y) p = new()
                                {
                                    x = point.x,
                                    y = coordinate - (point.y-coordinate)
                                };
                                foldedPoints.Add(p);
                            }
                            else
                            {
                                foldedPoints.Add(point);
                            }
                        }
                    }
                    points = foldedPoints.Distinct().ToList();
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    var pointStr = line.Split(",");
                    (int x, int y) point = new()
                    {
                        x = int.Parse(pointStr[0]),
                        y = int.Parse(pointStr[1])
                    };
                    points.Add(point);
                }
            }
            Print(points);
        }
        public static void Print(List<(int x, int y)> coordinates) {
            int maxX = coordinates.Max(p => p.x);
            int maxY = coordinates.Max(p => p.y);
            for(int i = 0; i <= maxY; i++) {
                for(int j = 0; j <= maxX; j++) {
                    if(coordinates.Contains((j,i))) {
                        Console.Write("#");
                    }
                    else {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
