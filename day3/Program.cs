using System.Buffers;

namespace day3;
public class Day3 {
    public static void Main() 
    {
        var input = File.ReadAllLines("./inputs/input");
        char[][] engine = new char[input.Length][];
        int part1 = 0;
        List<int> parts = new();

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
                    if(!string.IsNullOrEmpty(partStr) && isPartNo)
                    {
                        var p = int.Parse(partStr);
                        parts.Add(p);
                        part1 += p;
                    }
                    partStr = "";
                    isPartNo = false;
                }
            }
            if(!string.IsNullOrEmpty(partStr) && isPartNo)
            {
                var p = int.Parse(partStr);
                parts.Add(p);
                part1 += p;
            }
            partStr = "";
            isPartNo = false;
        }
        if(!string.IsNullOrEmpty(partStr) && isPartNo)
        {
            var p = int.Parse(partStr);
            parts.Add(p);
            part1 += p;
        }

        Console.WriteLine($"Part1: {part1}");//530849
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
}