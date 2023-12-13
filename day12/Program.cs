using System.Text.RegularExpressions;

public class Day12
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("inputs/test");
        int part1 = 0;
        foreach (var inputLine in input)
        {
            var line = inputLine.Split(" ");
            var springs = line[0];
            var groups = line[1].Split(",");

            part1 += Solve(springs, groups);
        }
        Console.WriteLine("Part1: " + part1);//6958

        int part2 = 0;
        int row = 1;
        foreach (var inputLine in input)
        {
            var line = inputLine.Split(" ");
            var springs = line[0] + "?" + line[0] + "?" + line[0] + "?" + line[0] + "?" + line[0];
            var groups = (line[1] + "," + line[1] + "," + line[1] + "," + line[1] + "," + line[1]).Split(",");

            part2 += Solve(springs, groups);
            Console.WriteLine(row);
            row++;
        }
        Console.WriteLine("Part2: " + part2);
    }
    private static int Solve(string springs, string[] groups) {
        var pattern = TestPattern(groups);
        List<string> combinations = [springs];
        for (int i = 0; i < springs.Length; i++)
        {
            if (springs[i] == '?')
            {
                List<string> additions = [];
                for (int j = 0; j < combinations.Count; j++)
                {
                    char[] s = combinations[j].ToCharArray();
                    s[i] = '#';
                    var test = new string(s);
                    if(Regex.IsMatch(test, pattern))
                        additions.Add(test);
                    s[i] = '.';
                    test = new string(s);
                    if(Regex.IsMatch(test, pattern))
                        additions.Add(test);
                }
                combinations = additions;
            }
        }
        return combinations.Count;
        /*
        int valid = 0;
        foreach (var c in combinations)
        {
            //Console.WriteLine(c);
            var t = c.Split(".", StringSplitOptions.RemoveEmptyEntries);
            bool validB = t.Length == groups.Length;
            for (int i = 0; validB && i < t.Length; i++)
            {
                if (t[i].Length != int.Parse(groups[i]))
                {
                    validB = false;
                    break;
                }
            }
            if (validB)
                valid++;
        }
        return valid;*/      
    }
    private static string TestPattern(string[] groups) {
        
        //TODO: pattern needs to improvement, check that there are no # outside of the match
        // adding [^#]* to start and end won't do but something like that :D
        
        string pattern = @"[#\?]{"+groups[0]+"}";
        for(int i = 1; i < groups.Length; i++)
        {
            pattern += @"[\.\?]+[#\?]{"+groups[i]+"}";
        }
        return pattern;
    }
}