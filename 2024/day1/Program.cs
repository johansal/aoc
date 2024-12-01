using day1;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        OrderedList<int> list1 = new();
        OrderedList<int> list2 = new();

        foreach(var line in input)
        {
            var (a,b) = ParseInput(line);
            //Console.WriteLine($"{a}   {b}");
            list1.Add(a);
            list2.Add(b);
        }

        Console.WriteLine($"Part1: {Part1(list1, list2)}");
        Console.WriteLine($"Part2: {Part2(list1, list2)}");
    }

    private static int Part1(OrderedList<int> list1, OrderedList<int> list2)
    {
        int distance = 0;
        for(int i = 0; i < list1.Count(); i++)
        {
            var diff = list1.GetItemAt(i) - list2.GetItemAt(i);
            if (diff < 0)
                diff *= -1;
            distance += diff;
        }
        return distance;
    }
    private static int Part2(OrderedList<int> list1, OrderedList<int> list2)
    {
        Dictionary<int, int> dict = [];
        int similarityScore = 0;
        for(int i = 0; i < list1.Count(); i++)
        {
            int key = list1.GetItemAt(i);
            if(dict.TryGetValue(key, out int count))
            {
                similarityScore += key * count;
            }
            else {
                dict[key] = list2.Count(key);
                similarityScore += key * dict[key];
            }
        }
        return similarityScore;
    }

    private static (int,int) ParseInput(string line)
    {
        var items = line.Split("   ");
        if(items.Length != 2)
            throw new Exception("Parsing failed!");
        return (int.Parse(items[0]),int.Parse(items[1]));
    }
}