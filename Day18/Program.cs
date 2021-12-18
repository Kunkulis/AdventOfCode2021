using System.Text.RegularExpressions;

var inputRaw = File.ReadAllLines("test.txt");
var line = string.Empty;

foreach (var item in inputRaw)
{
    if (line == string.Empty)
    {
        line += item;
    }
    else
    {
        line = "[" + line + "," + item + "]";
        line = Explode();
        line = Splits();
    }

}

string Explode()
{
    bool isNested;
    var pattern = @"\[[0-9],[0-9]\]"; //@"\[(.*?)\]";
    var rgx = new Regex(pattern);
    var countNests=rgx.Matches(line);
    


    isNested = line.Contains("[[[[");
    if (isNested)
    {
        var startIntIndex = line.IndexOfAny("0123456789".ToCharArray());     
        var endIntIndex = line.LastIndexOfAny("0123456789".ToCharArray(), line.IndexOf("[", startIntIndex));

        var firtToExplode = line[startIntIndex];
        var secondToExplode = line[startIntIndex + 2];

        var secondExplodeAddition = int.Parse(secondToExplode.ToString())+int.Parse(line[endIntIndex].ToString());
        line = line.Remove(startIntIndex+2, 1).Insert(startIntIndex+2, secondExplodeAddition.ToString());
        line = line.Remove(startIntIndex, 1).Insert(startIntIndex, "0");
        line = line.Remove(endIntIndex, 3).Remove(0,1);


    }
    throw new NotImplementedException();
}

string Splits()
{
    throw new NotImplementedException();
}