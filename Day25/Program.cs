var inputRaw = File.ReadAllLines("input.txt");
var rows = inputRaw.Count();
var cols = inputRaw[0].Count();

var input = new string[rows, cols];

for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        input[row, col] = inputRaw[row].ToCharArray()[col].ToString();
    }
}

Console.WriteLine(MoveCount());
Console.ReadLine();

int MoveCount()
{
    var moves = 1;
    var loops = 0;
    while (moves != 0)
    {
        moves = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (input[row, col] == ">")
                {
                    (input, moves, col) = DoMoveRight(row, col, moves);
                }
            }
        }

        input = UpdateMoves(input);

        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if (input[row, col] == "v")
                {
                    (input, moves, row) = DoMoveDown(row, col, moves);
                }
            }
        }
        input = UpdateMoves(input);
        loops++;

        if (loops == 2 || loops == 3 || loops == 4)
        {
            //File.WriteAllLines(@$"E:/data{loops}.csv", ToCsv(input));
        }
    }

    return loops;
}

string[,] UpdateMoves(string[,] cleanMap)
{
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            if (cleanMap[row, col] == "#")
            {
                cleanMap[row, col] = ".";
            }
        }
    }
    return cleanMap;
}

(string[,], int, int) DoMoveRight(int row, int col, int moves)
{
    if (col == cols - 1)
    {
        if (input[row, 0] == ".")
        {
            input[row, 0] = ">";
            input[row, col] = "#";
            moves++;
            col++;
        }
        return (input, moves, col);
    }

    if (input[row, col + 1] == ".")
    {
        input[row, col + 1] = ">";
        input[row, col] = "#";
        moves++;
        col++;
    }
    return (input, moves, col);
}

(string[,], int, int) DoMoveDown(int row, int col, int moves)
{
    if (row == rows - 1)
    {
        if (input[0, col] == ".")
        {
            input[0, col] = "v";
            input[row, col] = "#";
            moves++;
            row++;
        }
        return (input, moves, row);
    }

    if (input[row + 1, col] == ".")
    {
        input[row + 1, col] = "v";
        input[row, col] = "#";
        moves++;
        row++;
    }
    return (input, moves, row);
}

static IEnumerable<String> ToCsv<T>(T[,] data, string separator = ";")
{
    for (int i = 0; i < data.GetLength(0); ++i)
        yield return string.Join(separator, Enumerable
          .Range(0, data.GetLength(1))
          .Select(j => data[i, j])); // simplest, we don't expect ',' and '"' in the items
}
