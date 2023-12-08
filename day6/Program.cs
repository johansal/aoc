namespace day6;

public class Day6 {
    public static void Main()
    {
        var input = File.ReadAllLines("inputs/input");
        var times = input[0]["Time:".Length..].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var distances = input[1]["Distance:".Length..].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        long part1 = 1;
        //loop races
        for(int i = 0; i < times.Length; i++)
        {
            var time = Int64.Parse(times[i]);
            var distance = Int64.Parse(distances[i]);
            part1 *= Solve(time,distance);
        }
        Console.WriteLine(part1);
        Console.WriteLine(Solve(Int64.Parse(string.Join("", times)),Int64.Parse(string.Join("", distances))));
    }
    private static long Solve(long time, long distance) {
        //find min charging time
        long x1 = 0;
        long x2 = time - x1;
        while(!(x1 * x2 > distance)) {
            x1++;
            x2 = time - x1;
        }
        long xMin = x1;
        //find max charging time
        x1 = time;
        x2 = time - x1;
        while(!(x1 * x2 > distance)) {
            x1--;
            x2 = time - x1;
        }
        long xMax = x1;
        return xMax - xMin + 1; //assume there is at least one way to beat the record
    }
}