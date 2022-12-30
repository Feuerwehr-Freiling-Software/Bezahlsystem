using Paymentsystem.Shared.Models;
using Paymentsystem.Shared.ViewModels;

namespace Paymentsystem.Server.Hubs
{
    public interface IVendingClient
    {
        Task SendVendingArticlesToClient(List<VendingArticle> vendingArticles);
        Task TestConnection();
    }
}
