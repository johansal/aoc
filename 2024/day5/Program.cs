internal class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");
        Dictionary<int, List<int>> rules = [];
        bool readingRules = true;
        int part1 = 0;
        int part2 = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                readingRules = false;
            }
            else if (readingRules)
            {
                var rule = line.Split('|');
                var f = int.Parse(rule[0]);
                var s = int.Parse(rule[1]);
                if (rules.TryGetValue(f, out var ruleList))
                {
                    ruleList.Add(s);
                }
                else {
                    rules[f] = [s];
                }
                // Make sure all pages are in the dictionary
                if (!rules.ContainsKey(s))
                {
                    rules[s] = [];
                }
            }
            else
            {
                var pages = line.Split(',').Select(int.Parse).ToList();
                if(Validate(pages, rules))
                {
                    // add middle page number to part1 for all valid rows
                    part1 += pages[pages.Count/2];
                }
                else {
                    // fix order of invalid rows, add middle page number to part2
                    var ordered = Order(pages, rules);
                    part2 += ordered[pages.Count/2];
                }
            }
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    private static bool Validate(List<int> pages, Dictionary<int, List<int>> rules)
    {
        if (pages[1..].Any(x => rules[x].Contains(pages[0])))
        {
            return false;
        }
        else if(pages.Count > 1)
        {
            return Validate(pages[1..], rules);
        }
        else {
            return true;
        }
    }
    private static List<int> Order(List<int> pages, Dictionary<int, List<int>> rules)
    {
        List<int> ordered = [];
        foreach (int page in pages)
        {
            bool added = false;
            for (int i = 0; i < ordered.Count; i++)
            {
                if (rules[page].Contains(ordered[i]))
                {
                    ordered.Insert(i, page);
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                ordered.Add(page);
            }
        }
        return ordered;
    }
}