var inputRaw = File.ReadAllLines("input.txt");
var inputSplit = inputRaw[0].Split(new string[] { "x=", "..", "y=", "target area: ", " ", "," }, StringSplitOptions.RemoveEmptyEntries);

var targetXMin = int.Parse(inputSplit[0]);
var targetXMax = int.Parse(inputSplit[1]);
var targetYMin = int.Parse(inputSplit[2]);
var targetYMax = int.Parse(inputSplit[3]);

int[] probePosition;
int[] probeVelocity;
var yHits = new List<int>();
var allHits = new List<(int x, int y)>();

for (int x = 1; x < 1000; x++)
{
    for (int y = -1000; y < 1000; y++)
    {
        var highestY = 0;
        probePosition = new int[] { 0, 0 };
        probeVelocity = new int[] { x, y };

        while (probePosition[0] <= targetXMax && probePosition[1] >= targetYMin)
        {
            probePosition[0] += probeVelocity[0];
            probePosition[1] += probeVelocity[1];

            if (probeVelocity[0] > 0)
            {
                probeVelocity[0]--;
            }
            probeVelocity[1]--;

            if (highestY<probePosition[1])
            {
                highestY = probePosition[1];
            }

            if ((probePosition[0]>=targetXMin&&probePosition[0]<=targetXMax)&&
                (probePosition[1]>=targetYMin&&probePosition[1]<=targetYMax))
            {
                yHits.Add(highestY);
                allHits.Add((x, y));
            }

        }
    }
}
//File.WriteAllLines(@"E:/day17.csv", ToCsv(allHits));

IEnumerable<string> ToCsv(List<(int x, int y)> allHits)
{
    yield return string.Join(";", allHits.Select(x => x.ToString()).ToArray());
}

Console.WriteLine(allHits.Distinct().ToList().Count);
Console.WriteLine(yHits.Max());
Console.ReadLine();

