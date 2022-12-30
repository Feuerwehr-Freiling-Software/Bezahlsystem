using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Paymentsystem.Shared.Models;
using Paymentsystem.Shared.ViewModels;
using SignalRSwaggerGen.Attributes;

namespace Paymentsystem.Server.Hubs
{
    [SignalRHub]
    public class VendingHub : Hub<IVendingClient>
    {
        public async Task TestConnection()
        {            
            await Clients.Group(Context.ConnectionId).TestConnection();
        }

        public async Task SendArticlesToClient(List<VendingArticle> articles, string vendingConnectionId)
        {
            await Clients.Group(vendingConnectionId).SendVendingArticlesToClient(articles);
        }

        public async Task<ErrorCode> AddToGroup(string vendingId)
        {
            // TODO: Implement Update in Db
            await Groups.AddToGroupAsync(Context.ConnectionId, vendingId);
            return new ErrorCode() { Code = 10, ErrorText = "Added Successful to Group", IsSuccessErrorCode = true, Id = 10};
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // TODO: Implement Update in Db
            // _vendingService.RemoveConnectionId(Context.ConnectionId);
            // _loggerService.LogError();

            return base.OnDisconnectedAsync(exception);
        }
    }
}
