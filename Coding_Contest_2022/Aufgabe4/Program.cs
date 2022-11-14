int CountCoins()
{
    int count = 0;
    foreach (var item in GlobalVar.Board)
    {
        foreach (var list in item)
        {
            if (list == 'C')
            {
                count++;
            }
        }
    }
    return count;
}

// Function to check whether there
// exists a Hamiltonian Path or not
static bool Hamiltonian_path(char[,] adj, int N)
{
    bool[,] dp = new bool[N, (1 << N)];

    // Set all dp[i][(1 << i)] to
    // true
    for (int i = 0; i < N; i++)
        dp[i, (1 << i)] = true;

    // Iterate over each subset
    // of nodes
    for (int i = 0; i < (1 << N); i++)
    {
        for (int j = 0; j < N; j++)
        {

            // If the jth nodes is included
            // in the current subset
            if ((i & (1 << j)) != 0)
            {

                // Find K, neighbour of j
                // also present in the
                // current subset
                for (int k = 0; k < N; k++)
                {

                    if ((i & (1 << k)) != 0 &&
                        adj[k, j] == 'C' && j != k &&
                        dp[k, i ^ (1 << j)])
                    {

                        // Update dp[j][i]
                        // to true
                        dp[j, i] = true;
                        break;
                    }
                }
            }
        }
    }

    // Traverse the vertices
    for (int i = 0; i < N; i++)
    {
        // Hamiltonian Path exists
        if (dp[i, (1 << N) - 1])
            return true;
    }

    // Otherwise, return false
    return false;
}

for (int x = 1; x <= 5; x++)
{
    var inPath = $"G:\\c#_projekte\\CCC_2022\\4\\level4_{x}.in";
    var outPath = $"G:\\c#_projekte\\CCC_2022\\4\\level4_{x}.out";

    //TEST VALUES
    //var inPath = "G:\\c#_projekte\\CCC_2022\\4\\level4_example.in";
    //var outPath = "G:\\c#_projekte\\CCC_2022\\4\\level4_example.out";

    int coinCount = 0;

    int count = 0;
    int rowcount = 0;

    

    int maxLength = 0;

    string movement = "";

    GlobalVar.Board = new();

    // read input
    var input = File.ReadAllText(inPath);

    input = input.Replace("\r\n", "\n");

    var content = input.Split('\n');

    rowcount = Convert.ToInt32(content[0]);

    int index = 1;

    for (int i = index; i < rowcount; i++)
    {
        index++;
        GlobalVar.Board.Add(new List<char>());

        foreach (var item in content[i])
        {
            GlobalVar.Board[GlobalVar.Board.Count - 1].Add(item);
        }
    }

    index += 1;
    GlobalVar.PacRow = Convert.ToInt32(content[index].Split(' ')[0]) - 1;
    GlobalVar.PacCol = Convert.ToInt32(content[index].Split(' ')[1]) - 1;

    index += 1;

    maxLength = Convert.ToInt32(content[index]);

    for (int i = 0; i < count; i++)
    {
        // pacman movement
        switch (movement[i])
        {
            case 'L':
                GlobalVar.PacCol -= 1;
                break;
            case 'D':
                GlobalVar.PacRow += 1;
                break;
            case 'R':
                GlobalVar.PacCol += 1;
                break;
            case 'U':
                GlobalVar.PacRow -= 1;
                break;
        }

        if (GlobalVar.Board[GlobalVar.PacRow][GlobalVar.PacCol] == 'C')
        {
            coinCount++;
            GlobalVar.Board[GlobalVar.PacRow][GlobalVar.PacCol] = ' ';
        }
    }
}

public class GlobalVar
{
    public static List<List<char>> Board { get; set; }
    public static int PacCol { get; set; }
    public static int PacRow { get; set; }
}