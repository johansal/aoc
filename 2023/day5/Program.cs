namespace day5;

public class Day5 {
    public static void Main() 
    {
        //Part1("test");
        Part2("test");
    }
    private static void Part2(string file)
    {
        //parse input to almanac
        var input = File.ReadAllLines($"inputs/{file}");
        var tmpSeeds = input[0].Split(": ")[1].Split(" ");
        List<(string seed, string range)> seeds = [];
        for(int i = 0; i < tmpSeeds.Length-1; i += 2) {
            seeds.Add((tmpSeeds[i],tmpSeeds[i+1]));
        }
        List<(string seed, string range)> converted = [];
        List<List<(string destination, string source, string range)>> almanac = [];
        List<(string destination, string source, string range)> mapList = [];
        for(int i = 2; i < input.Length; i++)
        {
            if(input[i].Contains("map"))
            {
                mapList = [];
            }
            else if (string.IsNullOrEmpty(input[i])) {
                almanac.Add(mapList);
            }
            else {
                var tmpMap = input[i].Split(" ");
                mapList.Add((tmpMap[0], tmpMap[1], tmpMap[2]));
            }
        }
        almanac.Add(mapList);
        
        foreach(var list in almanac) {
            while (seeds.Count > 0) 
            {
                foreach(var map in list) {
                    /*
                    seed > source ?
                    (seed - source) < range ?
                    return destination + (seed - source)
                    (seed - source) + range2 <= range ?
                        true: return range2 
                        false: add new seed destination + range, new range ((seed - source) + range2)-range

                    */
                }
                seeds.RemoveAt(0);
            }
        }       
    }

    private static void Part1(string file)
    {
        var input = File.ReadAllLines($"inputs/{file}");
        var seeds = input[0].Split(": ")[1].Split(" ");
        bool[] found = new bool[seeds.Length];
        for(int i = 1; i < input.Length; i++)
        {
            if(string.IsNullOrEmpty(input[i]))
            {
                //old map ends
                Console.WriteLine("[{0}]", string.Join(", ", seeds));
                for(int f = 0; f < found.Length; f++) {
                    found[f] = false;
                }
            }
            else if(input[i].Contains("map")) {
                //new map starts
                Console.WriteLine(input[i]);
            }
            else {
                //map row
                //convert source seeds to destination
                var map = input[i].Split(" ");
                for(var s = 0; s < seeds.Length; s++)
                {
                    if(!found[s]) {
                        seeds[s] = ConvertSeed(seeds[s], map, ref found[s]);
                    }
                }
            }
        }
        Console.WriteLine("[{0}]", string.Join(", ", seeds));
        Console.WriteLine("Part1: " + MinStr(seeds));
    }
    private static string ConvertSeed(string seed, string[] map, ref bool found)
    {
        //check if seed is >= destination start
        if (IsGreater(seed, map[1]) != -1 )
        {
            //subtract destinations start from original seed
            var subtraction = SubtractStr(seed,map[1]);
            //check if destination range is larger than the substraction (= is the seed in the range)
            if(IsGreater(map[2],subtraction) == 1) {
                //seed was in the range, mark as found and 
                found = true;
                return AddStr(map[0], subtraction);
            }
        }
        return seed;
    }
    public static string MinStr(string[] values) {
        string min = values[0];
        for (int i = 1; i < values.Length; i++)
        {
            if(IsGreater(min, values[i]) == 1)
                min = values[i];
        }
        return min;
    }
    public static string SubtractStr(string value, string subtractor)
    {
        if (subtractor.Length > value.Length)
            throw new NotImplementedException();
        if (value.Equals(subtractor))
            return "0";
        var result = "";
        var a = value.ToCharArray();
        var b = subtractor.ToCharArray();
        for(int i = 0; i < a.Length; i++)
        {   
            if(i < b.Length) 
            {
                if (a[a.Length - i - 1] < b[b.Length - i -1])
                {
                    a[a.Length - i - 1] += (char)10;
                    a[a.Length - i - 2]--;
                }
                result = a[a.Length - i - 1] - b[b.Length - i -1] + result;
            }
            else {
                result = a[a.Length - i - 1] + result;
            }
        }
        return result.TrimStart('0');
    }
    public static int IsGreater(string value, string compare) {
        if(value.Equals(compare))
        {
            return 0;
        }
        else if(value.Length > compare.Length) 
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
            //Console.WriteLine(init[init.Length - 1 - i] + " + " + add[add.Length - 1 - i]);
            var digit = (char)(init[init.Length - 1 - i] + add[add.Length - 1 - i] - '0');
            if(digit > '9') {
                overflow = true;
                digit = (char)('0' + (digit - ':'));
            }
            result = digit + result;
            //Console.WriteLine("result "+result);
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
            //Console.WriteLine("result "+result);
        }
        if(overflow)
        {
            result = '1' + result;
        }
        
        return result.TrimStart('0');
    }
}