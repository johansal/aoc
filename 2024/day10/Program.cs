internal class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");
        int part1 = 0;
        int part2 = 0;
        for (int i = 0; i < input.Length; i++) 
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '0')
                {
                    //Console.WriteLine($"Trailhead at {i} {j}");
                    part1 += Score(input, (i,j)).Count;
                    part2 += Rating(input, (i,j));
                }
            }
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    private static Dictionary<(int,int), bool> Score(string[] map, (int i, int j) p)
    {
        Dictionary<(int,int), bool> ends = [];
        if(map[p.i][p.j] == '9')
        {
            ends[p] = true;
            return ends;
        }
        else {
            foreach (var n in Next(map, p))
            {
                foreach(var end in Score(map, n).Keys)
                {
                    ends[end] = true;
                }
            }
            return ends;
        }
    }
    private static int Rating(string[] map, (int i, int j) p)
    {
        if(map[p.i][p.j] == '9')
        {
            return 1;
        }
        else {
            int rating = 0;
            foreach (var n in Next(map, p))
            {
                rating += Rating(map, n);
            }
            return rating;
        }
    }
    private static List<(int i, int j)> Next(string[] map, (int i, int j) p)
    {
        List<(int i, int j)> result = [];
        if(p.i > 0 )
        {
            var diff = map[p.i-1][p.j] - map[p.i][p.j];
            if(diff == 1)
            {
                result.Add((p.i-1, p.j));
            }
        }
        if(p.j > 0 )
        {
            var diff = map[p.i][p.j-1] - map[p.i][p.j];
            if(diff == 1)
            {
                result.Add((p.i, p.j-1));
            }
        }
        if(p.i < map.Length-1)
        {
            var diff = map[p.i+1][p.j] - map[p.i][p.j];
            if(diff == 1)
            {
                result.Add((p.i+1, p.j));
            }
        }
        if(p.j < map[0].Length-1)
        {
            var diff = map[p.i][p.j+1] - map[p.i][p.j];
            if(diff == 1)
            {
                result.Add((p.i, p.j+1));
            }
        }
        return result;
    }
}