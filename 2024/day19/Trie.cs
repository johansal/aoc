namespace day19;
public class Trie 
{
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
}