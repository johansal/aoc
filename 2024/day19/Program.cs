namespace day19;
public class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");
        Trie trie = new();

        foreach(var pattern in input[0].Split(", "))
        {
            trie.Insert(pattern);
        }

        int part1 = 0;
        long part2 = 0;
        for(int i = 2; i < input.Length; i++)
        {
            Console.WriteLine($"checking {i}: {input[i]}");
            //var count = PartialMatch(input[i], 0, input[0].Split(", "));
            if(trie.CanBeConstructed(input[i]))
            {
                part1++;
            }
            var count = trie.CountPatterns(input[i]);
            part2 += count;
            Console.WriteLine(count);
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    // works for part 1, too slow for part2
    private static int PartialMatch(string design, int position, string[] patterns) {
        var part = design[position..];
        int exact = 0;
        for(int i = 0; i < patterns.Length; i++)
        {
            if(part.StartsWith(patterns[i]))
            {
                if(part.Length == patterns[i].Length)
                    exact++;
                else if(part.Length > patterns[i].Length)
                {
                    exact += PartialMatch(design, position + patterns[i].Length, patterns);
                }
            }
        }
        return exact;
    }
}