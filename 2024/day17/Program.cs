namespace day17;

public class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        (int[] registers, string program) = Parse(input);
        Console.WriteLine(Run(program, registers));
    }
    public static (int[] reg, string program) Parse(string[] input)
    {
        if (input.Length != 5)
            throw new ArgumentException();

        int[] reg = [
            int.Parse(input[0].Split(": ")[1]),
            int.Parse(input[1].Split(": ")[1]),
            int.Parse(input[2].Split(": ")[1])
        ];
        var program = input[4].Split(": ")[1];

        return (reg, program);
    }

    public static string Run(string programStr, int[] reg)
    {
        var program = programStr.Split(',').Select(int.Parse).ToList();
        int pointer = 0;
        var outputs = new List<int>();
        while (pointer < program.Count - 1)
        {
            pointer = Instruction(program[pointer], program[pointer + 1], reg, pointer, out var output);
            if (output != null)
                outputs.Add((int)output);
        }
        return string.Join(",", outputs);
    }
    /// <summary>
    /// Run 3-bit computer program.
    /// </summary>
    /// <param name="opcode"></param>
    /// <param name="operand"></param>
    /// <param name="reg"></param>
    /// <param name="pointer"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static int Instruction(int opcode, int operand, int[] reg, int pointer, out int? output)
    {
        if (opcode < 0 || opcode > 7 || operand < 0 || operand > 7)
            throw new ArgumentOutOfRangeException();

        // Combo operand, used by opcodes 0, 2, 4, 5, 6 & 7, operand 7 is reserved and not handled
        if (!(opcode == 1 || opcode == 3) && operand > 3 && operand < 7)
            operand = reg[operand - 4];

        pointer += 2;
        output = null;
        switch (opcode)
        {
            case 0:
                {
                    reg[0] = Adv(reg[0], operand);
                    break;
                }
            case 1:
                {
                    //B xor operand
                    reg[1] = reg[1] ^ operand;
                    break;
                }
            case 2:
                {
                    //mod 8
                    reg[1] = operand % 8;
                    break;
                }
            case 3:
                {
                    //jump
                    if (reg[0] != 0)
                    {
                        pointer = operand;
                    }
                    break;
                }
            case 4:
                {
                    //B xor C, operand discarded
                    reg[1] = reg[1] ^ reg[2];
                    break;
                }
            case 5:
                {
                    //out
                    output = operand % 8;
                    break;
                }
            case 6:
                {
                    reg[1] = Adv(reg[0], operand);
                    break;
                }
            case 7:
                {
                    reg[2] = Adv(reg[0], operand);
                    break;
                }
        }
        return pointer;
    }
    public static int Adv(int nom, int operand)
    {
        // x = nom / 2^operand
        int den = 1;
        for(int i = 0; i < operand; i++)
        {
            den *= 2;
        }
        return nom / den;
    }
}
