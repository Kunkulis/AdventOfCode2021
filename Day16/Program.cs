// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("test.txt");
var input = inputRaw[0].ToString();
var firstHalf = input[0..(input.Length / 2)];
var secondHalf = input[(input.Length / 2)..^0];

var binarystringFirst = String.Join(String.Empty,
  firstHalf.Select(
    c => Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2).PadLeft(4, '0')
  )
);
var binarystringSecond = String.Join(String.Empty,
  secondHalf.Select(
    c => Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2).PadLeft(4, '0')
  )
);

var binarystring = binarystringFirst + binarystringSecond;

var version = Convert.ToInt32(binarystring[0..3], 2);
//ID 4 is a literal value, everything eslse an operator
var typeId = Convert.ToInt32(binarystring[3..6], 2);

var packets = string.Empty;

if (typeId == 4)
{
    var start = 6;
    while (binarystring[start] != '0')
    {
        packets += binarystring[(start + 1)..(start + 5)];
        start += 5;
    }
    packets += binarystring[(start + 1)..(start + 5)];
}
else
{
    //1 - then the next 11 bits are a number that represents the number of sub-packets immediately contained by this packet
    //0 - then the next 15 bits are a number that represents the total length in bits of the sub-packets contained by this packet
    var lengTypeId = int.Parse(binarystring[6].ToString());
    if (lengTypeId == 1)
    {
        var binaryLenght = 11;
        var packetSum = Convert.ToInt32(binarystring[7..18], 2);
        binarystring = binarystring.Remove(0, (7 + binaryLenght));
        var subPackIds = new List<(int, int, int)>();
        for (int i = 0; i < packetSum; i++)
        {
            var binary = binarystring.Substring(0, 11);
            subPackIds.Add(SubPackIds(binary));
            binarystring = binarystring.Remove(0, 11);
        }
    }
    else
    {
        var binaryLenght = 15;
        var subLenght = Convert.ToInt32(binarystring[7..22], 2);
        binarystring = binarystring.Remove(0, (7 + binaryLenght));        

        
    }

}

Console.WriteLine(Convert.ToInt32(packets, 2));
Console.ReadLine();

(int, int, int) SubPackIds(string binary)
{
    var ver = Convert.ToInt32(binarystring[0..3], 2);    
    var typeId = Convert.ToInt32(binarystring[3..6], 2);
    var val = Convert.ToInt32(binarystring[6..], 2);

    return (ver, typeId, val);
}