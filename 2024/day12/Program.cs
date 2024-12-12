internal class Program
{
    private static void Main(string[] args)
    {
        var map = File.ReadAllLines("input");
        var regions = new HashSet<(int i, int j)>();

        var part1 = 0;
        var part2 = 0;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if(regions.Contains((i,j)) == false)
                {
                    var (region, cost, discountCost) = FenceCost(map, (i,j));
                    regions.UnionWith(region);
                    part1 += cost;
                    part2 += discountCost;
                }
            }
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    private static (HashSet<(int i, int j)> region, int cost, int discountCost) FenceCost(string[] map, (int i, int j) regionStart)
    {
        var (region, perimeter, sides) = Next(map, [], 0, 0, regionStart);
        var price = region.Count * perimeter;
        var discountCost = region.Count * sides;
        //Console.WriteLine($"region: {map[regionStart.i][regionStart.j]}, area: {region.Count}, sides: {sides}");
        return (region, price, discountCost);
    }
    private static (HashSet<(int i, int j)> region, int perimeter, int sides) Next(string[] map, HashSet<(int i, int j)> region, int perimeter, int sides, (int i, int j) p)
    {
        if(region.Contains(p) == false)
        {
            region.Add(p);
            sides += Corners(map, p);

            if(p.i > 0 && map[p.i-1][p.j] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, sides, (p.i-1, p.j));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
                sides = n.sides;
            }
            else {
                perimeter++;
            }
            if(p.j > 0 && map[p.i][p.j-1] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, sides, (p.i, p.j-1));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
                sides = n.sides;
            }
            else {
                perimeter++;
            }
            if(p.i < map.Length-1 && map[p.i+1][p.j] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, sides, (p.i+1, p.j));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
                sides = n.sides;
            }
            else {
                perimeter++;
            }
            if(p.j < map[0].Length-1 && map[p.i][p.j+1] - map[p.i][p.j] == 0)
            {
                var n = Next(map, region, perimeter, sides, (p.i, p.j+1));
                region.UnionWith(n.region);
                perimeter = n.perimeter;
                sides = n.sides;
            }
            else {
                perimeter++;
            }
        }
        return (region, perimeter, sides);
    }
    private static int Corners(string[] map, (int i, int j) p)
    {
        List<(int i, int j)> n = [];
        if(p.i > 0 && map[p.i-1][p.j] - map[p.i][p.j] == 0)
        {
            n.Add((p.i-1,p.j));
        }

        if(p.j > 0 && map[p.i][p.j-1] - map[p.i][p.j] == 0)
        {
            n.Add((p.i,p.j-1));
        }

        if(p.i < map.Length-1 && map[p.i+1][p.j] - map[p.i][p.j] == 0)
        {
            n.Add((p.i+1,p.j));
        }

        if(p.j < map[0].Length-1 && map[p.i][p.j+1] - map[p.i][p.j] == 0)
        {
            n.Add((p.i,p.j+1));
        }

        if (n.Count == 0)
            return 4;
        else if(n.Count == 1) {
            return 2;
        }
        else if(n.Count == 2) {
            if(n[0].i == n[1].i || n[0].j == n[1].j)
                return 0;
            else {
                if(n.Contains((p.i-1,p.j)))
                {
                    if(n.Contains((p.i,p.j-1)))
                    {
                        return map[p.i-1][p.j-1] == map[p.i][p.j] ? 1 : 2;
                    }
                    else {
                        return map[p.i-1][p.j+1] == map[p.i][p.j] ? 1 : 2;
                    }
                }
                else {
                    if(n.Contains((p.i,p.j-1)))
                    {
                        return map[p.i+1][p.j-1] == map[p.i][p.j] ? 1 : 2;
                    }
                    else {
                        return map[p.i+1][p.j+1] == map[p.i][p.j] ? 1 : 2;
                    }
                }
            }
        }
        else if(n.Count == 3) {
            if(!n.Contains((p.i-1,p.j)))
            {
                int corners = 0;
                corners += map[p.i+1][p.j-1] == map[p.i][p.j] ? 0 : 1;
                corners += map[p.i+1][p.j+1] == map[p.i][p.j] ? 0 : 1;
                return corners;
            }
            else if(!n.Contains((p.i,p.j+1)))
            {
                int corners = 0;
                corners += map[p.i-1][p.j-1] == map[p.i][p.j] ? 0 : 1;
                corners += map[p.i+1][p.j-1] == map[p.i][p.j] ? 0 : 1;
                return corners;
            }
            else if(!n.Contains((p.i+1,p.j)))
            {
                int corners = 0;
                corners += map[p.i-1][p.j-1] == map[p.i][p.j] ? 0 : 1;
                corners += map[p.i-1][p.j+1] == map[p.i][p.j] ? 0 : 1;
                return corners;
            }
            else
            {
                int corners = 0;
                corners += map[p.i-1][p.j+1] == map[p.i][p.j] ? 0 : 1;
                corners += map[p.i+1][p.j+1] == map[p.i][p.j] ? 0 : 1;
                return corners;
            }
        }
        else if(n.Count == 4) {
            int corners = 0;
            corners += map[p.i-1][p.j-1] == map[p.i][p.j] ? 0 : 1;
            corners += map[p.i-1][p.j+1] == map[p.i][p.j] ? 0 : 1;
            corners += map[p.i+1][p.j-1] == map[p.i][p.j] ? 0 : 1;
            corners += map[p.i+1][p.j+1] == map[p.i][p.j] ? 0 : 1;
            return corners;
        }
        else {
            throw new Exception("Incorrect amount of neighbours found!");
        }
    }
}