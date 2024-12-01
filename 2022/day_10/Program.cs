internal class Program
{
    private static void Main(string[] args)
    {
        Cpu cpu = new();
        var input = File.ReadAllLines("input");
        foreach(var line in input) {
            var cmd = line.Split(" ");
            if(cmd[0].Equals("noop")) {
                cpu.noop();
            }
            else if(cmd[0].Equals("addx")) {
                cpu.addx(int.Parse(cmd[1]));
            }
        }
        cpu.Crt.Print();
    }
}