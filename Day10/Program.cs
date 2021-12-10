// See https://aka.ms/new-console-template for more information
var input = File.ReadAllLines("input.txt");
var points1 = 0;
var incompleteList = new List<Int64>();

var points = new Dictionary<char, int>()
{
    ['('] = 1,
    ['['] = 2,
    ['{'] = 3,
    ['<'] = 4,
};

for (int x =0; x<input.Length;x++)
{
    var loops = 0;
    while (loops <1)
    {
        loops++;
        for (int i = 0; i < input[x].Length-1; i++)
        {   
            if (input[x][i]== input[x][i+1]-1 || input[x][i] == input[x][i + 1] - 2)
            {
                input[x] = input[x].Remove(i,2);
                loops = 0;
                break;
            }
        }
    }
    var itemDictionary = input[x].GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

    if (itemDictionary.ContainsKey((char)41)|| itemDictionary.ContainsKey((char)62) || itemDictionary.ContainsKey((char)93) || itemDictionary.ContainsKey((char)125))
    {
        var startPoints = points1;
        for (int i = 0; i < input[x].Length; i++)
        {
            points1 += input[x][i] switch
            {
                (char)41 => 3,
                (char)62 => 25137,
                (char)93 => 57,
                (char)125 => 1197,
                _ => 0,
            };
            if (points1 != startPoints) break;
        }
    }
    else
    {
        //var test = input[x].Aggregate()
        
        var result = input[x].Aggregate((0, x) => acc * 5 + points[x], acc);
        Int64 points2 = 0;
        for (int i = input[x].Length-1; i >= 0; i--)
        {
            switch (input[x][i])
            {
                case (char)40:
                    points2 = points2 * 5 + 1;
                    break;
                case (char)60:
                    points2 = points2 * 5 + 4;
                    break;
                case (char)91:
                    points2 = points2 * 5 + 2;
                    break;
                case (char)123:
                    points2=points2 * 5 + 3;
                    break;
                default:
                    points2 += 0;
                    break;
            };
        }
        incompleteList.Add(points2);
    }
}

incompleteList.Sort();
var mid = incompleteList[incompleteList.Count/2];

Console.WriteLine(points1);
Console.WriteLine(mid);
Console.ReadLine();
