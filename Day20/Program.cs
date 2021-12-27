var inputRaw = File.ReadAllLines("input.txt");
var enhanceAlgo = inputRaw[0].ToCharArray();

inputRaw = inputRaw[2..];

var frame = 1000;

var inputRows = inputRaw.Count();
var inputCols = inputRaw[0].Count();

var rows = inputRows + frame;
var cols = inputCols + frame;


var image = new string[rows, cols];
//var image = new string[inputRows, inputCols];

var maxRow = image.GetUpperBound(0);
var maxCol = image.GetUpperBound(1);
// do top and bottom
for (int col = 0; col < cols; ++col)
{
    for (int i = 0; i < (frame / 2); i++)
    {
        image[i, col] = ".";
        image[maxRow - i, col] = ".";

    }
}
// do left and right
for (int row = 0; row < maxRow; ++row)
{
    for (int i = 0; i < (frame / 2); i++)
    {
        image[row, i] = ".";
        image[row, maxCol - i] = ".";
    }
}

//File.WriteAllLines(@"C:/CP/day20-frame.csv", ToCsv(image));

for (int row = 0; row < inputRows; row++)
{
    for (int col = 0; col < inputCols; col++)
    {
        //if (row <= frame / 2 || col <= frame / 2 || row >= (frame / 2) + inputRows || col >= (frame / 2) + inputCols)
        //{
        //    image[row, col] = ".";
        //}
        //else
        //{
        //var rowInRaw = row - (frame / 2)-1;
        //var colInRaw = col - (frame / 2)-1;
        //image[row, col] = inputRaw[(row - (frame / 2)-1)].ToCharArray()[(col - (frame / 2)-1)].ToString();
        image[(row + frame / 2), (col + frame / 2)] = inputRaw[row].ToCharArray()[col].ToString();
        //}
    }
}

//File.WriteAllLines(@"C:/CP/day20-full.csv", ToCsv(image));

static IEnumerable<String> ToCsv<T>(T[,] data, string separator = ";")
{
    for (int i = 0; i < data.GetLength(0); ++i)
        yield return string.Join(separator, Enumerable
          .Range(0, data.GetLength(1))
          .Select(j => data[i, j])); // simplest, we don't expect ',' and '"' in the items
}

var binaryOutCome = 0;

for (int loop = 0; loop < 50; loop++)
{
    image = Enhance();
}
File.WriteAllLines(@$"C:/CP/day20.csv", ToCsv(image));

Console.WriteLine(image.Cast<string>().Count(c => c == "#"));
Console.ReadLine();

string[,] Enhance()
{
    var afterEnhance = new string[rows, cols];
    //var afterEnhance = new string[inputRows, inputCols];
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            string binaryRaw = string.Empty;
            binaryRaw += GetBinary(row - 1, col - 1, binaryRaw);
            binaryRaw += GetBinary(row - 1, col, binaryRaw);
            binaryRaw += GetBinary(row - 1, col + 1, binaryRaw);

            binaryRaw += GetBinary(row, col - 1, binaryRaw);
            binaryRaw += GetBinary(row, col, binaryRaw);
            binaryRaw += GetBinary(row, col + 1, binaryRaw);

            binaryRaw += GetBinary(row + 1, col - 1, binaryRaw);
            binaryRaw += GetBinary(row + 1, col, binaryRaw);
            binaryRaw += GetBinary(row + 1, col + 1, binaryRaw);

            binaryOutCome = Convert.ToInt32(binaryRaw, 2);
            var enhancerOutCome = enhanceAlgo[binaryOutCome];

            afterEnhance[row, col] = enhanceAlgo[binaryOutCome].ToString();
        }
    }
    return afterEnhance;
}


object GetBinary(int row, int col, string returnString)
{
    // Base cases
    if (row < 0 || row >= image.GetLength(0) ||
        col < 0 || col >= image.GetLength(1))
        return "0";
    else
    {
        return image[row, col] == "." ? "0" : "1";
    }
}
