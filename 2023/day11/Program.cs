var input = File.ReadAllLines("inputs/input");
List<(int x, int y)> Galaxies = [];
List<int> GalaxyRow = [];
List<int> GalaxyColumn = [];

for(int i = 0; i < input.Length; i++)
{
    for(int j = 0; j < input[0].Length; j++)
    {
        if(input[i][j] == '#')
        {
            Galaxies.Add((i,j));
            if(GalaxyRow.Contains(i) == false) 
            {
                GalaxyRow.Add(i);
            }
            if(GalaxyColumn.Contains(j) == false) 
            {
                GalaxyColumn.Add(j);
            }
        }
    }
}

//Calculate manhattan distance between galaxy pairs
var part1 = 0;
long part2 = 0;
for(int i = 0; i < Galaxies.Count; i++)
{
    for(int j = i + 1; j < Galaxies.Count; j++)
    {
        var yStart = Galaxies[j].y < Galaxies[i].y ?  Galaxies[j].y : Galaxies[i].y;
        var distance = Galaxies[j].y - Galaxies[i].y;
        if(distance < 0)
            distance *= -1;
        //check columns in between for expansion
        var expansions = 0;
        for(int e = yStart + 1; e < yStart + distance; e++) {
            if(GalaxyColumn.Contains(e) == false)
                expansions++;
        }
        for(int e = Galaxies[i].x + 1; e < Galaxies[j].x; e++) {
            if(GalaxyRow.Contains(e) == false)
                expansions++;
        }
        distance += Galaxies[j].x - Galaxies[i].x;
        //Console.WriteLine($"Galaxies {i+1} & {j+1}: distance {distance} (with expansions {distance+expansions})");
        part1 += distance + expansions;
        part2 += distance - expansions + (expansions*1000000);
    }
}
Console.WriteLine($"Part1: {part1}, part2: {part2}");