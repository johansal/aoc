var input = File.ReadAllLines("input");
List<int> totalCalories = new();
int currentElf = 0;
foreach(var line in input) {
    if(string.IsNullOrEmpty(line)) {
        totalCalories.Add(currentElf);
        currentElf = 0;
    }
    else {
        currentElf += int.Parse(line);
    }
}
totalCalories = totalCalories.OrderDescending().ToList();
Console.WriteLine($"Part 1: {totalCalories[0]}");
Console.WriteLine($"Part 2: {totalCalories.GetRange(0,3).Sum()}");