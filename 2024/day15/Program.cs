using day15;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        Map map = new(1);

        // Parse input: map & movement instructions separated by empty line
        bool isMap = true;
        for(int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if(string.IsNullOrEmpty(line))
            {
                //Print map for test
                map.Print();
                isMap = false;
            }
            else if(isMap)
            {
                for(int j = 0; j < input[0].Length; j++)
                {
                    map.Add(input[i][j], i, j);
                }
            }
            else {
                //Parse instructions on this line
                foreach(var c in line)
                {
                    map.Move(c, (map.Robot.i,map.Robot.j,'@'));
                    //Console.WriteLine($"Move {c}:");
                    //map.Print();
                }
            }
        }
        var part1 = 0;
        foreach(var box in map.Boxes)
        {
            part1 += Gps(box);
        }
        Console.WriteLine(part1);
    }
    private static int Gps((int i, int j) box)
    {
        return 100 * box.i + box.j;
    }
}