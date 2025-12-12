List<string> RotateShape(List<string> shape)
{
    int rows = shape.Count;
    int cols = shape[0].Length;
    List<string> rotated = [.. new string[cols]];
    for(int c = 0; c < cols; c++)
    {
        char[] newRow = new char[rows];
        for(int r = 0; r < rows; r++)
        {
            newRow[r] = shape[rows - r - 1][c];
        }
        rotated[c] = new string(newRow);
    }
    return rotated;
}
List<string> FlipShape(List<string> shape)
{
    List<string> flipped = [];
    foreach(var line in shape)
    {
        flipped.Add(new string([.. line.Reverse()]));
    }
    return flipped;
}
int TakeNextShape(List<int> shapeCounts)
{
    for(int i = 0; i < shapeCounts.Count; i++)
    {
        if(shapeCounts[i] > 0)
        {
            shapeCounts[i]--;
            return i;
        }
    }
    return -1;
}

bool TryPlaceShape(List<string> region, List<string> shape, int startRow, int startCol)
{
    int shapeRows = shape.Count;
    int shapeCols = shape[0].Length;
    int regionRows = region.Count;
    int regionCols = region[0].Length;

    // Check if shape fits
    for(int r = 0; r < shapeRows; r++)
    {
        for(int c = 0; c < shapeCols; c++)
        {
            if(shape[r][c] == '#')
            {
                int regionR = startRow + r;
                int regionC = startCol + c;
                if(regionR >= regionRows || regionC >= regionCols || region[regionR][regionC] != '.')
                    return false;
            }
        }
    }

    // Place shape
    for(int r = 0; r < shapeRows; r++)
    {
        char[] regionRow = region[startRow + r].ToCharArray();
        for(int c = 0; c < shapeCols; c++)
        {
            if(shape[r][c] == '#')
            {
                regionRow[startCol + c] = '#';
            }
        }
        region[startRow + r] = new string(regionRow);
    }
    return true;
}
//todo: instead of region, create combination of neighborin packages and test their fit, 
bool TestRegion(List<string> region, List<int> shapeCounts, List<List<List<string>>> shapes, Dictionary<string, bool> memo)
{
    // Create cache key from region state and shape counts
    string cacheKey = string.Join("|", region) + ":" + string.Join(",", shapeCounts);
    if(memo.TryGetValue(cacheKey, out bool value))
        return value;
    
    // Base case: all shapes placed
    if(shapeCounts.Sum() == 0)
        return memo[cacheKey] = true;
    
    // Recursive case: try to place the first shape in all possible positions and orientations
    List<int> shapeCountCopy = [.. shapeCounts];

    var shape = shapes[TakeNextShape(shapeCountCopy)];
    for(int r = 0; r < region.Count; r++)
    {
        for(int c = 0; c < region[0].Length; c++)
        {
            foreach(var variant in shape)
            {
                // Try to place shape at (r, c)
                List<string> regionCopy = [.. region];
                if(TryPlaceShape(regionCopy, variant, r, c))
                {
                    if(TestRegion(regionCopy, shapeCountCopy, shapes, memo))
                    {
                        return memo[cacheKey] = true;
                    }
                }
            }
        }
    }

    return memo[cacheKey] = false;
}

var input = File.ReadAllLines("input");

// Parse shapes and regions, shapes is a list of different unique rotations and flips of each shape
List<List<List<string>>> shapes = [];
List<string> currentShape = [];
List<string> regions = [];
var part1 = 0;
foreach (var line in input)
{
    if(shapes.Count < 6)
    {
        if(string.IsNullOrEmpty(line))
        {
            shapes.Add([]);
            // Generate unique rotations and flips
            var rotated = currentShape;
            for(int i = 0; i < 4; i++)
            {
                rotated = RotateShape(rotated);
                if(!shapes[^1].Any(s => s.SequenceEqual(rotated)))
                    shapes[^1].Add(rotated);
                var flipped = FlipShape(rotated);
                if(!shapes[^1].Any(s => s.SequenceEqual(flipped)))
                    shapes[^1].Add(flipped);
            }
        }
        else if (line.Contains(':'))
        {
            currentShape = [];
        }
        else
        {
            currentShape.Add(line);
        }
    }
    else
    {
        // Region line
        var parts = line.Split(':');
        var gridSize = parts[0].Split('x');
        var cols = int.Parse(gridSize[0]);
        var rows = int.Parse(gridSize[1]);
        List<string> region = [.. Enumerable.Repeat(new string('.', cols), rows)];
        
        // Shapes to fit
        List<int> shapeCounts = [.. parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)];
        var memo = new Dictionary<string, bool>();
        if(TestRegion(region, shapeCounts, shapes, memo))
            part1++;
    }
}

Console.WriteLine(part1);