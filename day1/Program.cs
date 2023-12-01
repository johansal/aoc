using System.Buffers;

namespace day1;
public class Program {
    private static readonly string[] validStrs = [
        "1","2","3","4","5","6","7","8","9","0",
        "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    ];

    public static void Main(string[] args) 
    {
        var input = File.ReadAllLines("./inputs/input");

        int total = 0;
        foreach (var line in input)
        {
            
                total += FindCalibrationValues_part2(line);
        }
        Console.WriteLine(total);
    }
    public static int FindCalibrationValues_part1(string line) 
    {
        int? first = null;
        int? last = null; 
        foreach (char c in line) 
        {
            if(int.TryParse(c.ToString(), out int value)) 
            {
                first ??= value;
                last = value;
            }
        }
        return SumValues(first, last);
    }
    public static int FindCalibrationValues_part2(string line) 
    {
        int? first = null;
        int? last = null; 
        while(line.Length > 0)
        {
            for(int i = 0; i < validStrs.Length; i++)
            {
                if (line.StartsWith(validStrs[i])) 
                {
                    int value = int.TryParse(validStrs[i], out int v) ? v : i - 9;
                    first ??= value;
                    last = value;
                }
            }

            line = line[1..];
        }
        return SumValues(first, last);
    }
    private static int SumValues(int? first, int? last) {
        var sum = (first ?? throw new Exception("first is null")) * 10 + (last ?? throw new Exception("last is null"));
        return sum;
    }
}