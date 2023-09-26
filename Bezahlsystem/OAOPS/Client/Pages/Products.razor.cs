using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Components;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages
{
    public partial class Products
    {
        public Products()
        {

        }

        [Inject]
        IDataService DataService { get; set; }

        [Inject]
        public ILocalStorageService localStorage { get; set; }

        List<ArticleDto> Articles;

        public List<ArticleDto> Cart { get; set; }

        public CartIconComponent CartIconComponent { get; set; } = new CartIconComponent();

        protected override async Task OnInitializedAsync()
        {
            Articles = await DataService.GetArticles() ?? new();
            await LoadCart();            
        }

        private async Task PaymentSuccessful()
        {
            Articles = await DataService.GetArticles() ?? new();
            Cart = new();
        }

        private async Task LoadCart()
        {
            var res = await localStorage.GetItemAsync<List<ArticleDto>>("Cart");
            if (res == null)
            {
                Cart = new();
            }
            else
            {
                Cart = res;
            }
            await InvokeAsync(StateHasChanged);
        }

        private async Task AddToCart(string article)
        {
            await LoadCart();
            
            var res = Articles.FirstOrDefault(x => x.Name == article);
            if (res == null) return;

            var fArticle = Cart.FirstOrDefault(x => x.Name == article);
            if (fArticle != null)
            {
                fArticle.Amount++;
            }
            else
            {
                Cart.Add(res);
            }

            await localStorage.SetItemAsync("Cart", Cart);
        }
    }
}
