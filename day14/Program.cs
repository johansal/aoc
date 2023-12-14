internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("inputs/input");
        char[][] refleftorDish = new char[input.Length][];

        //parse input
        for (int i = 0; i < input.Length; i++)
        {
            refleftorDish[i] = new char[input[i].Length];
            for (int j = 0; j < input[i].Length; j++)
            {
                refleftorDish[i][j] = input[i][j];
            }
        }
        //roll
        RollNorth(ref refleftorDish);
        CalculateLoad(ref refleftorDish);
    }
    private static void RollNorth(ref char[][] arr) {
        for (int i = 0; i < arr[0].Length; i++)
        {
            int free = 0;
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j][i] == 'O')
                {
                    if(j != free)
                    {
                        arr[free][i] = 'O';
                        arr[j][i] = '.';
                    }
                    free++;
                }
                else if (arr[j][i] == '#')
                {
                    free = j + 1;
                }
            }
        }
    }
    //TODO make rest of the roller methods
    private static void CalculateLoad(ref char[][] arr) {
        int northLoad = 0;
        for (int i = 0; i < arr[0].Length; i++)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j][i] == 'O')
                {
                    northLoad += arr.Length - j;
                }
            }
        }
        Console.WriteLine(northLoad);
    }

    private static void SolvePart1()
    {
        var input = File.ReadAllLines("inputs/test");
        int northLoad = 0;
        for (int i = 0; i < input[0].Length; i++)
        {
            int columnLoad = input.Length;
            for (int j = 0; j < input.Length; j++)
            {
                if (input[j][i] == 'O')
                {
                    northLoad += columnLoad;
                    columnLoad--;
                }
                else if (input[j][i] == '#')
                {
                    columnLoad = input.Length - j - 1;
                }
            }
        }
        Console.WriteLine(northLoad);
    }
}