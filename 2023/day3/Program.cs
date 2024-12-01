using System.Buffers;
using System.Linq;

namespace day3;
public class Day3 {
    public static void Main() 
    {
        var input = File.ReadAllLines("./inputs/input");
        char[][] engine = new char[input.Length][];
        List<(int x, int y, string partNo)> parts = [];
        List<(int x, int y)> gears = [];
        int part1 = 0;
        int part2 = 0;

        //init schematics array
        for (int i = 0; i < input.Length; i++)
        {
            engine[i] = input[i].ToCharArray();
        }

        //loop the schematics and find partnumbers
        bool isPartNo = false;
        string partStr = "";
        for (int i = 0; i < engine.Length; i++)
        {          
            for (int j = 0; j < engine[i].Length; j++)
            {
                if (IsNumber(engine[i][j]))
                {
                    partStr += engine[i][j];
                    if(!isPartNo)
                    {
                        isPartNo = HasAdjacentSymbol(i,j,engine);
                    }
                }
                else {
                    if(engine[i][j] == '*')
                    {
                        gears.Add((i,j));
                    }
                    if(!string.IsNullOrEmpty(partStr) && isPartNo)
                    {
                        part1 += int.Parse(partStr);
                        parts.Add((i, j - partStr.Length, partStr));
                    }
                    partStr = "";
                    isPartNo = false;
                }
            }
            if(!string.IsNullOrEmpty(partStr) && isPartNo)
            {
                part1 +=int.Parse(partStr);
                parts.Add((i, engine[i].Length - partStr.Length, partStr));
            }
            partStr = "";
            isPartNo = false;
        }
        if(!string.IsNullOrEmpty(partStr) && isPartNo)
        {
            part1 += int.Parse(partStr);
            parts.Add((engine.Length-1, engine[^1].Length - partStr.Length, partStr));
        }

        Console.WriteLine($"Part1: {part1}");//530849

        //find * that are gears
        foreach(var gear in gears) {
            var adjacentParts = parts.Where(x => (x.x <= gear.x + 1 && x.x >= gear.x - 1) && (x.y >= gear.y - x.partNo.Length && x.y <= gear.y + 1)).ToList();
            if(adjacentParts.Count == 2) 
            {
                part2 += int.Parse(adjacentParts[0].partNo) * int.Parse(adjacentParts[1].partNo);
            }
        }
        Console.WriteLine($"Part2: {part2}");//84900879
    }
    // Helper method to check if char is a number
    const string numbers = "0123456789";
    static readonly SearchValues<char> searchNumbers = SearchValues.Create(numbers);

    public static bool IsNumber(char c)
    {
        return searchNumbers.Contains(c);
    }
    // Helper method to check if char is a symbol
    public static bool IsSymbol(char c) 
    {
        return !(IsNumber(c) || c == '.');
    }
    // Helper method to check adjacent positions
    public static bool HasAdjacentSymbol(int x, int y, char[][] arr)
    {
        if(x > 0 && IsSymbol(arr[x-1][y]))
        {
            return true;

        }
        else if (x < arr.Length - 1 && IsSymbol(arr[x+1][y]))
        {
            return true;
        }
        else if(y > 0 && IsSymbol(arr[x][y-1]))
        {
            return true;

        }
        else if (y < arr[x].Length - 1 && IsSymbol(arr[x][y+1]))
        {
            return true;
        }
        else if(x > 0 && y > 0 && IsSymbol(arr[x-1][y-1]))
        {
            return true;

        }
        else if (x < arr.Length - 1 && y < arr[x].Length - 1 && IsSymbol(arr[x+1][y+1]))
        {
            return true;
        }
        else if(x < arr.Length - 1 && y > 0 && IsSymbol(arr[x+1][y-1]))
        {
            return true;

        }
        else if (x > 0 && y < arr[x].Length - 1 && IsSymbol(arr[x-1][y+1]))
        {
            return true;
        }
        return false;
    }
    public static int AdjacentPartNums(int x, int y, char[][] arr) {
        int ret = 0;
        //count numbers on top
        if(x > 0) {
            int tmp = 0;
            bool middleIsNumber = false;
            if(IsNumber(arr[x-1][y-1])) 
            {
                tmp++;
            }
            if(IsNumber(arr[x-1][y])) 
            {
                middleIsNumber = true;
                tmp++;
            }
            if(IsNumber(arr[x-1][y+1])) 
            {
                tmp++;
            }
            if(tmp == 3)
            {
                ret++;
            }
            else if(tmp == 2)
            {
                if(middleIsNumber) {
                    ret++;
                }
                else  {
                    ret += 2;
                }
            }
            else {
                ret += tmp;
            }
        }
        //count same row
        if(y > 0 && IsNumber(arr[x][y-1]))
        {
            ret++;
        }
        if(y < arr[x].Length - 1 && IsNumber(arr[x][y+1]))
        {
            ret++;
        }
        //count bottom row
        if(x < arr.Length - 1) {
            int tmp = 0;
            bool middleIsNumber = false;
            if(y > 0 && IsNumber(arr[x+1][y-1])) 
            {
                tmp++;
            }
            if(IsNumber(arr[x+1][y])) 
            {
                middleIsNumber = true;
                tmp++;
            }
            if(y < arr[x].Length - 1 && IsNumber(arr[x+1][y+1])) 
            {
                tmp++;
            }
            if(tmp == 3)
            {
                ret++;
            }
            else if(tmp == 2)
            {
                if(middleIsNumber) {
                    ret++;
                }
                else  {
                    ret += 2;
                }
            }
            else {
                ret += tmp;
            }
        }
        return ret;
    }
}