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

        steps = 0;
        while(part2Nodes.Any(x => x[^1] != 'Z'))
        {
            var sibling = instructions[steps % instructions.Length] == 'L' ? 0 : 1;
            for(int i = 0; i < part2Nodes.Count; i++)
            {              
                part2Nodes[i] = nodes[part2Nodes[i]][1..^1].Split(", ")[sibling];
            }
            steps++;
        }
        Console.WriteLine("Part2: " + steps);
    }

    public 
}

