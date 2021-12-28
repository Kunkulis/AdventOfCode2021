var player1 = 10;
var player2 = 9;

var dieNum = 1;

var player1Score = 0;
var player2Score = 0;

var throws = 0;
var move = 0;

while (true)
{
    (move, dieNum) = ThreeThrows(dieNum);
    throws+= 3;

    var score = int.Parse((player1 + move).ToString().ToCharArray()[^1].ToString());
    player1Score = score == 0 ? player1Score + 10 : player1Score + score;
    player1 = score == 0 ? 10 : score;
    if (player1Score >= 1000)
    {
        Console.WriteLine(throws * player2Score);
        break;
    }
    (move, dieNum) = ThreeThrows(dieNum);
    throws+= 3;

    score = int.Parse((player2 + move).ToString().ToCharArray()[^1].ToString());
    player2Score = score == 0 ? player2Score + 10 : player2Score + score;
    player2 = score == 0 ? 10 : score;

    if (player2Score >= 1000)
    {
        Console.WriteLine(throws * player1Score);
        break;
    }
}

Console.ReadLine();

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
            return ((sum + dieNum), (dieNum+1));
        }

        return (sum, (dieNum + 2));
    }
}
