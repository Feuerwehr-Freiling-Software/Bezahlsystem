using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace OAOPS.Server.Hubs
{
    [SignalRHub]
    public class VendingHub : Hub
    {
        public IStorageService StorageService { get; set; }

        public VendingHub(IStorageService storageService, ILogger<VendingHub> logger)
        {
            StorageService = storageService;
            Logger = logger;
        }

        public ILogger<VendingHub> Logger { get; set; }

        [SignalRMethod]
        public override Task OnConnectedAsync()
        {
            Logger.LogInformation("###Client Connected###");
            Console.WriteLine("###Client connected###");
            return base.OnConnectedAsync();
        }

        [SignalRMethod]
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("###Client disconnected###");
            return base.OnDisconnectedAsync(exception);
        }

        [SignalRMethod]
        public async Task Connect(string vendingMachineName)
        {
            Logger.LogInformation("###Client sent connection with name: " + vendingMachineName + " ###");
            await Console.Out.WriteLineAsync("###Client sent connection with name: " + vendingMachineName + " ###");
            bool connected = await StorageService.ConnectVendingMachine(vendingMachineName, this.Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.ConnectionId);
            await Clients.Group(Context.ConnectionId).SendAsync("ConnectResult", connected);
        }

        [SignalRMethod]
        public async Task<bool> NewArticleOrdered(int slot, string? username = null)
        {
            bool success = false;
            success = await StorageService.NewArticleOrdered(slot, this.Context.ConnectionId);
            if (username != null)
            {
                // Generate new Payment   
            }
            await Clients.Group(Context.ConnectionId).SendAsync("NewArticleOrdered", success);
            return success;
        }

        [SignalRMethod]
        public async Task Test(string test)
        {
            Logger.LogInformation("Testmethod Called");
            await Clients.All.SendAsync("Test", "Test complete");
        }
    }
}
