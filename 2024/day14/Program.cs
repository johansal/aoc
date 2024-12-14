﻿using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        var width = 101; // test=11, p=101
        var height = 103; // test=7, p=103
        var seconds = 100;
        List<int> part1 = [0,0,0,0,0];
        List<((int x, int y) p, (int x, int y) v)> robots = [];

        foreach(var line in input)
        {
            var (p, v) = Parse(line);
            robots.Add((p,v));
            var (x, y) = Simulate(p, v, seconds, width, height);
            //Console.WriteLine($"{x},{y}");
            var q = Quadrant(x, y, width, height);
            //var t = Triangle(x, y, width, height); //
            //Console.WriteLine(t);
            part1[q] = part1[q]+1;
        }
        Console.WriteLine(part1[0] * part1[1] * part1[2] * part1[3]);

        for(seconds = 1; seconds <= 10_000; seconds++) // use this to just render first 10 000 frames to console and hope part2 < 10 000 (it was)
        {
            //while(Console.ReadLine() != "q") // use this to render 1 frame and wait for input for the next frame
            //{
                for(int i = 0; i < robots.Count; i++)
                {
                    var p = Simulate(robots[i].p,robots[i].v,1,width,height); 
                    robots[i] = (p, robots[i].v);
                }
                Render(robots,width,height);
                Console.WriteLine(seconds + "s");
            //}
        }
    }
    private static ((int x, int y) p, (int x, int y) v) Parse(string line)
    {
        var pattern = @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)";
        var m = Regex.Match(line, pattern);
        return ((int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)),
            (int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value)));
    }
    private static (int x, int y) Simulate((int x, int y) p, (int x, int y) v, int s, int w, int h)
    {
        p.x = (p.x + (v.x * s)) % w;
        if (p.x < 0)
            p.x = w + p.x;
        p.y = (p.y + (v.y * s)) % h;
        if (p.y < 0)
            p.y = h + p.y;
        return (p.x, p.y);
    }
    private static int Quadrant(int x, int y, int w, int h)
    {
        var wL = w/2;
        var hL = h/2;
        if (x < wL && y < hL)
        {
            return 0;
        }
        else if (x > wL && y < hL)
        {
            return 1;
        }
        else if (x < wL && y > hL)
        {
            return 2;
        }
        else if (x > wL && y > hL)
        {
            return 3;
        }
        // if robot is on the middle line return 4 as "the fift" quadrant
        return 4;
    }
    private static bool Triangle(int x, int y, int w, int h) {
        var wL = w/2;
        var hL = h-x-1;
        return x >= wL-y && x <= wL+y && y >= hL;
    }
    private static void Render(List<((int x, int y) p, (int x, int y) v)> robots, int w, int h)
    {
        Console.Write('\n');
        for (int i = 0; i <= h; i++)
        {
            for (int j = 0; j <= w; j++)
            {
                if(robots.FindIndex(r => r.p.x == j && r.p.y == i) >= 0)
                {
                    Console.Write('*');
                }
                else {
                    Console.Write(' ');
                }
            }
            Console.Write('\n');
        }
    }

}