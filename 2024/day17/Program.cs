namespace day17;

public class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        (long[] registers, string program) = Parse(input);
        Console.WriteLine(Run(program, registers));
        Console.WriteLine(Crack(program, 0, 0));
    }

    // Find register A value that results output to be the program itself.
    // Carefull study of the program reveals that only last 3 bits of A
    // are fiddled by the operands, A is divided by 8 for each new output digit.
    public static long Crack(string program, long cur, int pos)
    {
        //check each 3bit value for reg A,
        for(int i = 0; i < 8; i++)
        {
            long guess = (cur << 3) + i;
            long[] regs = [guess, 0, 0];
            var output = Run(program, regs);
            if(output != program.Substring(program.Length - output.Length))
            {
                //output didn't match no need to find next pos
                continue;
            }
            if(pos == program.Length / 2) //program li
            {
                //output and program match fully, return guessed number
                return guess;
            }
            //we have partial match find next position
            var next = Crack(program, guess, pos + 1);
            if(next > -1)
                return next;
        }
        return -1;
    }
    public static (long[] reg, string program) Parse(string[] input)
    {
        if (input.Length != 5)
            throw new ArgumentException("Wrong number of lines in input.", nameof(input));

        long[] reg = [
            long.Parse(input[0].Split(": ")[1]),
            long.Parse(input[1].Split(": ")[1]),
            long.Parse(input[2].Split(": ")[1])
        ];
        var program = input[4].Split(": ")[1];

        return (reg, program);
    }

    public static string Run(string programStr, long[] reg)
    {
        var program = programStr.Split(',').Select(long.Parse).ToList();
        int pointer = 0;
        var outputs = new List<long>();
        while (pointer < program.Count - 1)
        {
            pointer = Instruction(program[pointer], program[pointer + 1], reg, pointer, out var output);
            if (output != null)
                outputs.Add((long)output);
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
    private static int Instruction(long opcode, long operand, long[] reg, int pointer, out long? output)
    {
        if (opcode < 0 || opcode > 7 || operand < 0 || operand > 7)
            throw new ArgumentOutOfRangeException($"opcode {opcode} or operand {operand} invalid");

        // Combo operand, used by opcodes 0, 2, 4, 5, 6 & 7, operand 7 is reserved and not handled
        if (!(opcode == 1 || opcode == 3) && operand > 3 && operand < 7)
            operand = reg[operand - 4];

        pointer += 2;
        output = null;
        switch (opcode)
        {
            case 0:
                {
                    // x = nom / 2^operand
                    // use bit shift to count 2 power n == 1 << n
                    reg[0] /= 1 << (int)operand;
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
                    //mod 8 (x % Math.Pow(2,n) == x % (1 << n) == x & ((1 << n) - 1))
                    reg[1] = operand & 7;
                    break;
                }
            case 3:
                {
                    //jump
                    if (reg[0] != 0)
                    {
                        pointer = (int)operand;
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
                {;
                    //out: operand % 8 == operand & 7
                    output = operand & 7;
                    break;
                }
            case 6:
                {
                    reg[1] = reg[0]/(1 << (int)operand);
                    break;
                }
            case 7:
                {
                    reg[2] = reg[0]/(1 << (int)operand);
                    break;
                }
        }
        return pointer;
    }
}
