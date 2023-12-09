var input = File.ReadAllLines("inputs/input");
var part1 = 0;
foreach(var line in input)
{
    //parse input to first row of history
    var values = line.Split(" ");
    List<List<int>> history = [];
    List<int> hRow = [];
    for(var i = 0; i < values.Length; i++)
    {
        hRow.Add(int.Parse(values[i]));
    }
    //Console.WriteLine(string.Join(',',hRow));
    history.Add(hRow);

    //find diffs to next rows
    bool allZeros = false;
    while(allZeros == false)
    {
        allZeros = true;
        hRow = [];
        for(var i = 0; i < history[^1].Count - 1; i++)
        {
            var diff = history[^1][i+1] - history[^1][i];
            hRow.Add(diff);
            if(diff != 0)
                allZeros = false;
        }
        //Console.WriteLine(string.Join(',',hRow));
        history.Add(hRow);
    }

    //Add predictions
    for(var i = history.Count - 2; i >= 0; i--)
    {
        history[i].Add(history[i][^1]+history[i+1][^1]);
    }
    //Console.WriteLine(history[0][^1]);
    part1 += history[0][^1];
}
Console.WriteLine(part1);