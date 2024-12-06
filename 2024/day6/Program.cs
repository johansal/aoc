internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        char[][] matrix = GetCharMatrix(input); 

        // Find guard
        int i = -1;
        int j = -1;
        for(i = 0; i < input.Length; i++)
        {
            j = input[i].IndexOf('^');
            if(j > -1)
            {
                break;
            }
        }
        // Set guard direction and traverse
        int d = 0;

        var part1 = Traverse(matrix, i, j, d).visited;
        Console.WriteLine(part1.Count());
        
        int part2 = 0;
        foreach((int vI, int vJ) in part1.Skip(1)) 
        {
            char tmp = matrix[vI][vJ];
            matrix[vI][vJ] = '#';
            part2 += Traverse(matrix, i, j, d).loop;
            matrix[vI][vJ] = tmp;
        }
        Console.WriteLine(part2);
    }
    private static (IEnumerable<(int i, int j)> visited, int loop) Traverse(char[][] matrix, int i, int j, int d)
    {
        List<(int i, int j, int d)> visited = [];
        int loop = 0;
        while (true) 
        {
            if (visited.Contains((i,j,d)))
            {
                loop = 1;
                break;
            }
            visited.Add((i,j,d));
            var next = GetNextPosition(i,j,d);
            if (BoundaryCheck(matrix.Length, matrix[0].Length, next.i, next.j) == false)
            {
                break;
            }
            if (matrix[next.i][next.j] == '#')
            {
                d = Turn90Right(d);
                continue;
            }
            i = next.i;
            j = next.j;
        }
        return (visited.Select(x => (x.i,x.j)).Distinct(),loop);
    } 
    private static char[][] GetCharMatrix(string[] input) {
        char[][] output = new char[input.Length][];
        for(int i = 0; i < input.Length; i++)
        {
            output[i] = new char[input[i].Length];
            for(int j = 0; j < input[0].Length; j++)
            {
                output[i][j] = input[i][j];
            }
        }
        return output;
    }
    private static (int i, int j) GetNextPosition(int i, int j, int direction)
    {
        (int i, int j) next = direction switch
        {
            0 => (i - 1, j),
            1 => (i, j + 1),
            2 => (i + 1, j),
            3 => (i, j - 1),
            _ => throw new Exception("invalid direction"),
        };
        return next;
    }
    private static bool BoundaryCheck(int mRows, int mCols, int row, int column)
    {
        return row >= 0 && column >= 0 && row < mRows && column < mCols;
    }
        private static int Turn90Right(int direction) 
    {
        direction++;
        return direction %= 4;
    }
}