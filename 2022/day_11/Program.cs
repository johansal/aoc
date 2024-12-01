internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        List<Monkey> monkeys = new();
        List<string> callbacks = new();
        long N = 1;
        foreach (var line in input)
        {
            var cmd = line.Split(": ");
            if(cmd[0].Contains("Monkey")) {
                monkeys.Add(new());
            }
            else if(cmd[0].Contains("Starting")) {
                var items = cmd[1].Split(", ");
                var cM = monkeys.Last();
                foreach(var item in items) {
                    cM.Items.Add(long.Parse(item));
                }
            }
            else if(cmd[0].Contains("Operation")) {
                var tmp = cmd[1].Split(" ");
                callbacks.Add(tmp[3]);
                var cM = monkeys.Last();
                cM.OperationValue = tmp.Last();
            }
            else if(cmd[0].Contains("Test")) {
                var tmp = cmd[1].Split(" ");
                var cM = monkeys.Last();
                cM.TestValue = long.Parse(tmp.Last());
                N *= cM.TestValue;
            }
            else if(cmd[0].Contains("true")) {
                var tmp = cmd[1].Split(" ");
                var cM = monkeys.Last();
                cM.TMonkey = int.Parse(tmp.Last());
            }
            else if(cmd[0].Contains("false")) {
                var tmp = cmd[1].Split(" ");
                var cM = monkeys.Last();
                cM.FMonkey = int.Parse(tmp.Last());
            }
        }

        for(int round = 1; round <= 10000; round++) {
            //1 Round
            for(int i = 0; i < monkeys.Count; i++) {
                if(callbacks[i].Equals("*")) {
                    monkeys[i].Operation(MultiplyOp, N);
                }
                else {
                    monkeys[i].Operation(AddOp, N);
                }
                for(int j = 0; j < monkeys[i].Items.Count; j++) {
                    monkeys[monkeys[i].Test(j)].Items.Add(monkeys[i].Items[j]);
                }
                monkeys[i].Items = new();
            }
        }

        List<long> counts = new();
        foreach(var monkey in monkeys) {
            counts.Add(monkey.InspectionCount);
        }
        counts.Sort();
        Console.WriteLine(counts[^1] * counts[^2]);
    }
    public static long MultiplyOp(long v, string x) {
        if(x.Equals("old"))
            return v * v;
        return v * long.Parse(x);
    }
    public static long AddOp(long v, string x) {
        if(x.Equals("old"))
            return v + v;
        return v + long.Parse(x);
    }
}
