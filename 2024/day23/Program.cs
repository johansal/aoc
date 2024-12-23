internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        Dictionary<string,List<string>> connections = [];
        ParseConnections(input, connections);
        
        // Part 1 find all groups of 3 that contain computer with name t
        HashSet<string> groups = [];
        foreach(var c in connections.Keys.Where(key => key.StartsWith('t')))
        {
            var neibor = connections[c];
            foreach(var n in neibor) {
                var neiborN = connections[n];
                foreach(var n2 in neiborN) {
                    if(n2 != c && connections[n2].Contains(c)) {
                        //Console.WriteLine($"Comparing: {string.Join(",",[c,n,n2])}");
                        //var common = connections[c].Intersect(connections[n]).Intersect(connections[n2]);
                        //if(common.Count() != 0)
                            //Console.WriteLine($"Found common: {string.Join(",",common)}");
                        List<string> group = [c, n, n2];
                        groups.Add(string.Join(",",group.Order()));
                    }
                }
            }
        }
        foreach(var g in groups) {
            Console.WriteLine(g);
        }
        Console.WriteLine(groups.Count);//< 2292
    }
    private static void DFS(
        string current, string parent, Dictionary<string, bool> visited, List<string> path, 
        HashSet<string> groups, Dictionary<string,List<string>> connections, int maxDepth)
    {
        if(maxDepth == 0)
            return;
        visited[current] = true;
        path.Add(current);
        Console.WriteLine(string.Join(",",path));
        foreach (var neighbor in connections[current])
        {
            // Recurse if the neighbor is not visited
            if ((visited.TryGetValue(neighbor, out var b) && b) == false)
            {
                DFS(neighbor, current, visited, path, groups, connections, maxDepth-1);
            }
            // Its a group if the neighbor is visited and isn't the parent
            else if (neighbor != parent)
            {
                // Build the group
                int start = path.IndexOf(neighbor);
                if(start != -1)
                {
                    var group = new List<string>();
                    for(int i = start; i < path.Count; i++)
                    {
                        group.Add(path[i]);
                    }
                    //group.Add(neighbor);
                    var g = string.Join(",",group.Order());
                    Console.WriteLine($"Found group: {g}");
                    groups.Add(g);
                }
                
            }
        }

        // Backtrack
        path.RemoveAt(path.Count - 1);
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