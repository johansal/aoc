static double CartesianDistance(string pointStr1, string pointStr2)
{
    var point1 = pointStr1.Split(',');
    var point2 = pointStr2.Split(',');
    return Math.Sqrt(
        Math.Pow(int.Parse(point1[0]) - int.Parse(point2[0]), 2) + 
        Math.Pow(int.Parse(point1[1]) - int.Parse(point2[1]), 2) + 
        Math.Pow(int.Parse(point1[2]) - int.Parse(point2[2]), 2));   
}

var input = File.ReadAllLines("input");
List<List<string>> circuits = [];
List<(string point1, string point2, double distance)> distances = [];
// loop all line pairs and calculate distances
for (int i = 0; i < input.Length; i++)
{
    var line = input[i];
    for (int j = i + 1; j < input.Length; j++)
    {
        var line2 = input[j];
        distances.Add((line, line2, CartesianDistance(line, line2)));
    }
    // Init circuits
    circuits.Add([line]);
}

// join circuits starting from smallest distance pairs untill all are joined
distances = [.. distances.OrderBy(t => t.distance)];
int counter = 0;
int treshold = 1000; // 10 for test, 1000 for real
foreach (var (point1, point2, distance) in distances)
{
    counter++;
    if (circuits.Any(c => c.Contains(point1) && c.Contains(point2)))
    {
        continue;
    }
    else
    {
        var circuit1 = circuits.First(c => c.Contains(point1));
        var circuit2 = circuits.First(c => c.Contains(point2));
        circuit1.AddRange(circuit2);
        circuits.Remove(circuit2);
    }

    if (counter == treshold)
    {
        // after joining 1000 pairs, output the product of the sizes of the three largest circuits
        Console.WriteLine(circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1, (acc, c) => acc * c.Count));
    }
    if (circuits.Count == 1)
    {
        // after joining all pairs, output the product of the first coordinates of the last joined pair
        Console.WriteLine(long.Parse(point1.Split(',')[0]) * long.Parse(point2.Split(',')[0]));
    }
}

