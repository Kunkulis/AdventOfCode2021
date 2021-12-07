// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt")
    .Select(x => x.Split(",")).ToArray();
var inputList = inputRaw[0].Select(x => int.Parse(x)).ToList();
var fuleLIstPart1 = new List<int>();
var fuleLIstPart2 = new List<int>();

var maxPos = inputList.Max();

for (int j = 0; j < maxPos; j++)
{
    var fule1 = 0;
    var fule2 = 0;
    for (int i = 0; i < inputList.Count; i++)
    {
        var fuleUsedPart1 = Math.Abs(inputList[i] - j);
        var fuleUsedPart2 = (fuleUsedPart1 * (fuleUsedPart1 + 1)) / 2;
        fule1 += fuleUsedPart1;
        fule2 += fuleUsedPart2;
    }
    fuleLIstPart1.Add(fule1);
    fuleLIstPart2.Add(fule2);
}

Console.WriteLine($"Part 1: {fuleLIstPart1.Min()}");
Console.WriteLine($"Part 2: {fuleLIstPart2.Min()}");
Console.ReadLine();
