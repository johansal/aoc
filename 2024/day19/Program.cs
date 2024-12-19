
using System.ComponentModel.DataAnnotations;

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

public class Node
{
    public bool IsTerminal { get; set; }
    public Dictionary<char, Node> Edges {get; private set;}
    public Node()
    {
        Edges = [];
        IsTerminal = false;
    }
}
public class Trie 
{
    private readonly Node Root;
    public Trie()
    {
        Root = new();
    }

    public void Insert(string pattern)
    {
        var n = Root;
        foreach(char c in pattern)
        {
            if(n.Edges.ContainsKey(c) == false)
            {
                n.Edges[c] = new();
            }
            n = n.Edges[c];
        }
        n.IsTerminal = true;
    }
    //use memoization instead of recursive search
    public bool CanBeConstructed(string design)
    {
        int len = design.Length;
        bool[] memo = new bool[len + 1];
        memo[len] = true;

        for(int i = len - 1; i >= 0; i--)
        {
            var n = Root;
            for(int j = i; j < len; j++) 
            {
                var c = design[j];
                if(n.Edges.ContainsKey(c) == false)
                    break;
                n = n.Edges[c];
                if(n.IsTerminal && memo[j + 1])
                {
                    memo[i] = true;
                    break;
                }
            }
        }
        return memo[0];
    }
    public long CountPatterns(string design)
    {
        var len = design.Length;
        var matches = new long[len+1];
        matches[len] = 1;
        for(int i = len - 1; i >= 0; i--)
        {
            var n = Root;
            for(int j = i; j < len; j++)
            {
                var c = design[j];
                if(n.Edges.ContainsKey(c) == false)
                {
                    break;
                }
                n = n.Edges[c];
                if(n.IsTerminal)
                    matches[i] += matches[j+1];
            }
        }
        return matches[0];
    }
    // works for part 1, too slow for part2
    /*
    public bool CanBeConstructed(string design)
    {
        return PartialMatch(design, 0);
    }
    private bool PartialMatch(string design, int index) 
    {
        var n = Root;
        for(int i = index; i < design.Length; i++)
        {
            var c = design[i];
            if(n.Edges.ContainsKey(c) == false)
                return false;
            n = n.Edges[c];
            if(n.IsTerminal)
            {
                // pattern ended check if also design ended or
                // rest of the design can be matched to another pattern
                if(i == design.Length - i || PartialMatch(design, i + 1))
                {
                    return true;
                }
            }
        }
        return false;
    }
    */
}