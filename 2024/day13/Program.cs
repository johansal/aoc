internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadLines("input");
        (int x, int y) buttonA = (0,0);
        (int x, int y) buttonB = (0,0);
        (int x, int y) prize = (0,0);
        int part1 = 0;
        foreach (var line in input)
        {
            if(string.IsNullOrEmpty(line))
            {
                // Do nothing.
            }
            else if (line.StartsWith("Button")) {             
                var spl = line.Split(" ");
                var x = Parse(spl[2]);
                var y = Parse(spl[3]);
                if (spl[1] == "A:")
                {
                    buttonA = (x,y);
                }
                else if (spl[1] == "B:"){
                    buttonB = (x,y);
                }
                else {
                    throw new Exception("Parsing error!");
                }
            }
            else {
                var spl = line.Split(" ");
                var x = Parse(spl[1]);
                var y = Parse(spl[2]);
                prize = (x,y);

                // Solve
                int cheapest = Solve(buttonA, buttonB, prize);
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
    private static int Solve((int x, int y) a, (int x, int y) b, (int x, int y) prize)
    {
        //Console.WriteLine($"Solving: A {a.x},{a.y}; B {b.x},{b.y}; Prize {prize.x},{prize.y}");
        int cheapest = int.MaxValue;
        for (int i = 1; i <= 100; i++)
        {
            for (int j = 1; j <= 100; j++)
            {
                if((a.x * i) + (b.x * j) == prize.x &&
                (a.y * i) + (b.y * j) == prize.y)
                {
                    int value = i * 3 + j;
                    if(value < cheapest)
                        cheapest = value;
                }
            }
        }
        return cheapest;
    }
}