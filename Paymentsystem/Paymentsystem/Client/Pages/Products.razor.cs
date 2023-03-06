using MudBlazor;
using Paymentsystem.Client.Components;
using System.Net.Http.Json;


namespace Paymentsystem.Client.Pages
{
    public partial class Products
    {
        public Products()
        {

        }

        protected override async Task OnInitializedAsync()
        {
           //Articles = await GetArticles();

            Cart = await localStorage.GetItemAsync<List<ArticleDto>>("Cart") ?? new List<ArticleDto>();
        }

        private async Task<List<ArticleDto>> GetArticles()
        {
            return await http.GetFromJsonAsync<List<ArticleDto>>("https://localhost:7237/api/Articles/GetAllArticles") ?? new List<ArticleDto>();
        }

        public List<ArticleDto> Articles { get; set; } = new ();
        public List<ArticleDto> Cart { get; set; } = new();

        private async void Checkout()
        {
            var parameters = new DialogParameters { ["Cart"] = Cart };

            var dialog = await DialogService.ShowAsync<CheckOutComponent>("Checkout", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                // TODO: Add payment to Server
                var res = await http.PutAsJsonAsync("https://localhost:7237/api/Purchase/Pay", Cart);
                if (!res.IsSuccessStatusCode) return;
                Cart.Clear();
                await localStorage.RemoveItemAsync("Cart");
            }

            await InvokeAsync(StateHasChanged);
        }

        async void AddToCart(ArticleDto article) 
        {
            var fArt = Cart.FirstOrDefault(x =>  x.Name == article.Name);
            if (fArt != null) fArt.Amount++;
            else
            {
                var tmpArticle = new ArticleDto()
                {
                    Name = article.Name,
                    Amount = 1,
                    PriceAmount = article.PriceAmount
                };

                Cart.Add(tmpArticle);

                await InvokeAsync(StateHasChanged);
                await localStorage.SetItemAsync("Cart", Cart);
            }
        }
    }
}
