var inputRaw = File.ReadAllLines("test.txt");
var enhanceAlgo = inputRaw[0].ToCharArray();

inputRaw = inputRaw[2..];

var frame = 100;

var inputRows = inputRaw.Count();
var inputCols = inputRaw[0].Count();

var rows = inputRaw.Count() + frame;
var cols = inputRaw[0].Count() + frame;


var image = new string[rows, cols];

for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        if (row <= frame / 2 || col <= frame / 2 || row >= (frame / 2) + inputRows || col >= (frame / 2) + inputCols)
        {
            image[row, col] = ".";
        }
        else
        {
            var rowInRaw = row - (frame / 2)-1;
            var colInRaw = col - (frame / 2)-1;
            image[row, col] = inputRaw[(row - (frame / 2)-1)].ToCharArray()[(col - (frame / 2)-1)].ToString();
        }
    }
}

var binaryOutCome = 0;

for (int loop = 0; loop < 2; loop++)
{
    image = Enhance();
}

Console.WriteLine(image.Cast<string>().Count(c => c == "#"));
Console.ReadLine();

string[,] Enhance()
{
    var afterEnhance = new string[rows,cols];
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
