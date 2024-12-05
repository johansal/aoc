internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");

        var part1 = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                for(int d = 0; d < 8; d++)
                {
                    if(Find("XMAS", input, i, j, d))
                    {
                        part1++;
                    }
                }
            }
        }
        Console.WriteLine("Part1: " + part1);
        
        var part2 = 0;
        for (int i = 1; i < input.Length-1; i++)
        {
            for (int j = 1; j < input[i].Length-1; j++)
            {
                if(input[i][j] == 'A' && CheckX_mas(input, i, j))
                    part2++;
            }
        }

        Console.WriteLine("Part2: " + part2);
    }

    // Check if word is found from input starting at row i, column j, by going to direction d (0=east,1=southeast...)
    private static bool Find(string word, string[] input, int i, int j, int d)
    {
        if(i < 0 || j < 0 || i >= input.Length || j >= input[0].Length || word[0] != input[i][j])
        {
            return false;
        }
        else if(word.Length == 1) 
        {
            return true;
        }
        else 
        {
            if (d == 0)
                j++;
            else if (d == 1)
            {
                i++;
                j++;
            }
            else if (d == 2)
                i++;
            else if (d == 3)
            {
                i++;
                j--;
            }
            else if (d == 4)
                j--;
            else if (d == 5)
            {
                i--;
                j--;
            }
            else if (d == 6)
                i--;
            else if (d == 7)
            {
                i--;
                j++;
            }
            return Find(word[1..], input, i, j , d);
        }
    }

    // Check if A in coordinates i,j are part of X-mas in input:
    // M S S M S S M M
    //  A   A   A   A
    // M S S M M M S S
    private static bool CheckX_mas(string[] input, int i, int j)
    {
        List<string> find = ["MASMAS", "SAMSAM", "SAMMAS", "MASSAM"];
        var value = $"{input[i-1][j-1]}{input[i][j]}{input[i+1][j+1]}{input[i+1][j-1]}{input[i][j]}{input[i-1][j+1]}";
        return find.Contains(value);
    }
}