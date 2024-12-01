var input = File.ReadAllLines("input");
var prioritySum = 0;
var prioritySum2 = 0;
for(int j = 0; j < input.Length; j ++) {
    var rucksack = input[j];
    if(j >= 2 && (j+1) % 3 == 0) {
        var elf1 = input[j].ToList();
        var elf2 = input[j-1].ToList();
        var elf3 = input[j-2].ToList();
        var badge = elf1.Intersect(elf2);
        badge = badge.Intersect(elf3);
        if(badge.Count() != 1)
            throw new Exception(badge.Count() + ", count should be 1");
        var offsetPart2 = Char.IsUpper(badge.First()) ? (int)'A' - 27 : (int)'a' - 1;
        var priorityPart2 = (int)badge.First() - offsetPart2;
        prioritySum2 += priorityPart2;
    }
    var compartmentSize = rucksack.Length / 2;
    for(int i = 0; i < compartmentSize; i++) {
        if(rucksack[compartmentSize..].Contains(rucksack[i]))
        {
            var offset = Char.IsUpper(rucksack[i]) ? (int)'A' - 27 : (int)'a' - 1;
            var priority = (int)rucksack[i] - offset;
            //Console.WriteLine(rucksack[i] + ": " + priority);
            prioritySum += priority;
            break;
        }
    }
}
Console.WriteLine(prioritySum);
Console.WriteLine(prioritySum2);