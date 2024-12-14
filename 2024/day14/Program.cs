using System.Text.RegularExpressions;
using System.Drawing;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
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
            var q = Quadrant(x, y, width, height);
            part1[q] = part1[q]+1;
        }
        Console.WriteLine(part1[0] * part1[1] * part1[2] * part1[3]);

        // Calculate standard deviation for x and y for all robots
        var baseline = Std(robots.Select(r => r.p).ToList());
        seconds = 0;
        while (true)
        {
            // Simulate 1s
            for(int i = 0; i < robots.Count; i++)
            {
                var p = Simulate(robots[i].p,robots[i].v,1,width,height); 
                robots[i] = (p, robots[i].v);
            }
            seconds++;
            // Calculate standard deviation for new positions
            var (x, y) = Std(robots.Select(r => r.p).ToList());
            // If the new std is lower than the baseline, render the picture
            if(x < baseline.x - 3 && y < baseline.y - 3)
            {
                Console.WriteLine(seconds);
                Render(robots,width,height, $"./renders/{seconds}s.png");
                break;
            }
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
    private static (double x, double y) Std(List<(int x, int y)> positions) {
        double meanX = positions.Sum(p => p.x)/positions.Count;
        double sqrtDevsX = 0;
        double meanY = positions.Sum(p => p.y)/positions.Count;
        double sqrtDevsY = 0;
        foreach (var (x, y) in positions)
        {
            var deviation = x - meanX;
            sqrtDevsX += Math.Pow(deviation,2);
            deviation = y - meanY;
            sqrtDevsY += Math.Pow(deviation,2);
        }
        return (Math.Sqrt(sqrtDevsX/positions.Count),Math.Sqrt(sqrtDevsY/positions.Count));
    }
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private static void Render(List<((int x, int y) p, (int x, int y) v)> robots, int w, int h, string filePath)
    {
        var b = new Bitmap(w, h);
        foreach (var (p, _) in robots)
        {
            b.SetPixel(p.x, p.y, Color.White);
        }
        b.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        b.Dispose();
    }

}