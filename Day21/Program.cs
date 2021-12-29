var player1 = 4; //4   10
var player2 = 8; //8   9

var dieNum = 1;

var player1Score = 0;
var player2Score = 0;

var throws = 0;
var move = 0;

while (true)
{
    (player1Score, player1) = MakeMove(player1Score, player1);
    
    if (player1Score >= 1000)
    {
        Console.WriteLine(throws * player2Score);
        break;
    }
    (player2Score, player2) = MakeMove(player2Score, player2);

    if (player2Score >= 1000)
    {
        Console.WriteLine(throws * player1Score);
        break;
    }
}

Console.ReadLine();

(int playerScore, int player) MakeMove(int playerScore, int player)
{
    (move, dieNum) = ThreeThrows(dieNum);
    throws += 3;

    var score = int.Parse((player + move).ToString().ToCharArray()[^1].ToString());
    playerScore = score == 0 ? playerScore + 10 : playerScore + score;
    player = score == 0 ? 10 : score;

    return (playerScore, player);
}


(int, int) ThreeThrows(int dieNum)
{
    var sum = 0;
    if (dieNum + 2 <= 100)
    {
        return ((dieNum + (dieNum + 1) + (dieNum + 2)), (dieNum + 3));
    }
    else
    {
        var throw1 = dieNum;
        var throw2 = dieNum + 1;
        var throw3 = dieNum + 2;

        if (throw1 <= 100)
        {
            sum += throw1;
        }
        else
        {
            dieNum = 1;
            return ((dieNum + (dieNum + 1) + (dieNum + 2)), (dieNum + 3));
        }

        if (throw2 <= 100)
        {
            sum += throw2;
        }
        else
        {
            dieNum = 1;
            return ((sum + dieNum + (dieNum + 1)), (dieNum + 2));
        }
        if (throw3 <= 100)
        {
            sum += throw3;
        }
        else
        {
            dieNum = 1;
            return ((sum + dieNum), (dieNum + 1));
        }

        return (sum, (dieNum + 2));
    }
}
