namespace day18_snailfish;
public static class SnailfishParser
{
    public static SnailfishNumber Parse(string line)
    {
        int position = 0;
        return ParseValue(line, ref position);
    }
    private static SnailfishNumber ParseValue(string line, ref int position)
    {
        if (line[position] == '[')
        {
            return ParsePair(line, ref position);
        }
        else
        {
            return ParseRegularNumber(line, ref position);
        }
    }
    private static SnailfishNumber ParsePair(string line, ref int position)
    {
        position++; // skip '['
        var left = ParseValue(line, ref position);
        position++; // skip ','
        var right = ParseValue(line, ref position);
        position++; // skip ']'
        return new SnailfishNumber(left, right);
    }
    private static SnailfishNumber ParseRegularNumber(string line, ref int position)
    {
        var start = position;
        while (char.IsDigit(line[position]))
        {
            position++;
        }
        var number = line[start..position];
        return new SnailfishNumber(int.Parse(number));
    }
}
