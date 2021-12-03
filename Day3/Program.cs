// See https://aka.ms/new-console-template for more information
var input = File.ReadAllLines("input.txt");
var oxyList = new List<string>(input);
var co2List = new List<string>(input);

int half = input.Count() / 2;
var gamma = string.Empty;
var epsilon = string.Empty;
var oxygen = string.Empty;
var co2 = string.Empty;

for (int i = 0; i < input[0].Length; i++)
{
    int count = input.Count(x => x.ToCharArray()[i].ToString() == "1");
    if (count >= half)
    {
        gamma += "1";
        epsilon += "0";
    }
    else
    {
        gamma += "0";
        epsilon += "1";
    }
}

for (int i = 0; i < input[0].Length; i++)
{
    if (oxyList.Count > 1)
    {
        int count = oxyList.Count(x => x.ToCharArray()[i].ToString() == "1");
        if (count >= (int)Math.Ceiling((double)oxyList.Count() / 2))
        {
            oxyList.RemoveAll(x => x.ToCharArray()[i].ToString() == "0");
        }
        else
        {
            oxyList.RemoveAll(x => x.ToCharArray()[i].ToString() == "1");
        }
    }

    if (co2List.Count > 1)
    {
        int count = co2List.Count(x => x.ToCharArray()[i].ToString() == "1");
        if (count >= (int)Math.Ceiling((double)co2List.Count() / 2))
        {
            co2List.RemoveAll(x => x.ToCharArray()[i].ToString() == "1");
        }
        else
        {
            co2List.RemoveAll(x => x.ToCharArray()[i].ToString() == "0");
        }
    }
}

var decimalGamma = Convert.ToInt32(gamma, 2);
var decimalEpsilon = Convert.ToInt32(epsilon, 2);
var decimalOxygen = Convert.ToInt32(oxyList[0], 2);
var decimalCo2 = Convert.ToInt32(co2List[0], 2);

Console.WriteLine(decimalEpsilon * decimalGamma);
Console.WriteLine(decimalOxygen * decimalCo2);
Console.ReadKey();
