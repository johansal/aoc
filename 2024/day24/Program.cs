internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        Dictionary<string,bool> wires = [];
        List<string[]> backlog = [];
        Dictionary<string, string> connections = [];

        bool isInit = true;
        foreach(var line in input)
        {
            if(string.IsNullOrEmpty(line))
                isInit = false;
            else if(isInit)
            {
                var w = line.Split(": ");
                wires[w[0]] = w[1] == "1";
            }
            else {
                // simulate 'x00 AND y00 -> z00'
                var w = line.Split(' ');
                var wPart2 = line.Split(" -> ");
                connections[wPart2[1]] = wPart2[0];
                if(wires.ContainsKey(w[0]) && wires.ContainsKey(w[2]))
                {
                    wires[w[4]] = Logic(wires[w[0]], w[1], wires[w[2]]);
                }
                else {
                    backlog.Add(w);
                }
            }
        }
        while(backlog.Count != 0)
        {
            List<string[]> done = [];
            foreach(var w in backlog)
            {
                if(wires.ContainsKey(w[0]) && wires.ContainsKey(w[2]))
                {
                    wires[w[4]] = Logic(wires[w[0]], w[1], wires[w[2]]);
                    done.Add(w);
                }
            }
            foreach(var w in done)
            {
                backlog.Remove(w);
            }
        }
        ulong part1 = 0;
        foreach(var w in wires.Keys.Where(x => x.StartsWith('z')).OrderByDescending(x => x))
        {
            //Console.WriteLine($"{w} {wires[w]}");
            Console.Write(wires[w] ? 1UL : 0UL);
            part1 = (part1 << 1) | (wires[w] ? 1UL : 0UL);
        }
        Console.Write("\n");
        Console.WriteLine(part1);
        
        //part2
        //no clever solution, just dts the connections and print to console, find anomalies in normal addition by hand and swap to correct.
        Dictionary<string, string> visited = [];
        for(int i = 0; i < 45; i++)
        {
            string port = "z";
            port += i < 10 ? "0"+i : i;
            Console.WriteLine($"{port} is connected to {Trace(port, connections, visited)}");
        }
        string[] part2 = ["z31","dmh","z38","dvq","z11","rpv","ctg","rpb"];
        Console.WriteLine(string.Join(",",part2.Order()));
    }
    private static string Trace(string key, Dictionary<string, string> connections, Dictionary<string, string> visited) 
    {
        var c = connections[key];
        var next = c.Split(" ");
        var n0 = ((next[0].StartsWith('x') || next[0].StartsWith('y') || visited.ContainsKey(key)) == false) ? Trace(next[0], connections, visited) : next[0];
        var n2 = ((next[2].StartsWith('x') || next[2].StartsWith('y') || visited.ContainsKey(key)) == false) ? Trace(next[2], connections, visited) : next[2];
        visited[key] = c;
        return $"({n0} {next[1]} {n2})={key}";
    }
    private static bool Logic(bool wire, string port, bool wire2)
    {
        var ret  = port switch
        {
            "AND" => wire && wire2,
            "OR" => wire || wire2,
            "XOR" => wire ^ wire2,
            _ => throw new Exception("invalid operand"),
        };
        return ret;
    }
}