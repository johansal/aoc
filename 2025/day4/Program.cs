static int CountNeighbors(string[] grid, int x, int y, char target)
{
    int count = 0;
    int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
    int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };
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

var input = File.ReadAllLines("test");

// loop grid
var part1 = 0;
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[y].Length; x++)
    {
        if (input[y][x] == '@')
        {
            if(CountNeighbors(input, x, y, '@') < 4)
            {
                part1++;
            }
        }
    }
}
Console.WriteLine(part1);


