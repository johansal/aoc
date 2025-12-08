Dictionary<(int, int), long> memo = [];

long TimeParticle(string[] map, int row, int col)
{
    if (row == map.Length)
        return 1;
    
    if (memo.TryGetValue((row, col), out long cached))
        return cached;
    
    long result;
    if (map[row][col] == '^')
        result = TimeParticle(map, row + 1, col - 1) + TimeParticle(map, row + 1, col + 1);
    else
        result = TimeParticle(map, row + 1, col);
    
    memo[(row, col)] = result;
    return result;
}

var input = File.ReadAllLines("input");
var splitters = 0;
List<int> beams = [input[0].IndexOf('S')];

for (int i = 1; i < input.Length; i++)
{
    List<int> newBeams = [];
    foreach(var beam in beams)
    {
        if (input[i][beam] == '.' && newBeams.Contains(beam) == false)
        {
            newBeams.Add(beam);
        }
        else if (input[i][beam] == '^')
        {
            splitters++;
            if(newBeams.Contains(beam + 1) == false)
                newBeams.Add(beam + 1);
            if(newBeams.Contains(beam - 1) == false)
                newBeams.Add(beam - 1);
        }
    }
    beams = newBeams;
}
Console.WriteLine(splitters);
Console.WriteLine(TimeParticle(input, 0, input[0].IndexOf('S')));