// See https://aka.ms/new-console-template for more information
var inputRaw = File.ReadAllLines("test.txt");

Dictionary<string, List<string>> input = new Dictionary<string, List<string>>();
var paths = new List<List<string>>();
var path = new List<string>() { "start" };

foreach (string link in inputRaw)
{
    string[] pa = link.Split('-');
    if (!input.ContainsKey(pa[0]))
    {
        input[pa[0]] = new List<string>();
    }
    if (!input.ContainsKey(pa[1]))
    {
        input[pa[1]] = new List<string>();
    }
    input[pa[0]].Add(pa[1]);
    input[pa[1]].Add(pa[0]);
}

GetPaths("start", path,false);
Console.WriteLine(paths.Count);
Console.ReadLine();

void GetPaths(string location, List<string> path, bool revisit)
{
    if (path.Last() == "end")
    {
        paths.Add(path);
        return;
    }
    
    foreach (var child in input[location])
    {
        var isChildUpper = IsAllUpper(child);
        if (!(path.Contains(child) && !isChildUpper))
        {
            var newPath = new List<string>(path) { child};
            GetPaths(child, newPath,revisit);
        }
        else if (path.Contains(child)&& !isChildUpper && child!="start" && !revisit)
        {
            var newPath = new List<string>(path) { child };
            GetPaths(child, newPath, true);
        }
    }
}

bool IsAllUpper(string child) => child.All(c => char.IsUpper(c));
