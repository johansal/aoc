namespace day19;

public class Program
{
    private static void Main()
    {
        int part1 = 0;
        var input = File.ReadAllLines("inputs/input");
        int lineNro = 0;
        Dictionary<string,string> workflows = [];

        while(string.IsNullOrEmpty(input[lineNro]) == false)
        {
            var line = input[lineNro];
            var wfStart = line.IndexOf('{');
            var name = line[..wfStart];
            var wf = line.Substring(wfStart+1, line.Length - (wfStart+2));
            workflows[name] = wf;
            lineNro++;

            //Console.WriteLine(name + ": " + wf);
        }

        lineNro++;
        while(lineNro < input.Length) {
            var part = input[lineNro][1..^1].Split(',');
            //handleworkflows
            var rules = workflows["in"].Split(',');
            var result = HandleWorkflow(part, rules);
            while(result.Equals("A") == false && result.Equals("R") == false)
            {
                rules = workflows[result].Split(',');
                result = HandleWorkflow(part, rules);
            }
            if(result.Equals("A"))
            {
                foreach(var p in part)
                {
                    part1 += int.Parse(p[2..]);
                }
            }
            lineNro++;
        }
        Console.WriteLine(part1);
    }

    private static string HandleWorkflow(string[] part, string[] rules)
    {
        foreach(var rule in rules) 
        {
            var tmp = rule.Split(':');
            if(tmp.Length == 1)
                return tmp[0];
            else {
                var category = tmp[0][0];
                var condition = tmp[0][1];
                var value = int.Parse(tmp[0][2..]);

                foreach(var p in part)
                {
                    if (p.Contains(category))
                    {
                        var partValue = int.Parse(p[2..]);
                        if((condition == '<' && partValue < value) || (condition == '>' && partValue > value))
                        {
                            return tmp[1];
                        }
                        else {
                            break;
                        }
                    }
                }
            }
        }
        return "! we should not be here !";
    }

}
