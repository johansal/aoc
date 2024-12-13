public class Program
{
    private static void Main(string[] args)
    {
        const long k = 10000000000000;
        var input = File.ReadLines("input");
        (int x, int y) a = (0,0);
        (int x, int y) b = (0,0);
        long part1 = 0;
        long part2 = 0;
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
                part1 += Solve(a, b, (x,y));
                part2 += Solve(a, b, (k+x,k+y));
            }
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    private static int Parse(string value)
    {
        if(value.EndsWith(','))
            return int.Parse(value[2..^1]);
        return int.Parse(value[2..]);
    }
    private static long Solve((int i, int j) a, (int i, int j) b, (long i, long j) p)
    {
        // a.i * x + b.i * y = p.i
        // a.j * x b.j * y = p.j

        // A = [[a.i,b.i], B = [[p.i], X = [[x],
        //      [a.j,b,j]]      [p.j]]      [y]]
        // A X = B <=> X = A- B
        
        int detA = (a.i * b.j) - (b.i * a.j);
        /*int[][] adjA = [[b.j, -1 * b.i],
                          [-1 * a.j, a.i]];*/
        // Käänteismatriisi A- = 1/detA * adjA        
        long x = ((b.j * p.i) / detA) + ((-1 * b.i * p.j) / detA);
        long y = ((-1 * a.j * p.i) / detA) + ((a.i * p.j) / detA);
        if(((a.i * x) + (b.i * y) == p.i) && ((a.j * x) + (b.j * y) == p.j))
        {
            return  3 * x + y;
        }
        else {
            return 0;
        }
    }
}