// See https://aka.ms/new-console-template for more information
var draw = File.ReadLines("input.txt").First().Split(",").Select(Int32.Parse).ToList();
var boardsRaw = File.ReadAllLines("input.txt").Skip(2).ToList();
var board = new List<int>();
var boards = new List<List<int>>();

for (int i = 0; i < boardsRaw.Count(); i++)
{
    var dataString = boardsRaw[i];
    if (dataString == string.Empty)
    {
        boards.Add(board.ToList());
        board.Clear();
        continue;
    }
    else
    {
        board.AddRange(dataString.Trim().Split().Select(Int32.Parse).ToList());
    }
}

var bingo = new List<Bingo>();

foreach (var num in draw)
{
    for (int i = 0; i < boards.Count; i++)
    {
        for (int n = 0; n < boards[i].Count; n++)
        {
            if (num == boards[i][n])
            {                
                bingo.Insert(i, new BingoItem(n,num)); //cenšos uztaisīt Listu, kur lieku iekšā nosauktos ciparus ar to indexu. Vajadzētu atrast no kuras bingo kartes viņš ir paņemts, tad tajā arī ielikt
            }
        }
    }
    //var items = boards.SelectMany(br=>br).Where(b=>b == num).ToList();

    foreach (var bing in bingo)
    {

    }
}

Console.ReadKey();

public class BingoItem
{    
    public int index;
    public int number;

    public BingoItem( int n, int num)
    {
        index = n;
        number = num;
    }
}

public class Bingo
{
    public int board;
    public BingoItem bingoItem;
    
}