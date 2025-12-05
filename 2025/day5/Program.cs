var input = File.ReadAllLines("input");
bool readRanges = true;
List<(long start, long end)> ranges = [];
var part1 = 0;

foreach (var line in input)
{
    // read ranges until empty line
    if (string.IsNullOrWhiteSpace(line))
    {
        readRanges = false;
        continue;
    }

    if(readRanges)
    {
        var parts = line.Split('-');
        var start = long.Parse(parts[0]);
        var end = long.Parse(parts[1]);
        // merge ranges if overlapping
        foreach(var (s, e) in ranges.ToList())
        {
            if(!(end < s || start > e))
            {
                start = Math.Min(start, s);
                end = Math.Max(end, e);
                ranges.Remove((s, e));
            }
        }
        ranges.Add((start, end));
    }
    else
    {
        // part 1, check if number is in any range
        var number = long.Parse(line);
        foreach (var (start, end) in ranges)
        {
            if (number >= start && number <= end)
            {
                part1++;
                break;
            }
        }
    }
}
// part 2, count all numbers in ranges
long part2 = 0;
foreach (var (start, end) in ranges)
{
    part2 += end - start + 1;
}

Console.WriteLine(part1);
Console.WriteLine(part2);