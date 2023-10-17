using Microsoft.AspNetCore.SignalR;

namespace OAOPS.Server.Hubs
{
    public class VendingHub : Hub
    {

        public IStorageService StorageService { get; set; }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("###Client connected###");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("###Client disconnected###");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task<bool> Connect(string vendingMachineName)
        {
            await Console.Out.WriteLineAsync("###Client sent connection with name: " + vendingMachineName + " ###");
            bool connected = await StorageService.ConnectVendingMachine(vendingMachineName, this.Context.ConnectionId);
            return connected;
        }

        public async Task<bool> NewArticleOrdered(int slot)
        {
            //bool success = await StorageService.NewArticleOrdered(slot);
            return true;
        }
    }
}
