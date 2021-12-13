// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt");
var maxX = 0;
var maxY = 0;
GetMaxCoordinate();
var folds = new List<string>();

string[,] input = new string[maxY, maxX];
SetupGrid();
GetFolds();
DoFolds();
var count = input.Cast<string>().Count(x => x == "#");

Console.WriteLine(count);
Console.ReadLine();

void DoFolds()
{
    foreach (var fold in folds)
    {
        var line = int.Parse(fold.Split("=")[1]);
        var axes = fold.Split("=")[0];

        if (axes == "y")
        {
            for (int i = line + 1; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    if (input[i, j] == "#")
                    {
                        var newY = line - (i - line);
                        input[newY, j] = "#";
                        input[i, j] = null;
                    }
                }
            }
        }
        else
        {
            for (int i = line + 1; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    if (input[j, i] == "#")
                    {
                        var newX = line - (i - line);
                        input[j, newX] = "#";
                        input[j, i] = null;
                    }
                }
            }
        }
    }
    ToCsv();
}

void ToCsv()
{
    using (StreamWriter outfile = new StreamWriter(@"E:\output.csv"))
    {
        for (int x = 0; x < maxX; x++)
        {
            string content = "";
            for (int y = 0; y < maxY; y++)
            {
                content += input[y,x]?.ToString() + ";";
            }
            //trying to write data to csv
            outfile.WriteLine(content);
        }
    }
}

void GetFolds()
{
    var emptyString = Array.IndexOf(inputRaw, string.Empty);
    for (int i = emptyString + 1; i < inputRaw.Length; i++)
    {
        folds.Add(inputRaw[i].Split(" ")[^1]);
    }

}
void GetMaxCoordinate()
{
    foreach (var item in inputRaw)
    {
        if (item == string.Empty) break;

        maxX = int.Parse(item.Split(",")[0].Trim()) + 1 > maxX ? int.Parse(item.Split(",")[0].Trim()) + 1 : maxX;
        maxY = int.Parse(item.Split(",")[1].Trim()) + 1 > maxY ? int.Parse(item.Split(",")[1].Trim()) + 1 : maxY;
    }

}
void SetupGrid()
{
    foreach (var item in inputRaw)
    {
        if (item == string.Empty) break;
        var x = int.Parse(item.Split(",")[0].Trim());
        var y = int.Parse(item.Split(",")[1].Trim());

        input[y, x] = "#";
    }

}