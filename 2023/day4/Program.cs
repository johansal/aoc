using System.Linq;

namespace day4;

public class Day4 {
    public static void Main() 
    {
        var input = File.ReadAllLines("inputs/input");
        List<int> copies = Enumerable.Repeat(1, input.Length).ToList();
        var part1 = 0;
        var part2 = 0;
        var cardIndex = 0;
        foreach (var line in input) 
        {
            var points = 0;
            var winningCards = 0;
            var numbers = line[(line.IndexOf(": ")+2)..].Split(" ").ToList();
            var pipe = numbers.FindIndex(x => x == "|");
            if (pipe == -1)
                throw new Exception($"Couldnt find | from {line}");
            for (int i = 0; i < pipe; i++) 
            {
                if(numbers[i] != "" && numbers[pipe..].Contains(numbers[i]))
                {
                    winningCards++;
                    points = AddPoints(points);
                }
            }
            part1 += points;
            for(int j = cardIndex+1; j < cardIndex+winningCards+1 && j < copies.Count; j++) 
            {
                copies[j] += copies[cardIndex];
            }
            Console.WriteLine(copies[cardIndex]);
            part2 += copies[cardIndex];
            cardIndex++;
        }
        Console.WriteLine($"Part1: {part1}");
        Console.WriteLine($"Part2: {part2}");
    }
    public static int AddPoints(int current) {
        if(current == 0)
            current++;
        else 
        {
            current *= 2;
        }
        return current;
    }
}