internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");

        List<List<int>> keys = [];
        List<List<int>> locks = [];

        List<string> current = [];
        List<int> tmp = [];
        foreach(var line in input)
        {
            if(string.IsNullOrEmpty(line))
            {
                tmp = HeightMap(current[1..^1]);
                Console.WriteLine(string.Join(",", tmp));
                if(current[0][0] == '.')
                {
                    //key
                    keys.Add(tmp);
                }
                else {
                    //lock
                    locks.Add(tmp);
                }
                current = [];
            }
            else {
                current.Add(line);
            }
        }
        tmp = HeightMap(current[1..^1]);
        Console.WriteLine(string.Join(",", tmp));
        if(current[0][0] == '.')
        {
            //key
            keys.Add(tmp);
        }
        else {
            //lock
            locks.Add(tmp);
        }

        var part1 = 0;
        foreach(var key in keys)
        {
            foreach(var loc in locks)
            {
                bool fits = true;
                for(int i = 0; i < 5; i++)
                {
                    if(loc[i] > 5 - key[i])
                    {
                        fits = false;
                        break;
                    }
                }
                if(fits)
                    part1++;
            }
        }
        Console.WriteLine(part1); 
    }
    private static List<int> HeightMap(List<string> input) {
        List<int> heights = [0,0,0,0,0];
        for(int i = 0; i < input.Count; i++)
        {
            for(int j = 0; j < input[i].Length; j++)
            {
                if(input[i][j] == '#')
                {
                    heights[j]++;
                }
            }
        }
        return heights;
    }
}