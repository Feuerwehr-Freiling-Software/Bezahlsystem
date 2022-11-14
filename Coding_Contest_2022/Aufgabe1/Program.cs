for (int i = 1; i <= 5; i++)
{
    var inPath = $"G:\\c#_projekte\\CCC_2022\\2\\level2_{i}.in";
    var outPath = $"G:\\c#_projekte\\CCC_2022\\2\\level2_{i}.out";

    //TEST VALUES
    //var inPath = "G:\\c#_projekte\\CCC_2022\\2\\level2_example.in";
    //var outPath = "G:\\c#_projekte\\CCC_2022\\2\\level2_example.out";

    int coinCount = 0;

    String line;
    int count = 0;
    int rowcount = 0;

    int pacCol = -1;
    int pacRow = -1;

    int length = 0;

    string movement = ".";

    List<List<char>> board = new();
    using (StreamReader sr = new StreamReader(inPath))
    {
        while ((line = sr.ReadLine()) != null)
        {
            if (board.Count < rowcount)
            {
                board.Add(new List<char>());

                foreach (var item in line)
                {
                    board[board.Count - 1].Add(item);
                }
            }

            count++;
            if (count == 1)
            {
                rowcount = Convert.ToInt32(line);
            }            

            if (rowcount == board.Count)
            {
                if (movement == "")
                {
                    movement = line;
                }

                if (length == -1)
                {
                    length = Convert.ToInt32(line);
                    movement = "";
                }

                if (pacCol == -1 && line[0] != 'W')
                {
                    pacRow = Convert.ToInt32(line.Split(' ')[0]) - 1;
                    pacCol = Convert.ToInt32(line.Split(' ')[1]) - 1;

                    length = -1;
                }                
            }
        }
    }

    foreach (var item in movement)
    {
        switch (item)
        {
            case 'L':
                pacCol -= 1;
                break;
            case 'D':
                pacRow += 1;
                break;
            case 'R':
                pacCol += 1;
                break;
            case 'U':
                pacRow -= 1;
                break;
        }

        if (board[pacRow][pacCol] == 'C')
        {
            coinCount++;
            board[pacRow][pacCol] = ' ';
        }
    }
    File.WriteAllText(outPath, coinCount.ToString());
}