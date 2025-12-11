static long FindAllPaths(string current, string end, ref Dictionary<string, List<string>> graph, HashSet<string> mustVisit, Dictionary<string, long>? memo = null)
{
    memo ??= [];
    
    // Create cache key from current position and remaining mustVisit nodes
    var cacheKey = current + ":" + string.Join(",", mustVisit.OrderBy(x => x));
    
    if (memo.TryGetValue(cacheKey, out long cachedResult))
    {
        return cachedResult;
    }

    if (current == end)
    {
        if(mustVisit.Count == 0)
            return 1;
        else
            return 0;
    }

    mustVisit.Remove(current);
    long pathCount = 0;

    foreach (var neighbor in graph[current])
    {
        pathCount += FindAllPaths(neighbor, end, ref graph, [.. mustVisit], memo);
    }

    memo[cacheKey] = pathCount;
    return pathCount;
}

var input = File.ReadAllLines("input");

Dictionary<string, List<string>> graph = [];
foreach (var line in input)
{
    var parts = line.Split(':');
    var node = parts[0];
    var edges = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    graph[node] = edges;
}

Console.WriteLine(FindAllPaths("you", "out", ref graph, []));
Console.WriteLine(FindAllPaths("svr", "out", ref graph, ["fft", "dac"]));