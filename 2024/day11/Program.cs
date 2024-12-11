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
        if(stone == 0)
        {
            return CountStones(1, blinks-1, known);
        }
        else {
            if(known.TryGetValue((stone,blinks), out var k))
            {
                return k;
            }

            var digits = (long)Math.Floor(Math.Log10(stone)) + 1;
            if(digits % 2 == 0)
            {
                //Console.WriteLine(stone + " has " + digits + " digits");
                long stoneCount = 0;
                var div = (long) Math.Pow(10, digits/2);
                var tmp = stone / div;
                var firstPath = CountStones(tmp, blinks-1, known);
                stoneCount += firstPath;
                known[(tmp, blinks-1)] = firstPath;
                tmp = stone % div;
                var nextPath = CountStones(tmp, blinks-1, known);
                stoneCount += nextPath;
                known[(tmp, blinks-1)] = nextPath;
                return stoneCount;
            }
            else {
                return CountStones(stone * 2024, blinks-1, known);
            }         
        }       
    }
}