// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("test.txt");
var polymer = inputRaw[0].Trim();

var links = new Dictionary<string, string>();

for (int i = 2; i < inputRaw.Length; i++)
{
    var splitInput = inputRaw[i].Split("->");
    links.Add(splitInput[0].Trim(), splitInput[1].Trim());
}

var pairsDic = Enumerable.Range(0, polymer.Length - 1).Select((x => polymer[x..(x + 2)])).GroupBy(x => x).ToDictionary(y => y.Key, y => (long)y.Count());
var charCount = polymer.GroupBy(x => x).ToDictionary(y => y.Key, y => (long)y.Count());

for (int i = 0; i < 40; i++)
{
    pairsDic = Step(pairsDic);       
}

Console.WriteLine(Result());
Console.ReadLine();

Dictionary<string, long> Step(Dictionary<string, long> pairsDic)
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
    return newPairs;
}

object Result()
{
    return charCount.Aggregate((l, r) => l.Value > r.Value ? l : r).Value - charCount.Aggregate((l, r) => l.Value < r.Value ? l : r).Value;
}