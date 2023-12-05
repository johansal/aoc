namespace day5;

public class Day5 {
    public static void Main() 
    { /*
        var input = File.ReadAllLines("inputs/test");
        var seeds = input[0].Split(": ")[1].Split(" ");
        for(int i = 1; i < input.Length; i++)
        {
            if(string.IsNullOrEmpty(input[i]))
            {
                //old map ends
            }
            else if(input[i].Contains("map")) {
                //new map starts
            }
            else {
                //map row
                //convert source seeds to destination
                var map = input[i].Split(" ");
                for(var s = 0; s < seeds.Length; s++)
                {
                    seeds[s] = ConvertSeed(seeds[s], map);
                }
            }
        }
        
        Console.WriteLine("Part1: " + seeds.Min());
        */
        Console.WriteLine();
    }
    private static string ConvertSeed(string seed, string[] map)
    {
        return "";
    }
    public static string SubtractStr(string value, string subtractor)
    {
        //todo
    }
    public static int IsGreater(string value, string compare) {
        if(value.Length > compare.Length) 
        {
            return 1;
        }
        else if(value.Length < compare.Length)
        {
            return -1;
        }
        else {
            var v1 = value.ToCharArray();
            var v2 = compare.ToCharArray();
            for (int i = 0; i < v1.Length; i++)
            {
                if(v1[i] > v2[i])
                {
                    return 1;
                }
                else if (v1[i] < v2[i])
                {
                    return -1;
                }
            }
            return 0;
        }

    }
    public static string AddStr(string value, string addition)
    {
        string result = "";
        var init = value.Length >= addition.Length ? value.ToCharArray() : addition.ToCharArray();
        var add = value.Length < addition.Length ? value.ToCharArray() : addition.ToCharArray();
        bool overflow = false;
        for(int i = 0; i < add.Length; i++)
        {
            if (overflow)
            {
                init[init.Length - 1 - i]++;//works?
                overflow = false;
            }
            Console.WriteLine(init[init.Length - 1 - i] + " + " + add[add.Length - 1 - i]);
            var digit = (char)(init[init.Length - 1 - i] + add[add.Length - 1 - i] - '0');
            if(digit > '9') {
                overflow = true;
                digit = (char)('0' + (digit - ':'));
            }
            result = digit + result;
            Console.WriteLine("result "+result);
        }
        for(int i = init.Length - add.Length - 1; i >= 0; i--) {
            if(overflow) {
                init[i]++;
                overflow = false;
            }
            if(init[i] > '9') {
                overflow = true;
                init[i] = (char)('0' + (init[i] - ':'));
            }
            result = init[i] + result;
            Console.WriteLine("result "+result);
        }
        if(overflow)
        {
            result = '1' + result;
        }
        
        return result;
    }
}