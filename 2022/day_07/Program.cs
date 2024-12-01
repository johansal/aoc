internal static class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");

        Dictionary<string, int> fileSystem = new()
        {
            { "/.", 0 }
        };
        List<string> path = new(){"."};

        foreach (var line in input)
        {
            var commandPart = line.Split(' ');
            //is command
            if (commandPart[0].Equals("$"))
            {
                if (commandPart[1].Equals("cd"))
                {
                    //cd
                    if (commandPart[2].Equals(".."))
                    {
                        path.RemoveAt(path.Count - 1);
                    }
                    else if (commandPart[2].Equals("/"))
                    {
                        path = new(){"."};
                    }
                    else
                    {
                        path.Add(commandPart[2]);
                    }
                }
                else if (commandPart[1].Equals("ls"))
                {
                    //list
                }
                else
                {
                    throw new Exception("unknown command!");
                }
            }
            else if (commandPart[0].Equals("dir"))
            {
                //dir
                var clonePath = new List<string>(path)
                {
                    commandPart[1]
                };
                string dirName = GetPath(clonePath);
                if (!fileSystem.ContainsKey(dirName))
                {
                    //add dir to dirstore
                    fileSystem.Add(dirName, 0);
                }
            }
            else
            {
                //is file, add file size to dir sizes
                var clonePath = new List<string>(path);
                while(clonePath.Count > 0) {
                    var subPath = GetPath(clonePath);
                    int value = fileSystem[subPath];
                    value += int.Parse(commandPart[0]);
                    fileSystem[subPath] = value;
                    clonePath.RemoveAt(clonePath.Count - 1);
                }
            }
        }
        int sizeUsed = fileSystem["/."];
        //total size available: 70000000
        //total size to be freed: 30000000
        int needToFree = 30000000 - (70000000 - sizeUsed);
        int smallestToDelete = fileSystem.Where(x => x.Value > needToFree).Min(x => x.Value);
        Console.WriteLine(smallestToDelete);
    }
    public static string GetPath(List<string> path) {
        var p = "";
        foreach(var part in path) {
            p += "/" + part;
        }
        return p;
    }
}