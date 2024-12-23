internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        Dictionary<string,List<string>> connections = [];
        ParseConnections(input, connections);

        HashSet<string> groups = [];
        foreach(var c in connections.Keys.Where(key => key.StartsWith('t')))
        {
            var neibor = connections[c];
            foreach(var n in neibor) {
                var neiborN = connections[n];
                foreach(var n2 in neiborN) {
                    if(n2 != c && connections[n2].Contains(c)) {
                        List<string> group = [c, n, n2];
                        groups.Add(string.Join(",",group.Order()));
                    }
                }
            }
        }
        Console.WriteLine(groups.Count);
        var max = BuildMaxCliq(connections);
        Console.WriteLine(string.Join(",",max.Order()));        
    }
    //neat grap algo to find largest set of connected nodes
    private static void BronKerbosch(HashSet<string> R, HashSet<string> P, HashSet<string> X, List<List<string>> cliques, Dictionary<string,List<string>> connections)
    {
        if (P.Count != 0 && X.Count != 0)
        {
            // Found a maximal clique
            cliques.Add([.. R]);
            return;
        }

        var Pcopy = new HashSet<string>(P);
        //Add each v in P to R
        foreach (var v in Pcopy)
        {
            R.Add(v);
            BronKerbosch(
                R,
                [.. P.Intersect(connections[v])], // Neighbors of v still in P
                [.. X.Intersect(connections[v])], // Neighbors of v already processed
                cliques, connections);
            R.Remove(v); // Backtrack
            P.Remove(v); // Exclude v from P
            X.Add(v);    // Add v to X as visited
        }
    }

    // Helper to find the largest clique
    public static List<string> BuildMaxCliq(Dictionary<string,List<string>> connections)
    {
        var cliqs = new List<List<string>>();
        var R = new HashSet<string>();
        var P = new HashSet<string>(connections.Keys); // All vertices are initially candidates
        var X = new HashSet<string>();

        BronKerbosch(R, P, X, cliqs, connections);
        return cliqs.OrderByDescending(clique => clique.Count).First();
    }
    private static void ParseConnections(string[] input, Dictionary<string, List<string>> connections)
    {
        foreach(var line in input)
        {
            var computers = line.Split('-');
            if(connections.TryGetValue(computers[0], out var existingConnections))
            {
                existingConnections.Add(computers[1]);
            }
            else
            {
                connections[computers[0]] = [computers[1]];
            }

            if(connections.TryGetValue(computers[1], out existingConnections))
            {
                existingConnections.Add(computers[0]);
            }
            else
            {
                connections[computers[1]] = [computers[0]];
            }
        }
    }
}