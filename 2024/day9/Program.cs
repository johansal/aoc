using day9;

internal class Program
{
    private static void Main()
    {
        string input = "input";

        var part1 = Checksum(DefragmentBlocks(ReadDiskMap(input)));
        Console.WriteLine(part1);

        var part2 = Checksum(DefragmentFiles(ReadDiskMapToBlocks(input)));
        Console.WriteLine(part2);
    }
    private static long Checksum(string content) {
        long checksum = 0;
        for(int i = 0; i < content.Length; i++)
        {
            if(content[i] != '.')
                checksum += i * (content[i] - '0');
        }
        return checksum;
    }
    private static string DefragmentFiles(List<Block> disk)
    {
        for(int i = disk.Count - 1; i >= 0; i--)
        {
            for(int j = 0; j < i; j++)
            {
                if(disk[j].TryWrite(disk[i]) == 1)
                {
                    break;
                }
            }
        }

        return string.Join("", disk.Select(x => x.StrContent()));

    }
    private static string DefragmentBlocks(List<char> content)
    {
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
        return string.Join("",content);
    }
    private static List<char> ReadDiskMap(string fileName)
    {
        using StreamReader sr = new(fileName);
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
            }
            else {
                content.AddRange(Enumerable.Repeat('.',digit));
            }
            isFile = !isFile;
        }
        return content;
    }
    private static List<Block> ReadDiskMapToBlocks(string fileName)
    {
        using (StreamReader sr = new(fileName))
        {
            List<Block> disk = [];
            int id = 0;
            bool isFile = true;
            while (sr.Peek() >= 0)
            {
                var digit = sr.Read() - '0';
                if (digit != 0) //dont add blocks of size 0
                {
                    Block b = new();
                    if(isFile)
                    {    
                        b.Content.AddRange(Enumerable.Repeat((char)(id+'0'), digit));
                        id++;
                    }
                    else {
                        b.Content.AddRange(Enumerable.Repeat('.',digit));
                    }
                    disk.Add(b);
                }
                isFile = !isFile;
            }
            return disk;
        }
    }
}