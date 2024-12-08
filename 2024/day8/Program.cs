internal class Program
{
    private static void Main(string[] args)
    {
        var map = File.ReadAllLines("input");
        Dictionary<char,List<(int x, int y)>> antennas = [];

        for(int i = 0; i < map.Length; i++)
        {
            for(int j = 0; j < map[0].Length; j++)
            {
                if(map[i][j] != '.')
                {
                    if(antennas.TryGetValue(map[i][j], out var antenna))
                    {
                        antenna.Add((i,j));
                    }
                    else {
                        antennas[map[i][j]] = [(i,j)];
                    }
                    
                }
            }
        }

        HashSet<(int x, int y)> antinodes = [];
        foreach(var positions in antennas.Values)
        {
            for(int n = 0; n < positions.Count-1; n++)
            {
                for(int m = n+1; m < positions.Count; m++)
                {
                    var dx = positions[m].x - positions[n].x;
                    var dy = positions[m].y - positions[n].y;
                    
                    (int x, int y) antinode1 = (positions[n].x-dx,positions[n].y-dy);                 
                    if(antinode1.x >= 0 && antinode1.x < map.Length && 
                        antinode1.y >= 0 && antinode1.y < map[0].Length)
                    {
                        antinodes.Add(antinode1);
                    }
                    
                    (int x, int y) antinode2 = (positions[m].x+dx,positions[m].y+dy);
                    if(antinode2.x >= 0 && antinode2.x < map.Length && 
                        antinode2.y >= 0 && antinode2.y < map[0].Length)
                    {
                        antinodes.Add(antinode2);
                    }
                }
            }
        }
        Console.WriteLine(antinodes.Count);

        antinodes = [];
        foreach(var positions in antennas.Values)
        {
            for(int n = 0; n < positions.Count-1; n++)
            {
                for(int m = n+1; m < positions.Count; m++)
                {
                    var dx = positions[m].x - positions[n].x;
                    var dy = positions[m].y - positions[n].y;
                    
                    (int x, int y) antinode1 = (positions[n].x,positions[n].y);                 
                    while(antinode1.x >= 0 && antinode1.x < map.Length && 
                        antinode1.y >= 0 && antinode1.y < map[0].Length)
                    {      
                        antinodes.Add(antinode1);
                            //Console.WriteLine("Anti node 1: " + antinode1.x + ", " + antinode1.y);
                        antinode1 = (antinode1.x-dx,antinode1.y-dy);
                    }
                    
                    (int x, int y) antinode2 = (positions[m].x,positions[m].y);
                    while(antinode2.x >= 0 && antinode2.x < map.Length && 
                        antinode2.y >= 0 && antinode2.y < map[0].Length)
                    {
                        antinodes.Add(antinode2);
                            //Console.WriteLine("Anti node 2: " + antinode2.x + ", " + antinode2.y);
                        antinode2 = (antinode2.x+dx,antinode2.y+dy);
                    }
                }
            }
        }
        Console.WriteLine(antinodes.Count);
    }
}