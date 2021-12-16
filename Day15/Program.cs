var inputRaw = File.ReadAllLines("test.txt");
var colSize = inputRaw[0].Count();
var rowSize = inputRaw.Count();

int[,] input = new int[rowSize, colSize];
int[,] unvisited = new int[rowSize, colSize];

for (int row = 0; row < rowSize; row++)
{
    for (int col = 0; col < colSize; col++)
    {
        input[row, col] = int.Parse(inputRaw[row].ToCharArray()[col].ToString());
        unvisited[row, col] = -1;
    }
}
unvisited[0, 0] = 0;
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

GetPath(0, 0);
//File.WriteAllLines(@"C:/CP/data.csv", ToCsv(unvisited));

static IEnumerable<String> ToCsv<T>(T[,] data, string separator = ";")
{
    for (int i = 0; i < data.GetLength(0); ++i)
        yield return string.Join(separator, Enumerable
          .Range(0, data.GetLength(1))
          .Select(j => data[i, j])); // simplest, we don't expect ',' and '"' in the items
}

Console.WriteLine(results.Min() - input[0, 0]);
Console.ReadLine();

void GetPath(int row, int col)
{
    var nextVal = new { y = 0, x = 0 };
    var IsCorner = false;
    do
    {
        row = nextVal.y;
        col = nextVal.x;

        if ((row == 99 & col == 99))// || (neighbour.Item1 == 99 & neighbour.Item2 == 99))
        {

        }
        if (row + 1 < input.GetLength(0))
        {
            if (!(input[row + 1, col] == 0) && unvisited[row + 1, col] == -1 || unvisited[row + 1, col] > input[row + 1, col] + unvisited[row, col])
            {
                unvisited[row + 1, col] = input[row + 1, col] + unvisited[row, col];
            }
        }
        if (col + 1 < input.GetLength(1))
        {
            if (!(input[row, col + 1] == 0) && unvisited[row, col + 1] == -1 || unvisited[row, col + 1] > input[row, col + 1] + unvisited[row, col])
            {
                unvisited[row, col + 1] = input[row, col + 1] + unvisited[row, col];
            }
        }

        //foreach (var neighbour in parentRelations[(row, col)])
        //{
        //    if ((row == 99 & col == 99)||(neighbour.Item1==99&neighbour.Item2==99))
        //    {

        //    }
        //    if (input[neighbour.Item1, neighbour.Item2] == 0)
        //    {
        //        continue;
        //    }
        //    if (unvisited[neighbour.Item1, neighbour.Item2] == -1 || unvisited[neighbour.Item1, neighbour.Item2] > input[neighbour.Item1, neighbour.Item2] + unvisited[row, col])
        //    {
        //        unvisited[neighbour.Item1, neighbour.Item2] = input[neighbour.Item1, neighbour.Item2] + unvisited[row, col];
        //    }
        //}

        input[row, col] = 0;

        var preNextVal = Enumerable.Range(0, colSize).SelectMany(y => Enumerable.Range(0, rowSize).Select(x => new { y, x })).Where(p => input[p.y, p.x] > 0 & unvisited[p.y, p.x] > 0);
        List<(int, (int, int))> listNextVal = new List<(int, (int, int))>();
        foreach (var item in preNextVal)
        {
            var val = unvisited[item.y, item.x];
            listNextVal.Add((val, (item.y, item.x)));
        }
        listNextVal.Sort();
        //nextVal = preNextVal.OrderBy(p => unvisited[p.y, p.x]).FirstOrDefault();
        nextVal = new { y=listNextVal[0].Item2.Item1, x=listNextVal[0].Item2.Item2};
        if (row == 99 & col == 99)
        {
            IsCorner = true;
        }

    } while (!IsCorner);

}


IEnumerable<(int, int)> GetChildren(int row, int col)
{
    var children = new List<(int, int)>();

    //down
    if (row + 1 < input.GetLength(0))
    {
        children.Add((row + 1, col));
    }

    //right
    if (col + 1 < input.GetLength(1))
    {
        children.Add((row, col + 1));
    }
    return children;
}