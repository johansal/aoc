internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        int part1 = 0;
        int part2 = 0;
        foreach(var line in input)
        {
            var tmp = line.Split(' ');
            List<int> parsedLine = [];
            foreach(var item in tmp)
            {
                parsedLine.Add(int.Parse(item));
            }
            if(ParseLevels(parsedLine))
            {
                part1++;
                part2++;
            }
            // Check if report is safe if we remove one level from it
            else {
                for(int i = 0; i < parsedLine.Count; i++)
                {
                    var parsed2 = parsedLine.ToList();
                    parsed2.RemoveAt(i);
                    if(ParseLevels(parsed2))
                    {
                        part2++;
                        break;
                    }
                }
            }
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    private static bool ParseLevels(List<int> line)
    {
        bool isSafe = true;
        int i = 1;
        int? lastDiff = null;
        while(isSafe && i < line.Count)
        {
            var diff = line[i-1] - line[i];
            isSafe = diff != 0 && diff <= 3 && diff >= -3 && (lastDiff == null || (lastDiff < 0 == diff < 0));
            lastDiff = diff;
            i++;
        }
        return isSafe;
    }
}