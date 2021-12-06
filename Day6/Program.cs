// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt")
    .Select(x => x.Split(",")).ToArray();
var inputList = inputRaw[0].Select(x => int.Parse(x)).ToList();
int days = 256;

var input = inputList.GroupBy(x => x).ToDictionary(g => g.Key, g => (Int64)g.Count());


for (int i = 0; i < days; i++)
{
    input = NextDay(input);
}
Console.WriteLine(input.Sum(x => x.Value));
Console.ReadKey();

Dictionary<int, long> NextDay(Dictionary<int, Int64> input)
{
    var result = new Dictionary<int, Int64>()
    {
        { 0, 0 },{ 1, 0 },{ 2, 0 },{ 3, 0 },{ 4, 0 },{ 5, 0 },{ 6, 0 },{ 7, 0 },{ 8, 0 },
    };

    foreach (var fish in input)
    {
        if (fish.Key == 0 && fish.Value > 0)
        {
            result[6] += fish.Value;
            result[8] += fish.Value;
        }
        else if (fish.Value>0)
        {
            result[fish.Key - 1] += fish.Value;
        }
    }    
    return result;
}


