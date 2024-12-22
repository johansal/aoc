﻿internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        ulong part1 = 0;
        foreach(var line in input)
        {
            ulong secret = ulong.Parse(line);
            for(int i = 0; i < 2000; i++)
            {
                secret = GetSecret(secret);
            }
            part1 += secret;
            //Console.WriteLine($"{line}: {secret}");
        }
        Console.WriteLine(part1);
        
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