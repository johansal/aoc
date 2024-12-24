internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        Dictionary<string,bool> wires = [];
        List<string[]> backlog = [];

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
            Console.WriteLine($"Backlog size {backlog.Count}");
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
            part1 = (part1 << 1) | (wires[w] ? 1UL : 0UL);
        }
        Console.WriteLine(part1);
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