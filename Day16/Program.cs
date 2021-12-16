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
    var lengTypeId = int.Parse(binarystring[6].ToString());
    var binaryLenght = lengTypeId == 1 ? 11 : 15;
    var packetSum = lengTypeId == 1 ? Convert.ToInt32(binarystring[7..18], 2) : Convert.ToInt32(binarystring[7..22], 2);
    binarystring=binarystring.Remove(0, (7 + binaryLenght));
    for (int i = 0; i < packetSum; i++)
    {
        
    }

}

Console.WriteLine(Convert.ToInt32(packets, 2));
Console.ReadLine();