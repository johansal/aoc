static int CountNeighbors(char[][] grid, int x, int y, char target)
{
    int count = 0;
    int[] dx = [-1, 0, 1, -1, 1, -1, 0, 1];
    int[] dy = [-1, -1, -1, 0, 0, 1, 1, 1];
    for (int i = 0; i < dx.Length; i++)
    {
        var neighborX = x + dx[i];
        var neighborY = y + dy[i];
        if(neighborX >= 0 && neighborX < grid[0].Length && 
        neighborY >= 0 && neighborY < grid.Length && 
        grid[neighborY][neighborX] == target)
        {
            count++;
        }
    }
    return count;
}

var lines = File.ReadAllLines("input");
var input = lines.Select(l => l.ToCharArray()).ToArray();

// loop grid to get roll positions
List<(int x, int y)> rolls = [];
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[y].Length; x++)
    {
        if (input[y][x] == '@')
        {
            rolls.Add((x, y));
        }
    }   
}

int rmCount = 0;
var part1 = 0;
var part2 = 0;
do {
    // find removable rolls
    List<(int x, int y)> removable = [];
    foreach(var (x, y) in rolls)
    {
        if (input[y][x] == '@')
        {
            var count = CountNeighbors(input, x, y, '@');
            if(count < 4)
            {
                removable.Add((x, y));
            }
        }
    }
    // first iteration set part1
    rmCount = removable.Count;
    if(part1 == 0)
    {
        part1 = rmCount;
    }
    part2 += rmCount;
    // remove found removable rolls
    foreach(var (x, y) in removable)
    {
        input[y][x] = '.';
    }
} while(rmCount > 0);

Console.WriteLine(part1);
Console.WriteLine(part2);




