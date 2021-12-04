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

foreach (var num in draw)
{
    for (int i = 0; i < boards.Count; i++)
    {
        var aBoard = boards[i];
        for (int j = 0; j < aBoard.Length; j++)
        {
            var luckyNum = aBoard[i,j];
            if(luckyNum == num)
            {

            }
        }
    }
}

//foreach (var num in draw)
//{
//    for (int i = 0; i < boards.Count; i++)
//    {
//        for (int n = 0; n < boards[i].Count; n++)
//        {
//            //if (num == boards[i][n])
//            //{                
//            //    //bingo.Insert(i, new BingoItem(n,num)); //cenšos uztaisīt Listu, kur lieku iekšā nosauktos ciparus ar to indexu. Vajadzētu atrast no kuras bingo kartes viņš ir paņemts, tad tajā arī ielikt
//            //}
//        }
//    }
//    //var items = boards.SelectMany(br=>br).Where(b=>b == num).ToList();

//    foreach (var bing in bingo)
//    {

//    }
//}

Console.ReadKey();
