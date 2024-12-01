using System;
using System.IO;
using System.Collections.Generic;

namespace day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            List<Point> points = new();

            foreach (var inputLine in input)
            {
                //parse line from input
                var line = new Line(inputLine);
                //parse points from lines (consider only vertical and horizontals for part 1)
                var linePoints = line.GetAllPoints();
                //check if point is already in list, if it is increase value, else add new point
                //Console.WriteLine("printing line:");
                //line.start.Print();
                //line.end.Print();
                //Console.WriteLine("linepoints on this line " + linePoints.Count);
                //todo: this is stupidly slow
                foreach (var linePoint in linePoints)
                {
                    //linePoint.Print();
                    bool found = false;
                    foreach (var point in points)
                    {
                        if (point.X == linePoint.X && point.Y == linePoint.Y)
                        {
                            found = true;
                            point.Value++;
                        }
                    }
                    if (!found)
                        points.Add(linePoint);
                }
            }
            //print number of points that have value >=2
            int counter = 0;
            foreach (var point in points)
            {
                if (point.Value > 1)
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Value { get; set; }

        public void Print()
        {
            Console.WriteLine(this.X + " " + this.Y + " " + this.Value);
        }
    }
    public class Line
    {
        public Point start { get; set; }
        public Point end { get; set; }

        public Line(string str)
        {
            var splitted = str.Split(" -> ");
            var point1 = splitted[0].Split(",");
            var point2 = splitted[1].Split(",");
            this.start = new Point
            {
                X = int.Parse(point1[0]),
                Y = int.Parse(point1[1])
            };
            this.end = new Point
            {
                X = int.Parse(point2[0]),
                Y = int.Parse(point2[1])
            };
        }
        public List<Point> GetAllPoints()
        {
            List<Point> line = new();
            if (this.start.X == this.end.X)
            {
                for (int i = Math.Min(this.start.Y, this.end.Y); i <= Math.Max(this.start.Y, this.end.Y); i++)
                {
                    Point tmp = new Point
                    {
                        X = this.start.X,
                        Y = i,
                        Value = 1
                    };
                    line.Add(tmp);
                }
            }
            else if (this.start.Y == this.end.Y)
            {
                for (int i = Math.Min(this.start.X, this.end.X); i <= Math.Max(this.start.X, this.end.X); i++)
                {
                    Point tmp = new Point
                    {
                        X = i,
                        Y = this.start.Y,
                        Value = 1
                    };
                    line.Add(tmp);
                }
            }
            else if (Math.Max(this.start.X, this.end.X) - Math.Min(this.start.X, this.end.X) ==
                Math.Max(this.start.Y, this.end.Y) - Math.Min(this.start.Y, this.end.Y))
            {
                int y = this.start.X < this.end.X ? this.start.Y : this.end.Y;
                int yInc = (this.start.X < this.end.X && this.start.Y > this.end.Y) || (this.start.X > this.end.X && this.start.Y < this.end.Y) ? -1 : 1;
                for (int i = Math.Min(this.start.X, this.end.X); i <= Math.Max(this.start.X, this.end.X); i++)
                {
                    Point tmp = new Point
                    {
                        X = i,
                        Y = y,
                        Value = 1
                    };
                    y += yInc;
                    line.Add(tmp);
                }
            }
            return line;
        }
    }
}
