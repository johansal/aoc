using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Day17
{
    public static class Program
    {
        static void Main(string[] args)
        {
            int[] targetArea = ParseTarget(File.ReadAllText("input.txt"));
            List<(int,int)> initialValues = new();

            //simulate the trajectory with all possible velocities
            //start by shooting straight at the boottom right corner of the target area (Vx max = x2 & Vy min = y1)
            //Vx min is reducing sum x1*(x1+1)/2 ~= sqrt(x1*2) where x1 is target area start
            //so that the projectile can fly at least to the target area
            var VxMin = (int)Math.Sqrt(targetArea[0]*2);
            //Vy max is (-Vy min - 1), since projectile comes back to y=0 with opposite of starting velocity
            //so any Vy greater than -Vy min will make the projectile fly past the target area
            var VyMax = (-1*targetArea[2])-1;
            var maxHeight = targetArea[3];

            for (var x = targetArea[1]; x > VxMin; x--)
            {
                for (var y = targetArea[2]; y <= VyMax; y++)
                {
                    Projectile p = new()
                    {
                        X = 0,
                        Y = 0,
                        Vx = x,
                        Vy = y
                    };
                    var pMaxHeight = p.Y;
                    var steps = 0;
                    while (!Miss(p, targetArea))
                    {
                        p.Fly();
                        steps++;
                        if (p.Y > pMaxHeight) pMaxHeight = p.Y;
                        if (Hit(p, targetArea))
                        {
                            initialValues.Add((x,y));
                            //Console.WriteLine(x + " velocity forward and " + y + " upward hit the target!");
                            if (pMaxHeight > maxHeight) maxHeight = pMaxHeight;
                        }
                    }
                }
            }
            Console.WriteLine(maxHeight);
            Console.WriteLine(initialValues.Distinct().Count()); // some shots may hit with multiple steps
        }
        public static int[] ParseTarget(string input)
        {
            int[] targetArea = new int[4];
            var numbers = Regex.Matches(input, @"-?\d+");
            for (var i = 0; i < 4; i++)
            {
                targetArea[i] = int.Parse(numbers[i].ToString());
            }
            return targetArea;
        }
        public static bool Hit(Projectile p, int[] target)
        {
            return p.X >= target[0] && p.X <= target[1] && p.Y >= target[2] && p.Y <= target[3];
        }
        public static bool Miss(Projectile p, int[] target)
        {
            return p.X > target[1] || p.Y < target[2];
        }
    }
    public class Projectile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Vx { get; set; }
        public int Vy { get; set; }

        public void Fly()
        {
            X += Vx;
            Y += Vy;
            //Drag
            if (Vx > 0)
            {
                Vx--;
            }
            else if (Vx < 0)
            {
                Vx++;
            }
            //Gravity
            Vy--;
        }
    }
}
