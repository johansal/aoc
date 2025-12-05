
using day18_snailfish;

var input = File.ReadAllLines("input");
SnailfishNumber? sum = null!;
foreach (var line in input)
{
    var number = SnailfishParser.Parse(line);
    if (sum == null)
    {
        sum = number;
    }
    else
    {
        sum += number;
    }
}
var part1 = sum.Magnitude();
Console.WriteLine(part1);

var part2 = 0; // max magnitude
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input.Length; j++)
    {
        if (i == j) continue; // skip adding to self
        var num1 = SnailfishParser.Parse(input[i]);
        var num2 = SnailfishParser.Parse(input[j]);
        sum = num1 + num2;
        var magnitude = sum.Magnitude();
        if (magnitude > part2)
        {
            part2 = magnitude;
        }
    }
}
Console.WriteLine(part2);