internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("inputs/test");
        int start = -1;
        int part1 = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrEmpty(input[i]))
            {
                //check  reflections
                part1 += FindReflection(start, i-1, ref input);
                //start new mirror on next row
                start = -1;
            }
            else if (start == -1)
            {
                start = i;
            }
            else {

            }
        }
        //check last
        part1 += FindReflection(start, input.Length-1, ref input);
    }
    private static int FindReflection(int start, int end, ref string[] input) {
        for(int i = start; i < end; i++)
        {
            bool isReflection = input[i].Equals(input[i+1]);
            int test = i - 1;
            int testEnd = i + 2;
            while(isReflection && test >= start && testEnd <= end)
            {
                isReflection = input[test].Equals(input[testEnd]);
                test--;
                testEnd++;
            }
            if(isReflection)
            {
                Console.WriteLine("Horizontal reflection at line " + (i - start));
                return i;
            }
        }
        List<string> columns = [];
        for(int i = 0; i < input[start].Length - 1; i++)
        {
            string column = "";
            for(int j = 0; j < end; j++)
            {
                column += input[j][i];

            }
            columns.Add(column);
        }
        for(int i = 0; i < columns.Count - 1; i++)
        {
            bool isReflection = columns[i].Equals(columns[i+1]);
            int test = i - 1;
            int testEnd = i + 2;
            while(isReflection && test >= 0 && testEnd < columns.Count)
            {
                isReflection = columns[test].Equals(columns[testEnd]);
                test--;
                testEnd++;
            }
            if(isReflection)
            {
                Console.WriteLine("Vertical reflection at column " + i);
                return i;
            }
        }
        return 0;       
    }
}