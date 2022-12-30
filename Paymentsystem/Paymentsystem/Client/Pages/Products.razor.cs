using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using Paymentsystem.Client.Components;
using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Pages
{
    public partial class Products
    {
        public Products()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter Märzen", Price = new() { Amount = 1.2 } });
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter Ratsherrn", Price = new() { Amount = 1.2 } });
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter OktoberBier", Price = new() { Amount = 1.2 } });
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter Bock", Price = new() { Amount = 1.2 } });

            //Articles = await GetArticles();

            Cart = await localStorage.GetItemAsync<List<Article>>("Cart") ?? new List<Article>();
        }

        private async Task<List<Article>> GetArticles()
        {
            return await http.GetFromJsonAsync<List<Article>>("https://localhost:7237/api/Articles/GetAllArticles") ?? new List<Article>();
        }

        public List<Article> Articles { get; set; } = new ();
        public List<Article> Cart { get; set; } = new();

        private async void Checkout()
        {
            var parameters = new DialogParameters { ["Cart"] = Cart };

            var dialog = await DialogService.ShowAsync<CheckOutComponent>("Checkout", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                // TODO: Add payment to Server

                Cart.Clear();
                await localStorage.RemoveItemAsync("Cart");
            }

            await InvokeAsync(StateHasChanged);
        }

        async void AddToCart(Article article) 
        {
            var fArt = Cart.FirstOrDefault(x =>  x.Name == article.Name);
            if (fArt != null) fArt.Amount++;
            else
            {
                var tmpArticle = new Article()
                {
                    Name = article.Name,
                    Amount = 1,
                    Active = article.Active,
                    Id = article.Id,
                    ImageData = article.ImageData,
                    IsInVending = article.IsInVending,
                    Price = article.Price,
                    PriceId = article.PriceId,
                    Type = article.Type,
                    VendingMachineNumber = article.VendingMachineNumber,
                    VendingSlot = article.VendingSlot
                };

                Cart.Add(tmpArticle);

                await InvokeAsync(StateHasChanged);
                await localStorage.SetItemAsync("Cart", Cart);
            }
        }
    }
}
