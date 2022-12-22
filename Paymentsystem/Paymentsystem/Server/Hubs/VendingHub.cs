using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Paymentsystem.Shared.ViewModels;
using SignalRSwaggerGen.Attributes;

namespace Paymentsystem.Server.Hubs
{
    [SignalRHub]
    public class VendingHub : Hub<IVendingClient>
    {

        public async Task GetVendingArticles(string connectionId, List<VendingArticle> vendingArticles) => await Clients.Group(connectionId).GetVendingArticles(vendingArticles);

        public async Task TestConnection()
        {            
            await Clients.Group(Context.ConnectionId).TestConnection();
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.ConnectionId);
            // TODO: Implement Update in Db

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
