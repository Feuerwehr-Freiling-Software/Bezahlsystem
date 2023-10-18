using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Test Client Started");

string hubUrl = "https://localhost:7127/hubs/VendingHub";

var connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

connection.On<string>("OrderRecieved", (message) =>  Console.WriteLine(message));
connection.On<string>("Test", (message) => Console.WriteLine(message));

try
{
    await connection.StartAsync();
    Console.WriteLine("Connection started");
    do
    {
        Thread.Sleep(1000);
    } while (connection.State != HubConnectionState.Connected);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// connect
var vendingMachineName = "Automat Werkstatt";
var connectResult = await connection.InvokeAsync<bool>("Connect", vendingMachineName);

int input = -1;

do
{
    Console.WriteLine("0. Programm Schließen");
    Console.WriteLine("1. Send Test to other Clients");
    Console.WriteLine("2. Send Order To Server");

    Console.WriteLine("Input number corresponding with the action:");
    var inputChar = Console.ReadKey(true).KeyChar.ToString();
    var tmpInput = int.TryParse(inputChar, out int result);

    if (tmpInput == false) input = 0;

    switch (result)
    {
        case 0:
            input = 0;
            break;
        case 1:
            await connection.InvokeAsync("Test", "Test Complete");
            break;
        case 2:
            Console.WriteLine(await connection.InvokeAsync<bool>("NewArticleOrdered", 1, "bamsti"));
            Console.WriteLine(await connection.InvokeAsync<bool>("NewArticleOrdered", 1, null));
            break;
        default:
            input = 0;
            break;
    }
}while (input != 0);

Console.WriteLine("Programm wird geschlossen.\n(Beliebige Taste zum Beenden)");
Console.ReadKey();