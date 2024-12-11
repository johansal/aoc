using System.Globalization;

internal class Program
{
    private static void Main()
    {
        var stones = File.ReadAllLines("input")[0].Split(" ").ToList();
        Dictionary<(long,int), long> known = [];
        
        long part1 = 0;
        foreach(var stone in stones)
        {
            part1 += CountStones(int.Parse(stone), 25, known);
        }
        Console.WriteLine(part1);
        
        long part2 = 0;
        foreach(var stone in stones)
        {
            part2 += CountStones(int.Parse(stone), 75, known);
        }
        Console.WriteLine(part2);
    }
    private static long CountStones(long stone, int blinks, Dictionary<(long stone, int blinks), long> known)
    {
        if(blinks == 0)
        {
            return 1;
        }
        // Calculate next stone only if we havent previously seen it before
        if(known.TryGetValue((stone,blinks), out var c))
        {
            return c;
        }
        // If stone 0, next stone is 1
        if(stone == 0)
        {
            var count = CountStones(1, blinks-1, known);
            known[(stone, blinks)] = count;
            return count;
        }
        // If stone.len % 2 = 0, split the stone, else * 2024
        else {
            var digits = (long)Math.Log10(stone) + 1;
            if(digits % 2 == 0)
            {
                // Calculate first half of stone
                var div = (long) Math.Pow(10, digits/2);
                var half = stone / div;
                var firstPath = CountStones(half, blinks-1, known);
                known[(half, blinks-1)] = firstPath;
                // Calculate 2nd half of stone
                half = stone % div;
                var nextPath = CountStones(half, blinks-1, known);
                known[(half, blinks-1)] = nextPath;
                return firstPath + nextPath;
            }
            else {
                var nextPath = CountStones(stone * 2024, blinks-1, known);
                known[(stone, blinks)] = nextPath;
                return nextPath;
            }         
        }       
    }
}