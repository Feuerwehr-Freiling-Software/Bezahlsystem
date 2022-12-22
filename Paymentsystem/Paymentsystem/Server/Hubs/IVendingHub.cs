using Paymentsystem.Shared.ViewModels;

namespace Paymentsystem.Server.Hubs
{
    public interface IVendingClient
    {
        Task GetVendingArticles(List<VendingArticle> vendingArticles);
        Task TestConnection();
    }
}
