internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("inputs/input");
        int start = -1;
        int result = 0;
        int smudge = 1; //smudge for part1 = 0, part2 = 1
        for (int i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrEmpty(input[i]))
            {
                //check  reflections
                result += FindReflection(start, i-1, ref input, smudge);
                //start new mirror on next row
                start = -1;
            }
            else if (start == -1)
            {
                start = i;
            }
        }
        //check last
        result += FindReflection(start, input.Length-1, ref input, smudge);
        if(smudge == 0)
            Console.WriteLine("Part1:");
        else if(smudge == 1)
            Console.WriteLine("Part2:");
        else
            throw new NotImplementedException();
        Console.WriteLine(result); //32723, 34536
    }
    private static int FindReflection(int start, int end, ref string[] input, int smudge) {
        int flip = 0;
        for(int i = start + ((end-start)/2); i >= start && i < end; i+=flip)
        {
            //Console.WriteLine(i);
            flip += flip < 0 ? -1 : 1;
            flip *= -1;
            int isReflection = MirrorDiff(input[i], input[i+1]);
            int test = i - 1;
            int testEnd = i + 2;
            while(isReflection <= smudge && test >= start && testEnd <= end)
            {
                isReflection += MirrorDiff(input[test], input[testEnd]);
                test--;
                testEnd++;
            }
            if(isReflection == smudge)
            {
                //Console.WriteLine("Horizontal reflection at line " + (i - start));
                return 100 * (i - start + 1);
            }
        }
        List<string> columns = [];
        for(int i = 0; i < input[start].Length; i++)
        {
            string column = "";
            for(int j = 0; j < end-start; j++)
            {
                column += input[start+j][i];

            }
            columns.Add(column);
        }
        flip = 0;
        for(int i = (columns.Count-1)/2; i >= 0 && i < columns.Count - 1; i+=flip)
        //for(int i = 0; i < columns.Count -1; i++)
        {
            flip += flip < 0 ? -1 : 1;
            flip *= -1;
            int isReflection = MirrorDiff(columns[i],columns[i+1]);
            int test = i - 1;
            int testEnd = i + 2;
            while(isReflection <= smudge && test >= 0 && testEnd < columns.Count)
            {
                isReflection += MirrorDiff(columns[test], columns[testEnd]);
                test--;
                testEnd++;
            }
            if(isReflection == smudge)
            {
                //Console.WriteLine("Vertical reflection at column " + i);
                return i+1;
            }
        }
        Console.WriteLine("ARGH! start: " + start);
        return 0;       
    }
    private static int MirrorDiff(string a, string b)
    {
        int diff = 0;
        for(int i = 0; i < a.Length; i++)
        {
            if(a[i] != b[i])
                diff++;
        }
        return diff;
    }
}