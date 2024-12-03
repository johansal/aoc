using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var text = File.ReadAllText("input");
        var pattern = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)|do\(\)|don't\(\)";

        var m = Regex.Match(text, pattern);
        int part1 = 0;
        int part2 = 0;
        bool enabled = true;

        while(m.Success)
        {
            var g = m.Groups[0].Value;
            if(g.Equals("do()"))
            {
                enabled = true;
            }
            else if(g.Equals("don't()"))
            {
                enabled = false;
            }
            else {
                var a = int.Parse(m.Groups[1].Value);
                var b = int.Parse(m.Groups[2].Value);

                part1 += a*b;
                
                if (enabled)
                {
                    part2 += a*b;
                }
            }

            m = m.NextMatch();
        
        }

        Console.WriteLine(part1);
        Console.WriteLine(part2);

    }
}