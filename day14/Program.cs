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
        Console.WriteLine("Part1:");
        CalculateLoad(ref refleftorDish);
        RollWest(ref refleftorDish);
        RollSouth(ref refleftorDish);
        RollEast(ref refleftorDish);
        for(int i = 1; i < 1000000000; i++)
        {
            Console.WriteLine("Cycle " + i);
            RollNorth(ref refleftorDish);
            RollWest(ref refleftorDish);
            RollSouth(ref refleftorDish);
            RollEast(ref refleftorDish);     
        }
        Console.WriteLine("Part2:");
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
    private static void RollWest(ref char[][] arr) {
        for (int i = 0; i < arr.Length; i++)
        {
            int free = 0;
            for (int j = 0; j < arr[i].Length; j++)
            {
                if (arr[i][j] == 'O')
                {
                    if(j != free)
                    {
                        arr[i][free] = 'O';
                        arr[i][j] = '.';
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
    private static void RollEast(ref char[][] arr) {
        for (int i = 0; i < arr.Length; i++)
        {
            int free = arr[i].Length - 1;
            for (int j =  arr[i].Length - 1; j >= 0; j--)
            {
                if (arr[i][j] == 'O')
                {
                    if(j != free)
                    {
                        arr[i][free] = 'O';
                        arr[i][j] = '.';
                    }
                    free--;
                }
                else if (arr[j][i] == '#')
                {
                    free = j - 1;
                }
            }
        }
    }
    private static void RollSouth(ref char[][] arr) {
        for (int i = 0; i < arr[0].Length; i++)
        {
            int free = arr.Length - 1;
            for (int j = arr.Length - 1; j >= 0; j--)
            {
                if (arr[j][i] == 'O')
                {
                    if(j != free)
                    {
                        arr[free][i] = 'O';
                        arr[j][i] = '.';
                    }
                    free--;
                }
                else if (arr[j][i] == '#')
                {
                    free = j - 1;
                }
            }
        }
    }
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