using System;
using System.Threading;
using System.Threading.Tasks;

namespace day8;
public class Day8 {
    public static void Main()
    {
        var input = File.ReadAllLines("inputs/input");
        
        Dictionary<string, string> nodes = [];
        string instructions = input[0];
        List<string> part2Nodes = [];
        foreach (var line in input)
        {
            var tmp = line.Split(" = ");
            if(tmp.Length == 2)
            {
                var name = tmp[0];
                nodes.Add(name, tmp[1]);
                if(name[^1] == 'A') {
                    part2Nodes.Add(name);
                }
            }
                
        }
        int steps = 0;
/*
        //Walk to zzz
        var node = "AAA";
        while(node.Equals("ZZZ") == false) 
        {
            //Console.WriteLine(node);
            var sibling = instructions[steps % instructions.Length] == 'L' ? 0 : 1;
            node = nodes[node][1..^1].Split(", ")[sibling];
            steps++;
        }
        Console.WriteLine("Part1: " + steps);
*/
        List<(int end, int l)> loopLengths = [];
        for(int i = 0; i < part2Nodes.Count; i++) 
        {
            steps = 0;
            var og = part2Nodes[i];
            var sibling = instructions[steps % instructions.Length] == 'L' ? 0 : 1;
            var tmp = nodes[og][1..^1].Split(", ")[sibling];
            List<(string,int)> visited = [];
            List<int> endNodes = [];
            visited.Add((og, steps % instructions.Length));
            steps++;
            var pos = steps % instructions.Length;
            while (!visited.Contains((tmp,pos))) //fix this
            {
                if(tmp[^1] == 'Z')
                    endNodes.Add(steps);
                visited.Add((tmp, pos));
                sibling = instructions[pos] == 'L' ? 0 : 1;
                tmp = nodes[tmp][1..^1].Split(", ")[sibling];
                steps++;
                pos = steps % instructions.Length;
            }
            Console.WriteLine("Loop at step " + steps + " endnodes at steps: " + string.Join(',',endNodes) + ", loop start on step " + visited.IndexOf((tmp,pos)));
            loopLengths.Add((endNodes[0],steps-visited.IndexOf((tmp,pos))));
        }        
        //calculate when loops are all at their end nodes
        /*
        end1 + length1 * x1 = Z
        end2 + length1 * x2 = Z
        end3 + length3 * x3 = Z
        */
        long Z = loopLengths[4].end + loopLengths[4].l;
        while(loopLengths.Any(x => Z-x.end % x.l != 0)) {
            Z +=loopLengths[4].l;
        }
        Console.WriteLine("Part2: " + Z);
    }
}