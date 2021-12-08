// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt")
    .Select(x => x.Split("|")).ToArray();
//var inputList = inputRaw[0].Select(x => int.Parse(x)).ToList();
var input = inputRaw.SelectMany(x => x).Select(y => y.Trim().Split()).ToList();

var allNumbers = new List<int>();

for (int i = 1; i < input.Count+1; i+=2)
{
    var check = input[i - 1].Select(x => x).ToHashSet();
    var result = input[i].Select(x => x).ToArray();

    var numMatch = new Dictionary<int, string>()
    {
        { 0,string.Empty }, { 1,string.Empty },{ 2,string.Empty },{ 3,string.Empty },{ 4,string.Empty },{ 5,string.Empty },{ 6,string.Empty },{ 7,string.Empty },{ 8,string.Empty },{ 9,string.Empty }
    };

    numMatch[1] = FindOne(check);
    check.RemoveWhere(x => x == numMatch[1]);
    numMatch[7] = FindSeven(check);
    check.RemoveWhere(x => x == numMatch[7]);
    numMatch[8] = FindEight(check);
    check.RemoveWhere(x => x == numMatch[8]);
    numMatch[4] = FindFour(check);
    check.RemoveWhere(x => x == numMatch[4]);
    numMatch[9] = FindNine(check, numMatch);
    check.RemoveWhere(x => x == numMatch[9]);
    numMatch[0] = FindZero(check, numMatch);
    check.RemoveWhere(x => x == numMatch[0]);
    numMatch[6] = FindSix(check);
    check.RemoveWhere(x => x == numMatch[6]);
    numMatch[3] = FindThree(check, numMatch);
    check.RemoveWhere(x => x == numMatch[3]);
    numMatch[2] = FindTwo(check, numMatch);
    check.RemoveWhere(x => x == numMatch[2]);
    numMatch[5] = FindFive(check);
    check.RemoveWhere(x => x == numMatch[5]);

    var numOutcome = string.Empty;
    foreach (var val in result)
    {

        foreach (var dic in numMatch)
        {
            if (val.Count()==dic.Value.Count() && val.Where(x => dic.Value.Contains(x)).Count()== val.Count())
            {
                numOutcome += dic.Key.ToString();
                break;
            }
        }
    }
    allNumbers.Add(int.Parse(numOutcome));
}

Console.WriteLine(allNumbers.Sum());
Console.ReadKey();


string FindFive(HashSet<string> check)
{
    foreach (var item in check)
    {
        return item;
    }
    return string.Empty;
}

string FindTwo(HashSet<string> check, Dictionary<int, string> numMatch)
{
    var eightChar = numMatch[8].ToCharArray();
    var nineChar = numMatch[9].ToCharArray();

    var uniqe = eightChar.Except(nineChar);

    foreach (var item in check)
    {
        if (item.Where(x => uniqe.Contains(x)).Count() == 1)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindThree(HashSet<string> check, Dictionary<int, string> numMatch)
{
    var oneChar = numMatch[1].ToCharArray();
    foreach (var item in check)
    {
        if (item.Where(x => oneChar.Contains(x)).Count() == 2)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindSix(HashSet<string> check)
{
    foreach (var item in check)
    {
        if (item.Length == 6)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindZero(HashSet<string> check, Dictionary<int, string> numMatch)
{
    var sevenChar = numMatch[7].ToCharArray();
    foreach (var item in check)
    {
        if (item.Length == 6 && item.Where(x => !sevenChar.Contains(x)).Count() == 3)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindNine(HashSet<string> check, Dictionary<int, string> numMatch)
{
    var nineCombo = string.Join(string.Empty, (numMatch[4] + numMatch[7]).ToCharArray()).Distinct();
    foreach (var item in check)
    {
        if (item.Length == 6 && item.Where(x => !nineCombo.Contains(x)).Count() == 1)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindFour(HashSet<string> check)
{
    foreach (var item in check)
    {
        if (item.Length == 4)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindEight(HashSet<string> check)
{
    foreach (var item in check)
    {
        if (item.Length == 7)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindSeven(HashSet<string> check)
{
    foreach (var item in check)
    {
        if (item.Length == 3)
        {
            return item;
        }
    }
    return string.Empty;
}

string FindOne(HashSet<string> check)
{
    foreach (var item in check)
    {
        if (item.Length == 2)
        {
            return item;
        }
    }
    return string.Empty;
}
