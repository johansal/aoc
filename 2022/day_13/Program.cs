internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        List<Packet> packets = new();
        Packet? line1 = null;
        Packet? line2 = null;
        int pairCounter = 1;
        int part1 = 0;
        int result;
        foreach (var line in input)
        {
            if (line1 == null)
            {
                line1 = new(line);
            }
            else if (line2 == null)
            {
                line2 = new(line);
            }
            else if (string.IsNullOrEmpty(line))
            {
                //compare pairs
                result = line1.CompareTo(line2);
                part1 += result < 0 ? pairCounter : 0;

                //clear pair and increase index
                pairCounter++;
                packets.Add(line1);
                packets.Add(line2);
                line1 = null;
                line2 = null;
            }
        }
        if(line1 != null && line2 != null) {
            result = line1.CompareTo(line2);
            packets.Add(line1);
            packets.Add(line2);
            part1 += result < 0 ? pairCounter : 0;
        }

        Console.WriteLine(part1);
        //Part2
        Packet dividerA = new("[[2]]");
        Packet dividerB = new("[[6]]");
        packets.Add(dividerA);
        packets.Add(dividerB);
        packets.Sort();
        int a = 1 + packets.FindIndex(x=>x.Value == dividerA.Value);
        int b = 1 + packets.FindIndex(x=>x.Value == dividerB.Value);
        Console.WriteLine(a*b);
    }
}