static bool SequenceRepeatsAtLeastTwice(string value)
{
    var middle = value.Length / 2;
    // Check all possible sequence lengths from 1 to middle (sequence cannot be longer than half the string to reapeat at least twice)
    for (int i = 1; i <= middle; i++)
    {
        var seq = value[0..i];
        var s = 0;
        bool didNotBreak = true;
        // Check the entire string against the sequence
        for (int j = 0; j < value.Length; j++)
        {
            if (value[j] != seq[s])
            {
                // Mismatch found, break out of the loop and try the next sequence length
                didNotBreak = false;
                break;
            }
            // Move to the next character in the sequence, wrapping around if necessary
            s++;
            if (s >= seq.Length)
            {
                s = 0;
            }
        }
        // If we completed the loop without breaking and s is 0, it means the sequence repeated perfectly
        if (didNotBreak && s == 0)
        {
            return true;
        }
    }
    return false;
}

var input = File.ReadAllText("input").Split(',', StringSplitOptions.TrimEntries);
long part1 = 0;
long part2 = 0;

foreach(var rangeStr in input)
{
    var rangeParts = rangeStr.Split('-');
    var start = long.Parse(rangeParts[0]);
    var end = long.Parse(rangeParts[1]);

    var iterator = start;
    while (iterator <= end)
    {
        var iteratorStr = iterator.ToString();
        var middle = iteratorStr.Length / 2;
        if(iteratorStr.Length % 2 == 0 && iteratorStr[0..middle] == iteratorStr[middle..])
        {
            part1 += iterator;
        }        
        if(SequenceRepeatsAtLeastTwice(iteratorStr))
        {
            part2 += iterator;
        }
        iterator++;
    }
}
Console.WriteLine(part1);
Console.WriteLine(part2);