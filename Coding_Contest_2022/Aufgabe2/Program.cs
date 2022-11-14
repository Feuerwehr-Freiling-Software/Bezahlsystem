for (int i = 1; i <= 5; i++)
{
    var path = $"G:\\c#_projekte\\CCC_2022\\2\\level1_{i}.in";
    var outPath = $"G:\\c#_projekte\\CCC_2022\\2\\level1_{i}.out";
    var content = File.ReadAllText(path);

    int count = 0;
    foreach (var item in content)
    {
        if (item == 'C')
        {
            count++;
        }
    }
    File.WriteAllText(outPath, count.ToString());
}