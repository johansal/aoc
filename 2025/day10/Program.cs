using System;
using Google.OrTools.Init;
using Google.OrTools.LinearSolver;

IEnumerable<List<T>> GetPermutations<T>(List<T> items, int length)
{
    if (length == 0)
    {
        yield return new List<T>();
        yield break;
    }

    foreach (var item in items)
    {
        foreach (var perm in GetPermutations(items, length - 1))
        {
            perm.Insert(0, item);
            yield return perm;
        }
    }
}

List<int> ToggleIndicatorLights(List<int> current, List<int> button)
{
    var result = new List<int>(current);
    foreach (var index in button)
    {
        if (!result.Remove(index))
        {
            result.Add(index);
        }
    }
    result.Sort();
    return result;
}

int SolveJoltageSystemGoogleORTools(List<List<int>> buttons, List<int> target)
{
    // Create solver, sat backend works for this, CBS did not
    Solver solver = Solver.CreateSolver("SAT_INTEGER_PROGRAMMING");
    if (solver == null)
    {
        throw new Exception("Could not create solver.");
    }

    // Create variables
    var buttonVars = new List<Variable>();
    for (int i = 0; i < buttons.Count; i++)
    {
        buttonVars.Add(solver.MakeIntVar(0.0, double.PositiveInfinity, $"button_{i}"));
    }

    // Create constraints for each target joltage
    for (int j = 0; j < target.Count; j++)
    {
        var constraint = solver.MakeConstraint(target[j], target[j], $"target_{j}");
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].Contains(j))
            {
                constraint.SetCoefficient(buttonVars[i], 1);
            }
        }
    }

    // Objective: minimize total button presses
    var objective = solver.Objective();
    foreach (var var in buttonVars)
    {
        objective.SetCoefficient(var, 1);
    }
    objective.SetMinimization();

    // Solve the system
    var resultStatus = solver.Solve();

    if (resultStatus != Solver.ResultStatus.OPTIMAL)
    {
        throw new Exception("The problem does not have an optimal solution.");
    }

    return (int)objective.Value();
}

var input = File.ReadAllLines("input");
var part1 = 0;
var part2 = 0;
foreach (var line in input)
{
    var parts = line.Split(' ');

    // Parse target indicator light sequence
    List<int> targetLights = [];
    for (int i = 1; i < parts[0].Length - 1; i++)
    {
        if (parts[0][i] == '#')
        {
            targetLights.Add(i-1);
        }
    }

    // Parse target joltage levels
    List<int> targetJoltages = [];
    var joltageStr = parts[^1][1..^1].Split(',');
    foreach (var j in joltageStr)
    {
        targetJoltages.Add(int.Parse(j));
    }

    // Parse buttons
    var buttons = new List<List<int>>();
    for (int i = 1; i < parts.Length - 1; i++)
    {
        var buttonStr = parts[i][1..^1].Split(',');
        var button = new List<int>();
        foreach (var b in buttonStr)
        {
            button.Add(int.Parse(b));
        }
        buttons.Add(button);
    }

    // Try different sequence lengths
    bool foundMinLights = false;
    for (int seqLength = 1; seqLength <= buttons.Count; seqLength++)
    {
        foreach (var sequence in GetPermutations(buttons, seqLength))
        {
            var currentLights = new List<int>();

            foreach (var button in sequence)
            {
                currentLights = ToggleIndicatorLights(currentLights, button);
            }
            
            if (currentLights.SequenceEqual(targetLights))
            {
                part1 += seqLength;
                foundMinLights = true;
                break;
            }

        }
        if (foundMinLights)
        {
            break;
        }
    }

    // Solve joltage system
    var temp = SolveJoltageSystemGoogleORTools(buttons, targetJoltages);
    part2 += temp;
}

Console.WriteLine(part1);
Console.WriteLine(part2);