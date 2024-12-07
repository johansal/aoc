internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        long part1 = 0;
        long part2 = 0;
        foreach (var line in input)
        {
            // Parse line into result and list of numbers
            var split = line.Split(": ");
            var result = long.Parse(split[0]);
            part1 += Solve(result, 0, 0, split[1].Split(' '), false);
            part2 += Solve(result, 0, 0, split[1].Split(' '), true);
        }
        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }
    private static long Solve(long result, long total, int i, string[] nums, bool concat)
    {
        if (nums.Length == i)
        {
            return result == total ? result : 0;
        }
        else if (total > result)
        {
            return 0;
        }
        else
        {
            var nextValue = int.Parse(nums[i]);
            var nextConcat = long.Parse(total.ToString() + nums[i]);
            i++;

            return (
                Solve(result, total + nextValue, i, nums, concat) != 0 ||
                Solve(result, total * nextValue, i, nums, concat) != 0 ||
                (concat && Solve(result, nextConcat, i, nums, concat) != 0)
            ) ? result : 0;

        }
    }
}