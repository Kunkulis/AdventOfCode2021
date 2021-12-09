// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt");
var ySize = inputRaw[0].Count();
var xSize = inputRaw.Count();

int[,] input = new int[xSize, ySize];

//placing values
//row
for (int i = 0; i < xSize; i++)
{
    //column
    for (int j = 0; j < ySize; j++)
    {
        input[i, j] = int.Parse(inputRaw[i].ToCharArray()[j].ToString());
    }
}

var pointsArount = new List<int>();
var result = new List<int>();
var floodSize = new HashSet<(int,int)>();
var floodList = new List<int>();
var floodCount = 0;

//find low points
for (int i = 0; i < xSize; i++)
{
    for (int j = 0; j < ySize; j++)
    {
        var point = input[i, j];
        if (!(i - 1 < 0))
            pointsArount.Add(input[i - 1, j]);
        if (!(i + 1 > xSize - 1))
            pointsArount.Add(input[i + 1, j]);
        if (!(j - 1 < 0))
            pointsArount.Add(input[i, j - 1]);
        if (!(j + 1 > ySize - 1))
            pointsArount.Add(input[i, j + 1]);

        //lowPoints
        if (pointsArount.Min() > point && pointsArount.Max() > point)
        {
            floodSize = new HashSet<(int,int)>();
            result.Add(point + 1);
            floodFill(i, j, point, floodCount);
            floodList.Add(floodSize.Count());            
        }
        pointsArount.Clear();
    }
}

var largest = floodList.Max();
var second = floodList.OrderByDescending(z => z).Skip(1).First();
var third = floodList.OrderByDescending(z => z).Skip(2).First();

Console.WriteLine(largest*second*third);
Console.WriteLine(result.Sum());
Console.ReadLine();

void floodFill(int x, int y, int point, int floodCount)
{
    // Base cases
    if (x < 0 || x >= input.GetLength(0) ||
        y < 0 || y >= input.GetLength(1))
        return;

    if (input[x, y] == 9 || input[x, y] < point)
        return ;

    var currentPoint = input[x, y];    
    // Replace the color at (x, y)
    floodSize.Add((x,y));

    // Recur for north, east, south and west
    floodFill(x, y - 1, currentPoint, floodCount);
    floodFill(x + 1, y, currentPoint, floodCount);
    floodFill(x - 1, y, currentPoint, floodCount);
    floodFill(x, y + 1, currentPoint, floodCount);  
    
    return;
}

