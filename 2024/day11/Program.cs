internal class Program
{
    private static void Main()
    {
        var stones = File.ReadAllLines("input")[0].Split(" ").ToList();
        for (int i = 0; i < 25; i++)
        {
            //Console.WriteLine(string.Join(" ", stones));
            stones = Blink(stones);
        }
        Console.WriteLine(stones.Count);
        for (int i = 25; i < 75; i++)
        {
            //Console.WriteLine(string.Join(" ", stones));
            stones = Blink(stones);
        }
        Console.WriteLine(stones.Count);
    }
    private static List<string> Blink(List<string> stones)
    {
        List<string> result = [];
        foreach (var stone in stones)
        {
            if(stone == "0")
            {
                result.Add("1");
            }
            else if(stone.Length % 2 == 0)
            {
                var tmp = stone[..(stone.Length / 2)].TrimStart('0');
                if (string.IsNullOrEmpty(tmp))
                {
                    result.Add("0");
                }
                else {
                    result.Add(tmp);
                }
                tmp = stone[(stone.Length / 2)..].TrimStart('0');
                if (string.IsNullOrEmpty(tmp))
                {
                    result.Add("0");
                }
                else {
                    result.Add(tmp);
                }
            }
            else {
                var tmp = long.Parse(stone) * 2024;
                result.Add(tmp.ToString());
            }           
        }
        return result;
    }
}