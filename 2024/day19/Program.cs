using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");
        var patterns = input[0].Split(", ");
        var part1 = 0;

        for(int i = 2; i < input.Length; i++)
        {
            var design = input[i];
            if(Match(design, patterns))
            {
                //Console.WriteLine(design);
                part1++;
            }
        }
        Console.WriteLine(part1);
    }
    private static bool Match(string design, string[] patterns)
    {
        var partialMatches = PartialMatch(design, 0, patterns);
        while(partialMatches.Where(m => m < design.Length).Any())
        {
            List<int> m = partialMatches.Where(m => m == design.Length).ToList();
            //part 1, no need to find all
            if(m.Any())
            {
                return true;
            }
            foreach (var item in partialMatches.Where(m => m < design.Length))
            {
                m = m.Union(PartialMatch(design, item, patterns)).ToList();
            }
            partialMatches = m;
        }
        return partialMatches.Where(m => m == design.Length).Any();
    }
    private static List<int> PartialMatch(string design, int position, string[] patterns) {
        var part = design.Substring(position);
        List<int> possible = [];
        //Console.WriteLine($"cheking: {part}");
        for(int i = 0; i < patterns.Length; i++)
        {
            if(part.Length >= patterns[i].Length && part.StartsWith(patterns[i]))
                possible.Add(position + patterns[i].Length);
        }
        return possible;
    }
}