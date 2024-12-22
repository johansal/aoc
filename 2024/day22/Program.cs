internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        ulong part1 = 0;
        Dictionary<string, int> seqPrice = [];
        foreach(var line in input)
        {
            HashSet<string> sequences = [];
            List<int> sequence = [];
            ulong secret = ulong.Parse(line);
            // price is the last digit of the "random" secret
            int price = (int)(secret % 10);
            for(int i = 0; i < 2000; i++)
            {
                secret = GetSecret(secret);
                int newPrice = (int)(secret % 10);
                int change = newPrice - price;
                price = newPrice; 
                // create price change sequences of 4, 
                // save price if this was the first time seq was encountered for this monkey
                // if it was, add the price to total for this sequence
                sequence.Add(change);
                if(i > 3)
                    sequence.RemoveAt(0);
                if (i >= 3)
                {
                    var hash = string.Join(",", sequence);
                    if(sequences.Add(hash))
                    {
                        if(seqPrice.ContainsKey(hash))
                            seqPrice[hash] += price;
                        else
                            seqPrice[hash] = price;
                        //Console.WriteLine($"New seq {hash}: {price}");
                    }
                }
            }
            part1 += secret;
            //Console.WriteLine($"{line}: {secret}");
        }
        Console.WriteLine(part1);
        Console.WriteLine(seqPrice.Values.Max());
        
    }
    private static ulong GetSecret(ulong secret)
    {
        // Use bitwise operator for the calculations since all numbers are 2powN
        //secret * 64
        secret = Prune(Mix(secret << 6, secret));
        //secret / 32
        secret = Prune(Mix(secret >> 5, secret));
        //secret * 2048
        secret = Prune(Mix(secret << 11, secret));
        return secret;
    }
    private static ulong Mix(ulong value, ulong secret)
    {
        return value ^ secret;
    }
    private static ulong Prune(ulong secret)
    {
        // secret % 16777216 = secret % 2pow24 = secret & ((1 << 24) - 1)
        return secret & ((1 << 24) - 1);
    }
}