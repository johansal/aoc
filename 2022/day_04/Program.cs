var input = File.ReadAllLines("input");
int fullOverlap = 0;
int overlaps = 0;
foreach(var line in input) {
    var sectionStr = line.Split(',');
    var section1 = sectionStr[0].Split('-');
    var section2 = sectionStr[1].Split('-');

    var start1 = int.Parse(section1[0]);
    var end1 = int.Parse(section1[1]);
    var start2 = int.Parse(section2[0]);
    var end2 = int.Parse(section2[1]);

    if((start1 <= start2 && end1 >= end2) || (start2 <= start1 && end2 >= end1))
        fullOverlap++;
    if(!((end1 < start2) || (end2 < start1)))
        overlaps++;
}
Console.WriteLine(fullOverlap);
Console.WriteLine(overlaps);