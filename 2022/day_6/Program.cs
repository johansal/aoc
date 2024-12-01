internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Part 1: " + solve(4));
        Console.WriteLine("Part 2: " + solve(14));
    }
    private static int solve(int bufferlength) {
        using (var sr = new StreamReader("input"))
        {
            string buffer = "";
            int index = 0;
            while (sr.Peek() >= 0)
            {
                buffer += (char)sr.Read();
                index++;
                
                if(buffer.Length >= bufferlength) {
                    if (bufferIsUnique(buffer))
                    {
                        break;
                    }
                    buffer = buffer.Substring(buffer.Length - (bufferlength-1));
                }
            }
            return index;
        }
    }
    private static bool bufferIsUnique(string b) {
        for(int i = 0; i < b.Length-1; i++) {
            var sb = b.Substring(i+1);
            if(sb.Contains(b[i]))
                return false;
        }
        return true;
    }
}