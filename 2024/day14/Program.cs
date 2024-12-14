using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        var width = 101; // test=11, p=101
        var height = 103; // test=7, p=103
        List<int> part1 = [0,0,0,0,0];

        foreach(var line in input)
        {
            var r = Parse(line);
            var (p, v) = Simulate(r.p, r.v, 100, width, height);
            //Console.WriteLine($"x={robot.p.x}, y={robot.p.y}");
            var q = FindQuadrant(p.x, p.y, width, height);
            //Console.WriteLine(q);
            part1[q] = part1[q]+1;
        }
        Console.WriteLine(part1[0] * part1[1] * part1[2] * part1[3]);
    }
    private static ((int x, int y) p, (int x, int y) v) Parse(string line)
    {
        var pattern = @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)";
        var m = Regex.Match(line, pattern);
        //Console.WriteLine($"Found: {m.Groups[1].Value} ; {m.Groups[2].Value} ; {m.Groups[3].Value} ; {m.Groups[4].Value}");
        return (
            (
                int.Parse(m.Groups[1].Value),
                int.Parse(m.Groups[2].Value)),
            (
                int.Parse(m.Groups[3].Value),
                int.Parse(m.Groups[4].Value)
            )
        );
    }
    private static ((int x, int y) p, (int x, int y) v) Simulate((int x, int y) p, (int x, int y) v, int s, int w, int h)
    {
        p.x = (p.x + (v.x * s)) % w;
        if (p.x < 0)
            p.x = w + p.x;
        p.y = (p.y + (v.y * s)) % h;
        if (p.y < 0)
            p.y = h + p.y;
        return (p,v);
    }
    private static int FindQuadrant(int x, int y, int w, int h)
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
}