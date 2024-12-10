internal class Program
{
    private static void Main()
    {
        var map = File.ReadAllLines("input");
        var trailhead = '0';
        var trailend = '9';

        int part1 = 0;
        int part2 = 0;

        for (int i = 0; i < map.Length; i++) 
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                // Find trailheads
                if (map[i][j] == trailhead)
                {
                    // Score = number of unique trailends accessible from trailhead
                    part1 += GetTrailEnds(map, (i,j), trailend).Count;
                    // Rating = number on different routes from trailhead to a trailend
                    part2 += Rating(map, (i,j), trailend);
                }
            }
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    // 
    private static Dictionary<(int,int), bool> GetTrailEnds(string[] map, (int i, int j) p, char trailend)
    {
        Dictionary<(int,int), bool> ends = [];
        if(map[p.i][p.j] == trailend)
        {
            ends[p] = true;
            return ends;
        }
        else {
            foreach (var n in Next(map, p))
            {
                foreach(var end in GetTrailEnds(map, n, trailend).Keys)
                {
                    ends[end] = true;
                }
            }
            return ends;
        }
    }
    private static int Rating(string[] map, (int i, int j) p, char trailend)
    {
        if(map[p.i][p.j] == trailend)
        {
            return 1;
        }
        else {
            int rating = 0;
            foreach (var n in Next(map, p))
            {
                rating += Rating(map, n, trailend);
            }
            return rating;
        }
    }
    // Get list of neighbours for point p
    private static List<(int i, int j)> Next(string[] map, (int i, int j) p)
    {
        List<(int i, int j)> result = [];
        if(p.i > 0 && map[p.i-1][p.j] - map[p.i][p.j] == 1)
        {
            result.Add((p.i-1, p.j));
        }
        if(p.j > 0 && map[p.i][p.j-1] - map[p.i][p.j] == 1)
        {
            result.Add((p.i, p.j-1));
        }
        if(p.i < map.Length-1 && map[p.i+1][p.j] - map[p.i][p.j] == 1)
        {
            result.Add((p.i+1, p.j));
        }
        if(p.j < map[0].Length-1 && map[p.i][p.j+1] - map[p.i][p.j] == 1)
        {
            result.Add((p.i, p.j+1));
        }
        return result;
    }
}