internal class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");
        var patterns = input[0].Split(", ");
        var part1 = 0;
        var part2 = 0;

        for(int i = 2; i < input.Length; i++)
        {
            var design = input[i];
            Console.WriteLine($"cheking: {design}");
            var ret = FindAll(design, patterns, 0);
            part1 += ret == 0 ? 0 : 1;
            part2 += ret;
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    private static int FindAll(string design, IEnumerable<string> patterns, int matches) {
        var partial = patterns.Where(pattern => design.Length >= pattern.Length && design.StartsWith(pattern));
        var exact = partial.Where(pattern => design.Length == pattern.Length).Count();
        foreach(var pattern in partial)
        {
            var part = design.Substring(pattern.Length);
            exact = FindAll(part, patterns, exact);
        }
        return exact + matches;
    }
/*
    private static int Match(string design, string[] patterns)
    {
        var partialMatches = PartialMatch(design, patterns);
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
                m = m.Concat(PartialMatch(design, item, patterns)).ToList();
            }
            partialMatches = m;
        }
        return partialMatches.Where(m => m == design.Length).Count();
    }
    private static List<int> PartialMatch(string design, string[] patterns) {
        List<int> possible = [];
        //Console.WriteLine($"cheking: {part}");
        for(int i = 0; i < patterns.Length; i++)
        {
            if(design.Length >= patterns[i].Length && design.StartsWith(patterns[i]))
                possible.Add(i);
        }
        return possible;
    }
*/
}