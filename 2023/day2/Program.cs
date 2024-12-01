namespace day2;

public class Day2 {
    public static void Main(string[] args) {
       Part2(); 
    }
    public static void Part1() {
        var input = File.ReadAllLines("inputs/input");

        int result = 0;
        foreach(var line in input) {
            var g = new Game(line);
            if(g.MinReds <= 12 && g.MinBlues <= 14 && g.MinGreens <= 13) 
            {
                result += g.Id;
            }
        }
        Console.WriteLine($"Part1: {result}");
    }
    public static void Part2() {
        var input = File.ReadAllLines("inputs/input");

        int result = 0;
        foreach(var line in input) {
            var g = new Game(line);
            result += g.Power;
        }
        Console.WriteLine($"Part2: {result}");
    }
}

public class Game {
    public int Id {get; set;}
    public int Power {get; set;}
    public int MinReds {get; set;}
    public int MinGreens {get; set;}
    public int MinBlues {get; set;}

    public Game (string line) {
        //init
        MinBlues = 0;
        MinGreens = 0;
        MinReds = 0;

        //Console.WriteLine(line);     
        var tmp = line.Split(": ");
        //parse id
        Id = int.Parse(tmp[0].Split(" ")[1]);
        //parse cubes pulled from bag
        var pulls = tmp[1].Split("; ");
        //set max values for each cube color and pull
        foreach(var pull in pulls)
        {
            var cubes = pull.Split(", ");
            foreach(var cube in cubes)
            {
                //c[0] count and c[1] color
                var c = cube.Split(" ");
                var count = int.Parse(c[0]);
                if(c[1].Equals("green") && count > MinGreens)
                {
                    MinGreens = count;
                }
                else if(c[1].Equals("blue") && count > MinBlues)
                {
                    MinBlues = count;
                }
                else if(c[1].Equals("red") && count > MinReds)
                {
                    MinReds = count;
                }
            }
        }
        Power = MinBlues * MinGreens * MinReds;
    }
}