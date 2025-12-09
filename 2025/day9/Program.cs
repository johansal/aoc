using Day9;

var input = File.ReadAllLines("input");
List<(int x, int y)> points = [];
foreach (var line in input)
{
    var parts = line.Split(',');
    var point = (x: int.Parse(parts[1]), y: int.Parse(parts[0]));
    points.Add(point);
}

long maxArea = GeometryHelper.FindLargestRectangle(points);
long part2maxArea = GeometryHelper.FindLargestRectangleInsidePolygon(points);

Console.WriteLine(maxArea);
Console.WriteLine(part2maxArea);


