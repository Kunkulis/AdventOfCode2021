// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt");
var ySize = inputRaw[0].Count();
var xSize = inputRaw.Count();

int[,] input = new int[xSize, ySize];
var flashedInStep = new HashSet<(int, int)>();
var totalFlashes = 0;
var flashesInStep = 0;

for (int i = 0; i < xSize; i++)
{
    for (int j = 0; j < ySize; j++)
    {
        input[i, j] = int.Parse(inputRaw[i].ToCharArray()[j].ToString());
    }
}

//var steps = 100;
var loop = 0;
while (flashesInStep != 100)
{
    flashedInStep = new HashSet<(int, int)>();
    for (int x = 0; x < xSize; x++)
    {
        for (int y = 0; y < ySize; y++)
        {
            input[x, y] += 1;
        }
    }
    for (int x = 0; x < xSize; x++)
    {
        for (int y = 0; y < ySize; y++)
        {
            if (input[x, y] > 9 && !flashedInStep.Contains((x, y)))
            {
                input[x, y] = 0;
                IncreaseEnergyAroung(x, y);
            }
        }
    }
    loop++;
    totalFlashes += input.Cast<int>().Count(x => x == 0);
    flashesInStep = input.Cast<int>().Count(x => x == 0);
}

Console.WriteLine(totalFlashes);
Console.WriteLine(loop);
Console.ReadLine();

void IncreaseEnergyAroung(int x, int y)
{
    if (x < 0 || x >= input.GetLength(0) ||
        y < 0 || y >= input.GetLength(1))
        return;

    if (flashedInStep.Contains((x, y))) return;

    else if (input[x, y] != 0 && (input[x, y] + 1) <= 9)
    {
        input[x, y] += 1;
        return;
    }

    input[x, y] = 0;
    flashedInStep.Add((x, y));

    // Recur for north, east, south and west
    IncreaseEnergyAroung(x + 1, y);
    IncreaseEnergyAroung(x - 1, y);
    IncreaseEnergyAroung(x, y + 1);
    IncreaseEnergyAroung(x, y - 1);
    //Diagonal
    IncreaseEnergyAroung(x + 1, y - 1);
    IncreaseEnergyAroung(x + 1, y + 1);
    IncreaseEnergyAroung(x - 1, y - 1);
    IncreaseEnergyAroung(x - 1, y + 1);
}