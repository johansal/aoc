internal class Program
{
    private static void Main(string[] args)
    {
        var test = false;
        var tmp = test? "test" : "input";
        var input = File.ReadAllLines(tmp);
        Decrypt d = new(input);
        d.Loop(test);
        Console.WriteLine(d.GetCoordinates(test)); //2117040448 too low
    }
}