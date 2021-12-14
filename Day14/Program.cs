// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt");
var polymer = inputRaw[0].Trim();

var links = new Dictionary<string, string>();

for (int i = 2; i < inputRaw.Length; i++)
{
    var splitInput = inputRaw[i].Split("->");
    links.Add(splitInput[0].Trim(), splitInput[1].Trim());
}

var steps = 0;

var pairs = Enumerable.Range(0, polymer.Length - 1).Select((x => polymer[x..(x + 2)]));
var pairsDic = pairs.GroupBy(x => x).ToDictionary(y => y.Key, y => (long)y.Count());

var charCount = polymer.GroupBy(x => x).ToDictionary(y => y.Key, y => (long)y.Count());

while (steps != 40)
{   
    var newPairs = new Dictionary<string, long>();

    foreach (var item in pairsDic)
    {
        var insert = links[item.Key];
        var newPair1 = item.Key.ToString()[0] + insert;
        var newPair2 = insert + item.Key.ToString()[1];
        if (!charCount.ContainsKey(char.Parse(insert)))
        {
            charCount.Add(char.Parse(insert), 1);
        }
        else
        {
            charCount[char.Parse(insert)] += item.Value;
        }
        if (newPairs.ContainsKey(newPair1))
        {
            newPairs[newPair1] += item.Value;
        }
        else
        {
            newPairs.Add(newPair1, item.Value);
        }
        if (newPairs.ContainsKey(newPair2))
        {
            newPairs[newPair2] += item.Value;
        }
        else
        {
            newPairs.Add(newPair2, item.Value);
        }

    }
    pairsDic = new Dictionary<string, long>(newPairs);
    steps++;
}

var min = charCount.Aggregate((l, r) => l.Value < r.Value ? l : r).Value;
var max = charCount.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;

Console.WriteLine(max-min);
Console.ReadLine();