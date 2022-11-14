for (int x = 1; x <= 7; x++)
{
    var inPath = $"G:\\c#_projekte\\CCC_2022\\3\\level3_{x}.in";
    var outPath = $"G:\\c#_projekte\\CCC_2022\\3\\level3_{x}.out";

    //TEST VALUES
    //var inPath = "G:\\c#_projekte\\CCC_2022\\3\\level3_example.in";
    //var outPath = "G:\\c#_projekte\\CCC_2022\\3\\level3_example.out";

    int coinCount = 0;

    String line;
    int count = 0;
    int rowcount = 0;

    bool death = false;
    int lastMove = -1;

    int ghostCount = 0;

    int pacCol = -1;
    int pacRow = -1;

    int length = 0;

    string movement = ".";

    List<List<char>> board = new();
    List<Ghost> ghosts = new();

    // read input
    var input = File.ReadAllText(inPath);

    input = input.Replace("G", " ");
    input = input.Replace("\r\n", "\n");

    var content = input.Split('\n');

    rowcount = Convert.ToInt32(content[0]);

    int index = 1;

    for (int i = index; i < rowcount; i++)
    {
        index++;
        board.Add(new List<char>());

        foreach (var item in content[i])
        {
            board[board.Count - 1].Add(item);
        }
    }

    index += 1;
    pacRow = Convert.ToInt32(content[index].Split(' ')[0]) - 1;
    pacCol = Convert.ToInt32(content[index].Split(' ')[1]) - 1;

    index += 1;

    length = Convert.ToInt32(content[index]);

    index += 1;

    movement = content[index];

    index += 1;

    ghostCount = Convert.ToInt32(content[index]);

    index += 1;

    for (int i = index; i < content.Length -2; i += 3)
    {
        var ghost = new Ghost();

        ghost.row = Convert.ToInt32(content[i].Split(" ")[0]) -1;
        ghost.col = Convert.ToInt32(content[i].Split(" ")[1]) -1;

        ghost.moves = Convert.ToInt32(content[i + 1]);
        ghost.movement = content[i + 2];

        ghosts.Add(ghost);
    }

    int longestMovement = ghosts.Max(ghost => ghost.moves);

    if (length > longestMovement)
    {
        longestMovement = length;
    }    

    for (int i = 0; i < longestMovement; i++)
    {
        // pacman movement
        switch (movement[i])
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

        if (board[pacRow][pacCol] == 'W')
        {
            death = true;
            lastMove = i;
            break;
        }

        // ghost movement
        foreach (var item in ghosts)
        {
            switch (item.movement[i])
            {
                case 'L':
                    item.col -= 1;
                    break;
                case 'D':
                    item.row += 1;
                    break;
                case 'R':
                    item.col += 1;
                    break;
                case 'U':
                    item.row -= 1;
                    break;
            }
        }

        foreach (var item in ghosts)
        {
            if (item.row == pacRow && item.col == pacCol)
            {
                death = true;
                lastMove = i;
            }

            if (item.row < 0 || item.col < 0)
            {

            }
        }

        if (death == true)
        {
            break;
        }

        if (board[pacRow][pacCol] == 'C')
        {
            coinCount++;
            board[pacRow][pacCol] = ' ';
        }
    }

    switch (death)
    {
        case true:
            File.WriteAllText(outPath, $"{coinCount} NO");
            break;
        case false:
            File.WriteAllText(outPath, $"{coinCount} YES");
            break;
    }
}

class Ghost
{
    public int col;
    public int row;
    public int moves;
    public string? movement;
}