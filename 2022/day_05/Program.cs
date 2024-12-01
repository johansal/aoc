var input = File.ReadAllLines("input");
List<List<char>> stacks = new();

var tmp = new List<char> {
'Z','V','T','B','J','G','R'
};
stacks.Add(tmp);
tmp = new List<char> {
'L','V','R','J'
};
stacks.Add(tmp);
tmp = new List<char> {
'F','Q','S'
};
stacks.Add(tmp);
tmp = new List<char> {
'G','Q','V','F','L','N','H','Z'
};
stacks.Add(tmp);
tmp = new List<char> {
'W','M','S','C','J','T','Q','R'
};
stacks.Add(tmp);
tmp = new List<char> {
'F','H','C','T','W','S'
};
stacks.Add(tmp);
tmp = new List<char> {
'J','N','F','V','C','Z','D'
};
stacks.Add(tmp);
tmp = new List<char> {
'Q','F','R','W','D','Z','G','L'
};
stacks.Add(tmp);
tmp = new List<char> {
'P','V','W','B','J'
};
stacks.Add(tmp);

foreach(var line in input) {
    if(line.Contains("move")) {
        var instructions = line.Split(' ');
        int count = int.Parse(instructions[1]);
        int from = int.Parse(instructions[3]);
        int to = int.Parse(instructions[5]);
        stacks[to-1].InsertRange(0, stacks[from-1].GetRange(0, count));
        stacks[from-1].RemoveRange(0, count);
    }
}
foreach(var stack in stacks) {
    Console.Write(stack[0]);
}
Console.WriteLine();