var input = File.ReadAllLines("input");
List<List<string>> groups = [];
foreach (var line in input)
{
    groups.Add([.. line.Split(" ", StringSplitOptions.RemoveEmptyEntries)]);
}
var columns = groups[0].Count;
var lines = groups.Count;
//Console.WriteLine($"Columns: {columns}, Lines: {lines}");
long? part1 = 0;
for (int i = 0; i < columns; i++)
{
    long? sum = null;
    for (int j = 0; j < lines-1; j++)
    {
        var value = int.Parse(groups[j][i]);
        //Console.Write(value + " ");
        if (groups[lines-1][i] == "+")
        {
            sum ??= 0;
            sum += value;
        }
        else if (groups[lines-1][i] == "*")
        {
            sum ??= 1;
            sum *= value;
        }
        else
        {
            throw new Exception("Unknown operation");
        }
    }
    part1 += sum;
    //Console.WriteLine(sum);
}
Console.WriteLine(part1);

// Read through the input char by char starting from upper right corner until you reach operator
List<int> values = [];
long part2 = 0;
for (int i = input[0].Length - 1; i >= 0; i--)
{
    string valueStr = string.Empty;
    for (int j = 0; j < input.Length; j++)
    {
        if (input[j][i] == ' ')
        {
            continue;
        }
        else if (char.IsDigit(input[j][i]))
        {
            valueStr += input[j][i];
        }
        else
        {
            var v= int.Parse(valueStr);
            values.Add(v);

            if (input[j][i] == '+')
            {
                long sum = 0;
                foreach (var val in values)
                {
                    sum += val;
                }
                part2 += sum;
            }
            else if (input[j][i] == '*')
            {
                long prod = 1;
                foreach (var val in values)
                {
                    prod *= val;
                }
                part2 += prod;
            }
            else
            {
                throw new Exception("Unknown operator");
            }
            valueStr = string.Empty;
            values = [];
            break;
        }
    }
    if (valueStr != string.Empty)
    {
        var value = int.Parse(valueStr);
        values.Add(value);
    }
}
Console.WriteLine(part2);
