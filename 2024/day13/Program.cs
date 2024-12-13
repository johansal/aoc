internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadLines("input");
        (int x, int y) a = (0,0);
        (int x, int y) b = (0,0);
        int part1 = 0;
        foreach (var line in input)
        {
            if (line.StartsWith("Button")) {             
                var spl = line.Split(" ");
                var x = Parse(spl[2]);
                var y = Parse(spl[3]);
                if (spl[1] == "A:")
                {
                    a = (x,y);
                }
                else if (spl[1] == "B:"){
                    b = (x,y);
                }
            }
            else if (line.StartsWith("Prize")) {
                var spl = line.Split(" ");
                var x = Parse(spl[1]);
                var y = Parse(spl[2]);
                // Solve
                int cheapest = Solve(a, b, (x,y));
                if (cheapest != int.MaxValue)
                {
                    part1 += cheapest;
                }
            }
        }
        Console.WriteLine(part1);
    }
    private static int Parse(string value)
    {
        if(value.EndsWith(','))
            return int.Parse(value[2..^1]);
        return int.Parse(value[2..]);
    }
    private static int Solve((int i, int j) a, (int i, int j) b, (int i, int j) p)
    {
        int min = int.MaxValue;
        for (int x = 1; x <= 100; x++)
        {
            if((a.j * x) + (b.j * ((p.i - a.i * x)/b.i)) == p.j)
            {
                int value = x * 3 + ((p.i - (a.i * x)) / b.i);
                if(value < min)
                    min = value;
            }
        }
        return min;
    }
}