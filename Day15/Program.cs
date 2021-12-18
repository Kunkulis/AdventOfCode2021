var inputRaw = File.ReadAllLines("input.txt");

var colSize2 = inputRaw[0].Count() * 5;
var rowSize2 = inputRaw.Count() * 5;

var colSize = inputRaw[0].Count();
var rowSize = inputRaw.Count();

int[,] input1 = new int[rowSize, colSize];
int[,] input2 = new int[rowSize2, colSize2];
int[,] unvisited1 = new int[rowSize, colSize];
int[,] unvisited2 = new int[rowSize2, colSize2];

for (int row = 0; row < rowSize2; row++)
{
    for (int col = 0; col < colSize2; col++)
    {
        unvisited2[row, col] = -1;
    }
}

for (int row = 0; row < rowSize; row++)
{
    for (int col = 0; col < colSize; col++)
    {
        input1[row, col] = int.Parse(inputRaw[row].ToCharArray()[col].ToString());
        input2[row, col] = int.Parse(inputRaw[row].ToCharArray()[col].ToString());
        unvisited1[row, col] = -1;
    }
}

for (int row = rowSize; row < rowSize2; row++)
{
    for (int col = 0; col < colSize; col++)
    {
        input2[row, col] = input2[row - rowSize, col] < 9 ? input2[row - rowSize, col] + 1 : 1;
    }
}

for (int row = 0; row < rowSize2; row++)
{
    for (int col = colSize; col < colSize2; col++)
    {
        input2[row, col] = input2[row, (col - colSize)] < 9 ? input2[row, (col - colSize)] + 1 : 1;
    }}



unvisited1[0, 0] = 0;
unvisited2[0, 0] = 0;

unvisited2 = GetPath(0, 0, input2, unvisited2);


Console.WriteLine(unvisited2[rowSize2 - 1, colSize2 - 1]);
Console.ReadLine();

int[,] GetPath(int row, int col, int[,] input, int[,] unvisited)
{
    var nextVal = new { y = 0, x = 0 };
    var IsCorner = false;
    do
    {
        row = nextVal.y;
        col = nextVal.x;


        if (row + 1 < input.GetLength(0))
        {
            if (!(input[row + 1, col] == 0) && (unvisited[row + 1, col] == -1 || unvisited[row + 1, col] > (input[row + 1, col] + unvisited[row, col])))
            {
                unvisited[row + 1, col] = input[row + 1, col] + unvisited[row, col];
            }
        }
        if (row - 1 > 0)
        {
            if (!(input[row - 1, col] == 0) && (unvisited[row - 1, col] == -1 || unvisited[row - 1, col] > (input[row - 1, col] + unvisited[row, col])))
            {
                unvisited[row - 1, col] = input[row - 1, col] + unvisited[row, col];
            }
        }
        if (col + 1 < input.GetLength(1))
        {
            if (!(input[row, col + 1] == 0) && (unvisited[row, col + 1] == -1 || unvisited[row, col + 1] > (input[row, col + 1] + unvisited[row, col])))
            {
                unvisited[row, col + 1] = input[row, col + 1] + unvisited[row, col];
            }
        }
        if (col - 1 > 0)
        {
            if (!(input[row, col - 1] == 0) && (unvisited[row, col - 1] == -1 || unvisited[row, col - 1] > (input[row, col - 1] + unvisited[row, col])))
            {
                unvisited[row, col - 1] = input[row, col - 1] + unvisited[row, col];
            }
        }

        input[row, col] = 0;

        var preNextVal = Enumerable.Range(0, colSize2).SelectMany(y => Enumerable.Range(0, rowSize2).Select(x => new { y, x })).Where(p => input[p.y, p.x] > 0 & unvisited[p.y, p.x] > 0);
        List<(int, (int, int))> listNextVal = new List<(int, (int, int))>();
        foreach (var item in preNextVal)
        {
            var val = unvisited[item.y, item.x];
            listNextVal.Add((val, (item.y, item.x)));
        }
        listNextVal.Sort();
        //nextVal = preNextVal.OrderBy(p => unvisited[p.y, p.x]).FirstOrDefault();
        if (listNextVal.Count > 0)
        {
            nextVal = new { y = listNextVal[0].Item2.Item1, x = listNextVal[0].Item2.Item2 };
        }
        else
        {
            IsCorner = true;
        }

        if (row == rowSize2 - 1 & col == colSize2 - 1)
        {
            IsCorner = true;
        }

    } while (!IsCorner);
    return unvisited2;
}