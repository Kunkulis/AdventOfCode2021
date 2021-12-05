// See https://aka.ms/new-console-template for more information
var draw = File.ReadLines("input.txt").First().Split(",").Select(Int32.Parse).ToArray();
var boardsRaw = File.ReadAllLines("input.txt").Skip(2).ToList();
int[,] board = new int[5, 5];
var boards = new List<int[,]>();

int row = 0;
for (int i = 0; i < boardsRaw.Count(); i++)
{
    var dataString = boardsRaw[i];
    if (dataString == string.Empty)
    {
        boards.Add(board);
        board = new int[5, 5];
        row = 0;
        continue;
    }
    else
    {
        var intRow = dataString.Trim().Split().Select(n => Convert.ToInt32(n)).ToArray();
        for (int j = 0; j < intRow.Length; j++)
        {
            board[row, j] = intRow[j];
        }
        row++;
    }
}
boards.Add(board);

for (int i = 4; i < draw.Length; i++)
{
    FindWinner(boards, draw[0..i]);
}
Console.ReadKey();

void FindWinner(List<int[,]> boards, int[] draw)
{
    //take each draw and loop through boards
    for (int i = 1; i < boards.Count; i++)
    {
        var board = boards[^i];
        //check coluns
        for (int x = 0; x < 5; x++)
        {
            var count = 0;
            for (int y = 0; y < 5; y++)
            {
                for (int d = 0; d < draw.Length; d++)
                {
                    if (board[x, y] == draw[d])
                    {
                        count++;
                        if (count == 5)
                        {
                            Winner(board, draw);
                        }
                    }
                }
            }
        }

        //check rows
        for (int y = 0; y < 5; y++)
        {
            var count = 0;
            for (int x = 0; x < 5; x++)
            {
                for (int d = 0; d < draw.Length; d++)
                {
                    if (board[x, y] == draw[d])
                    {
                        count++;
                        if (count == 5)
                        {
                            Winner(board, draw);
                        }
                    }
                }
            }
        }
    }
}

void Winner(int[,] board, int[] draw)
{
    int sum = 0;
    //clear board
    for (int x = 0; x < 5; x++)
    {
        for (int y = 0; y < 5; y++)
        {
            for (int d = 0; d < draw.Length; d++)
            {
                if (board[x, y] == draw[d])
                {
                    board[x, y] = 0;
                }
            }
        }
    }

    foreach (var num in board)
    {
        sum += num;
    }
    Console.WriteLine(sum * draw[^1]);
}