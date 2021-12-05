// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt");
var xSize = 1000;
var ySize = 1000;

int[,] result = new int[xSize, ySize];
foreach (var row in inputRaw)
{
    var rowSet = row.Trim().Split("->");
    var x1 = int.Parse(rowSet[0].Trim().Split(",")[0]);
    var y1 = int.Parse(rowSet[0].Trim().Split(",")[1]);

    var x2 = int.Parse(rowSet[1].Trim().Split(",")[0]);
    var y2 = int.Parse(rowSet[1].Trim().Split(",")[1]);
    //vertical
    if (x1 == x2)
    {
        var startY = y1 > y2 ? y2 : y1;
        var endY = y1 > y2 ? y1 : y2;
        for (int i = startY; i <= endY; i++)
        {
            result[x1, i] += 1;
        }
    }
    //horizontal
    else if (y1 == y2)
    {
        var startX = x1 > x2 ? x2 : x1;
        var endX = x1 > x2 ? x1 : x2;
        for (int i = startX; i <= endX; i++)
        {
            result[i, y1] += 1;
        }
    }
    //diagonal
    else
    {
        var xDir = x1 > x2 ? -1 : +1;
        var yDir = y1 > y2 ? -1 : +1;

        var loops = Math.Abs(x1 - x2)+1;        

        for (int i = 0; i < loops; i++)        
        {
            result[x1, y1] += 1;

            x1 += xDir;
            y1 += yDir; 
        }
    }
}

var count = 0;
for (int x = 0; x < xSize; x++)
{
    for (int y = 0; y < ySize; y++)
    {
        if (result[x, y] > 1)
        {
            count++;
        }
    }
}
Console.WriteLine(count);
Console.ReadLine();