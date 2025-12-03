static long TurnOnBatteries(string bank, int batteryCount)
{
    string joltage = string.Empty;
    int start = 0;
    // Iterate to pick the highest value battery after last picked battery and before the remaining batteries are less than needed
    for (int end = bank.Length - (batteryCount - 1); end <= bank.Length; end++)
    {
        start += bank[start..end].IndexOf(bank[start..end].Max());
        joltage += bank[start];
        start++;
    }
    return long.Parse(joltage);
}

var input = File.ReadAllLines("input");
long part1 = 0;
long part2 = 0;
foreach (var line in input)
{
    part1 += TurnOnBatteries(line, 2);
    part2 += TurnOnBatteries(line, 12);
}
Console.WriteLine(part1);
Console.WriteLine(part2);