internal class Program
{
    private static void Main()
    {
        using StreamReader sr = new("input");
        List<char> content = [];
        int id = 0;
        bool isFile = true;
        while (sr.Peek() >= 0)
        {
            var digit = sr.Read() - '0';
            if(isFile)
            {
                content.AddRange(Enumerable.Repeat((char)(id+'0'), digit));
                id++;
                isFile = !isFile;
            }
            else {
                content.AddRange(Enumerable.Repeat('.',digit));
                isFile = !isFile;
            }
        }
        //Console.WriteLine(string.Join("", content));

        int i = 0;
        int j = content.Count - 1;
        while(i < j)
        {
            if(content[i] == '.')
            {
                while(j > i && content[j] == '.')
                {
                    j--;
                }
                if(i >= j)
                {
                    break;
                }
                content[i] = content[j];
                content[j] = '.';
            }
            i++;
        }
        //Console.WriteLine(string.Join("", content));

        long checksum = 0;
        for(i = 0; i < content.Count; i++)
        {
            if(content[i] != '.')
                checksum += i * (content[i] - '0');
        }
        Console.WriteLine(checksum);
    }
}