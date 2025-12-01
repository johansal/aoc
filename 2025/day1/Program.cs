var input = new FileStream("input", FileMode.Open);
var sr = new StreamReader(input);

string? line;;
int dial = 50;
int part1 = 0;
int part2 = 0;
const int totalPositions = 100;

//Console.WriteLine($"Dial starts at {dial}");
while((line = sr.ReadLine()) != null)
{
    var direction = line[..1];
    var fullDistance = int.Parse(line[1..]);
    var distance = fullDistance % totalPositions;

    // add full rotations to part2
    part2 += fullDistance / totalPositions;
    // check if distance makes us pass 0 and add to part2
    if(dial == 0)
    {
        // wont cross 0 if we are there already
    }
    else if(direction == "L" && distance >= dial)
        part2++;
    else if(direction == "R" && distance >= (totalPositions - dial))
        part2++;

    // rotate dial
    if(direction == "L")
        dial -= distance;
    else if(direction == "R")
        dial += distance;
    else
        throw new Exception("Invalid direction");

    // overflow
    if (dial < 0)
        dial += totalPositions;
    else if (dial >= totalPositions)
        dial -= totalPositions;

    if(dial == 0)
    {
        part1++;
    }

    //Console.WriteLine($"Dial is rotated {direction}{distance} to point at {dial}, {part2} 0 crossings so far");
}

Console.WriteLine(part1);
Console.WriteLine(part2);