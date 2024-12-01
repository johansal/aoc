using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_3
    {
        public static string firstPuzzle(string location)
        {
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);

            //Map all lines
            List<Point> line1 = mapLine(lines[0]);
            Console.WriteLine(line1.Count.ToString());
            List<Point> line2 = mapLine(lines[1]);
            Console.WriteLine(line2.Count.ToString());
            //Find all intersections
            List<Point> intersections = line1.Intersect(line2).ToList();
            Console.WriteLine(intersections.Count.ToString());
            //Get smallest manhattan distance from O
            intersections = intersections.OrderBy(p => p.ManhattanDistance).ToList();
            //Output the nearest (skip 1st intersection at O)
            return intersections.Count > 1 ? intersections[1].ManhattanDistance.ToString() : "wtf?";
        }
        public static string secondPuzzle(string location)
        {
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);

            //Map all lines
            List<Point> line1 = mapLine(lines[0]);
            Console.WriteLine(line1.Count.ToString());
            List<Point> line2 = mapLine(lines[1]);
            Console.WriteLine(line2.Count.ToString());
            //Find all intersections
            List<Point> intersections = line1.Intersect(line2).ToList();
            Console.WriteLine(intersections.Count.ToString());
            //Find smalles step distances from O
            var stepDistance = 0;
            foreach(Point intersection in intersections) {
                var steps = line1.IndexOf(intersection) + line2.IndexOf(intersection);
                if (steps < stepDistance || stepDistance == 0)
                    stepDistance = steps;
            }
            //Output the nearest
            return stepDistance.ToString(); //19242 for real and 610 for test input
        }
        private static List<Point> mapLine(string line)
        {
            Point currentPosition = new Point
            {
                X = 0,
                Y = 0
            };
            List<Point> lineCoordinates = new List<Point>();
            lineCoordinates.Add(currentPosition);
            string[] directions = line.Split(',');
            foreach (string direction in directions)
            {
                lineCoordinates.AddRange(mapPoints(direction, currentPosition));
                currentPosition = lineCoordinates[lineCoordinates.Count - 1];
            }
            return lineCoordinates;
        }
        private static List<Point> mapPoints(string direction, Point currentPoint)
        {
            List<Point> points = new List<Point>();
            //R, L, U, D
            string angle = direction.Substring(0, 1);
            int length = Convert.ToInt32(direction.Substring(1));
            if (angle.Equals("R"))
            {
                for (int i = 1; i <= length; i++)
                {
                    Point point = new Point
                    {
                        X = currentPoint.X + i,
                        Y = currentPoint.Y
                    };
                    points.Add(point);
                }
            }
            else if (angle.Equals("L"))
            {
                for (int i = 1; i <= length; i++)
                {
                    Point point = new Point
                    {
                        X = currentPoint.X - i,
                        Y = currentPoint.Y
                    };
                    points.Add(point);
                }
            }
            else if (angle.Equals("U"))
            {
                for (int i = 1; i <= length; i++)
                {
                    Point point = new Point
                    {
                        X = currentPoint.X,
                        Y = currentPoint.Y + i
                    };
                    points.Add(point);
                }
            }
            else if (angle.Equals("D"))
            {
                for (int i = 1; i <= length; i++)
                {
                    Point point = new Point
                    {
                        X = currentPoint.X,
                        Y = currentPoint.Y - i
                    };
                    points.Add(point);
                }
            }
            return points;
        }
    }
    public class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ManhattanDistance
        {
            get
            {
                return Math.Abs(this.X) + Math.Abs(this.Y);
            }
        }

        public bool Equals(Point other)
        {
            if (other is null)
                return false;
            
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj) => Equals(obj as Point);
        public override int GetHashCode() => (X, Y, ManhattanDistance).GetHashCode();
    }
}
