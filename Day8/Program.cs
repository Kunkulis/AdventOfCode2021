// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("input.txt")
    .Select(x => x.Split("|")).ToArray();
var input = inputRaw.SelectMany(x => x).Select(y => y.Trim().Split()).ToList();
var allNumbers = new List<int>();

for (int i = 1; i < input.Count + 1; i += 2)
{
    var check = input[i - 1].Select(x => x).ToHashSet();
    var result = input[i].Select(x => x).ToArray();

    var numMatch = new Dictionary<int, string>()
    {
        { 0,string.Empty }, { 1,string.Empty },{ 2,string.Empty },{ 3,string.Empty },{ 4,string.Empty },{ 5,string.Empty },{ 6,string.Empty },{ 7,string.Empty },{ 8,string.Empty },{ 9,string.Empty }
    };

    numMatch[1] = FindOne(check);
    check.Remove(numMatch[1]);
    numMatch[7] = FindSeven(check);
    check.Remove(numMatch[7]);
    numMatch[8] = FindEight(check);
    check.Remove(numMatch[8]);
    numMatch[4] = FindFour(check);
    check.Remove(numMatch[4]);
    numMatch[9] = FindNine(check, numMatch);
    check.Remove(numMatch[9]);
    numMatch[0] = FindZero(check, numMatch);
    check.Remove(numMatch[0]);
    numMatch[6] = FindSix(check);
    check.Remove(numMatch[6]);
    numMatch[3] = FindThree(check, numMatch);
    check.Remove(numMatch[3]);
    numMatch[2] = FindTwo(check, numMatch);
    check.Remove(numMatch[2]);
    numMatch[5] = FindFive(check);
    check.Remove( numMatch[5]);

    var numOutcome = string.Empty;
    foreach (var val in result)
    {
        foreach (var dic in numMatch)
        {
            if (val.Count() == dic.Value.Count() && val.Where(x => dic.Value.Contains(x)).Count() == val.Count())
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

string FindNine(HashSet<string> check, Dictionary<int, string> numMatch)
{
    var combo = new HashSet<char>(numMatch[4]);
    combo.UnionWith(numMatch[7]);
    return check.FirstOrDefault(x=>x.Length==6 && x.Where(y=>!combo.Contains(y)).Count()==1)??string.Empty;
}

string FindZero(HashSet<string> check, Dictionary<int, string> numMatch)
{
    var sevenChar = numMatch[7].ToCharArray();
    return check.FirstOrDefault(x=>x.Length==6 && x.Where(y => !sevenChar.Contains(y)).Count() == 3)??string.Empty;   
}

string FindTwo(HashSet<string> check, Dictionary<int, string> numMatch)
{
    var unique =new HashSet<char>(numMatch[8]);
    unique.ExceptWith(numMatch[9]);
    return check.FirstOrDefault(x=>x.Contains(unique.FirstOrDefault()))?? string.Empty;    
}

string FindThree(HashSet<string> check, Dictionary<int, string> numMatch) =>
    check.FirstOrDefault(x => new HashSet<char>(x).IsSupersetOf(new HashSet<char>(numMatch[1]))) ?? string.Empty;

string FindFive(HashSet<string> check) => check.FirstOrDefault() ?? string.Empty;

string FindSix(HashSet<string> check) => check.FirstOrDefault(i => i.Length == 6) ?? string.Empty;

string FindFour(HashSet<string> check) => check.FirstOrDefault(i => i.Length == 4) ?? string.Empty;

string FindEight(HashSet<string> check) => check.FirstOrDefault(i => i.Length == 7) ?? string.Empty;

string FindSeven(HashSet<string> check) => check.FirstOrDefault(i => i.Length == 3) ?? string.Empty;

string FindOne(HashSet<string> check) => check.FirstOrDefault(i => i.Length == 2) ?? string.Empty;
