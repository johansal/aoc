using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_15
    {

        Dictionary<Point, long> SeenPoints = new Dictionary<Point, long>();
        long[,] Board = null;
        long[,] FloodLvls = null;
        bool[,] Seen = null;

        Point StartPoint = null;
        Point TargetPoint = null;

        IntComputer ic = null;

        public string firstPuzzle(string input)
        {

            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            ic = new IntComputer(intcode);



            Point og = new Point { X = 0, Y = 0 };
            StartPoint = og;

            CheckAdjacentPoints(og);

            var minX = SeenPoints.Keys.Min(k => k.X);
            var maxX = SeenPoints.Keys.Max(k => k.X);
            var minY = SeenPoints.Keys.Min(k => k.Y);
            var maxY = SeenPoints.Keys.Max(k => k.Y);

            var offsetX = Math.Abs(minX);
            var offsetY = Math.Abs(minY);

            var sizeX = maxX + offsetX;
            var sizeY = maxY + offsetY;

            StartPoint = new Point { X = StartPoint.X + offsetX, Y = StartPoint.Y + offsetY };
            TargetPoint = new Point { X = TargetPoint.X + offsetX, Y = TargetPoint.Y + offsetY };

            Board = new long[sizeY + 1, sizeX + 1];
            FloodLvls = new long[sizeY + 1, sizeX + 1];
            Seen = new bool[sizeY + 1, sizeX + 1];

            foreach (var p in SeenPoints)
            {
                Board[p.Key.Y + offsetY, p.Key.X + offsetX] = p.Value;
            }

            FloodFill(StartPoint);
            return FloodLvls[TargetPoint.Y, TargetPoint.X].ToString();

        }

        public string secondPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            ic = new IntComputer(intcode);



            Point og = new Point { X = 0, Y = 0 };
            StartPoint = og;

            CheckAdjacentPoints(og);

            var minX = SeenPoints.Keys.Min(k => k.X);
            var maxX = SeenPoints.Keys.Max(k => k.X);
            var minY = SeenPoints.Keys.Min(k => k.Y);
            var maxY = SeenPoints.Keys.Max(k => k.Y);

            var offsetX = Math.Abs(minX);
            var offsetY = Math.Abs(minY);

            var sizeX = maxX + offsetX;
            var sizeY = maxY + offsetY;

            StartPoint = new Point { X = StartPoint.X + offsetX, Y = StartPoint.Y + offsetY };
            TargetPoint = new Point { X = TargetPoint.X + offsetX, Y = TargetPoint.Y + offsetY };

            Board = new long[sizeY + 1, sizeX + 1];
            FloodLvls = new long[sizeY + 1, sizeX + 1];
            Seen = new bool[sizeY + 1, sizeX + 1];

            foreach (var p in SeenPoints)
            {
                Board[p.Key.Y + offsetY, p.Key.X + offsetX] = p.Value;
            }

            FloodFill(TargetPoint);
            long timeStepsToFlood = FloodLvls.Cast<long>().Max();
            return timeStepsToFlood.ToString();
        }
        void CheckAdjacentPoints(Point p)
        {
            //Try different directions, map the seen point and step back
            var newPoint = new Point { X = p.X, Y = p.Y - 1 };
            if (SeenPoints.ContainsKey(newPoint) == false)
            {
                var valueAtTargetPoint = MoveInDirection(1);
                SeenPoints.Add(newPoint, valueAtTargetPoint);

                if (valueAtTargetPoint != 0)
                {
                    CheckAdjacentPoints(newPoint);
                    MoveInDirection(2);
                    if (valueAtTargetPoint == 2)
                    {
                        SeenPoints[newPoint] = 1;
                        TargetPoint = new Point { X = newPoint.X, Y = newPoint.Y };
                    }
                }
            }
            newPoint = new Point { X = p.X + 1, Y = p.Y };
            if (SeenPoints.ContainsKey(newPoint) == false)
            {
                var moveResult = MoveInDirection(4);
                SeenPoints.Add(newPoint, moveResult);

                if (moveResult != 0)
                {
                    CheckAdjacentPoints(newPoint);
                    MoveInDirection(3);
                    if (moveResult == 2)
                    {
                        SeenPoints[newPoint] = 1;
                        TargetPoint = new Point { X = newPoint.X, Y = newPoint.Y };
                    }
                }
            }
            newPoint = new Point { X = p.X, Y = p.Y + 1 };
            if (SeenPoints.ContainsKey(newPoint) == false)
            {
                var moveResult = MoveInDirection(2);
                SeenPoints.Add(newPoint, moveResult);

                if (moveResult != 0)
                {
                    CheckAdjacentPoints(newPoint);
                    MoveInDirection(1);
                    if (moveResult == 2)
                    {
                        SeenPoints[newPoint] = 1;
                        TargetPoint = new Point { X = newPoint.X, Y = newPoint.Y };
                    }
                }
            }
            newPoint = new Point { X = p.X - 1, Y = p.Y };
            if (SeenPoints.ContainsKey(newPoint) == false)
            {
                var moveResult = MoveInDirection(3);
                SeenPoints.Add(newPoint, moveResult);

                if (moveResult != 0)
                {
                    CheckAdjacentPoints(newPoint);
                    MoveInDirection(4);
                    if (moveResult == 2)
                    {
                        SeenPoints[newPoint] = 1;
                        TargetPoint = new Point { X = newPoint.X, Y = newPoint.Y };
                    }
                }
            }

        }
        long MoveInDirection(int inputDirection)
        {
            ic.inputs.Add(inputDirection);
            ic.compute();
            long ret = ic.outputs[0];
            ic.outputs.RemoveAt(0);
            return ret;
        }
        void FloodFill(Point startPoint)
        {
            Stack<Point> pointQueue = new Stack<Point>();

            pointQueue.Push(startPoint);

            while (pointQueue.Count > 0)
            {
                var p = pointQueue.Pop();

                Point[] possiblePoints = new Point[]
                {
                    new Point{X=p.X, Y=p.Y - 1},
                    new Point{X=p.X, Y=p.Y + 1},
                    new Point{X=p.X + 1, Y=p.Y},
                    new Point{X=p.X - 1, Y=p.Y}
                };

                foreach (var point in possiblePoints)
                {
                    if (Board[point.Y, point.X] != 0)
                    {
                        if (Seen[point.Y, point.X] == false)
                        {
                            FloodLvls[point.Y, point.X] = FloodLvls[p.Y, p.X] + 1;
                            pointQueue.Push(point);
                        }
                    }
                }

                Seen[p.Y, p.X] = true;
            }

        }
    }
}
