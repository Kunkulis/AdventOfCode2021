var inputRaw = File.ReadAllLines("input.txt");
var colSize = inputRaw[0].Count();
var rowSize = inputRaw.Count();

int[,] input = new int[rowSize, colSize];
for (int row = 0; row < rowSize; row++)
{
    for (int col = 0; col < colSize; col++)
    {
        input[row, col] = int.Parse(inputRaw[row].ToCharArray()[col].ToString());
    }
}
var parentRelations = new Dictionary<(int, int), List<(int, int)>>();
for (int row = 0; row < rowSize; row++)
{
    for (int col = 0; col < colSize; col++)
    {
        var children = new List<(int, int)>(GetChildren(row, col));
        parentRelations.Add((row, col), children);
    }
}

var visited = new HashSet<(int, int)>();
var pathPoints = new List<int>();
var results = new List<int>();

GetPath(0, 0, pathPoints, visited);
Console.WriteLine(results.Min() - input[0, 0]);
Console.ReadLine();

void GetPath(int row, int col, List<int> pathPoints, HashSet<(int, int)> visited)
{
    if (row == rowSize - 1 & col == colSize - 1)
    {
        pathPoints.Add(input[row, col]);
        results.Add(pathPoints.Sum());
        return;
    }

    if (row < 0 || row >= input.GetLength(0) ||
        col < 0 || col >= input.GetLength(1))
        return;

    if (visited.Contains((row, col)))
        return;

    visited.Add((row, col));
    if (results.Count()<1)
    {
        pathPoints.Add(input[row, col]);
    }
    else if (results.Min() > pathPoints.Sum()+ input[row, col])
    {
        pathPoints.Add(input[row, col]);
    }
    else { return; }

    foreach (var item in parentRelations[(row, col)])
    {
        GetPath(item.Item1, item.Item2, new List<int>(pathPoints), new HashSet<(int, int)>(visited));
    }

    //if (parentRelations[(row, col)].Contains((row - 1, col)))
    //{
    //    GetPath(row - 1, col);
    //}
    //if (parentRelations[(row, col)].Contains((row + 1, col)))
    //{
    //    GetPath(row + 1, col);
    //}
    //if (parentRelations[(row, col)].Contains((row, col - 1)))
    //{
    //    GetPath(row, col - 1);
    //}
    //if (parentRelations[(row, col)].Contains((row, col + 1)))
    //{
    //    GetPath(row, col + 1);
    //}
}
IEnumerable<(int, int)> GetChildren(int row, int col)
{
    var children = new List<(int, int)>();
    var childrenWithPoints = new List<(int, (int, int))>();
    var currentPoint = input[row, col];
    //up
    //if (row - 1 >= 0)
    //{
    //    //if (input[row - 1, col] <= currentPoint)
    //    //{
    //    childrenWithPoints.Add((input[row - 1, col], (row - 1, col)));
    //    //}
    //}
    //down
    if (row + 1 < input.GetLength(0))
    {
        //if (input[row + 1, col] <= currentPoint)
        //{
        childrenWithPoints.Add((input[row + 1, col], (row + 1, col)));
        //}
    }
    //left
    //if (col - 1 >= 0)
    //{
    //    //if (input[row, col - 1] <= currentPoint)
    //    //{
    //    childrenWithPoints.Add((input[row, col - 1], (row, col - 1)));
    //    //}
    //}
    //right
    if (col + 1 < input.GetLength(1))
    {
        //if (input[row, col + 1] <= currentPoint)
        //{
        childrenWithPoints.Add((input[row, col + 1], (row, col + 1)));
        //}
    }
    childrenWithPoints.Sort();
    foreach (var child in childrenWithPoints)
    {
        children.Add(child.Item2);
    }
    return children;
}