internal class Program
{
    private static void Main(string[] args)
    {
        var map = File.ReadAllLines("input");
        var regions = new HashSet<(int i, int j)>();

        var part1 = 0;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if(regions.Contains((i,j)) == false)
                {
                    var (region, cost) = FenceCost(map, (i,j));
                    regions.UnionWith(region);
                    part1 += cost;
                }
            }
        }
        Console.WriteLine(part1);
    }
    private static (HashSet<(int i, int j)> region, int cost) FenceCost(string[] map, (int i, int j) regionStart)
    {
        var r = Next(map, [], 0, regionStart);
        var price = r.region.Count * r.perimeter;
        //Console.WriteLine($"region: {map[regionStart.i][regionStart.j]}, area: {r.region.Count}, perimeter: {r.perimeter}");
        return (r.region, price);
    }
    private static (HashSet<(int i, int j)> region, int perimeter) Next(string[] map, HashSet<(int i, int j)> region, int perimeter, (int i, int j) p)
    {
        if(region.Contains(p) == false)
        {
            region.Add(p);

            if(p.i > 0 && map[p.i-1][p.j] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, (p.i-1, p.j));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
            }
            else {
                perimeter++;
            }
            if(p.j > 0 && map[p.i][p.j-1] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, (p.i, p.j-1));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
            }
            else {
                perimeter++;
            }
            if(p.i < map.Length-1 && map[p.i+1][p.j] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, (p.i+1, p.j));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
            }
            else {
                perimeter++;
            }
            if(p.j < map[0].Length-1 && map[p.i][p.j+1] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, (p.i, p.j+1));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
            }
            else {
                perimeter++;
            }
        }
        return (region, perimeter);
    }
}