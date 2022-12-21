using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Pages
{
    public partial class Products
    {
        public Products()
        {

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter Märzen", Price =  new() { Amount = 1.2} });
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter Ratsherrn", Price = new() { Amount = 1.2 } });
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter OktoberBier", Price = new() { Amount = 1.2 } });
            Articles.Add(new() { Active = true, Amount = 12, Id = 1, IsInVending = false, Name = "Freistädter Bock", Price = new() { Amount = 1.2 } });

        }

        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Article> Cart { get; set; } = new();

        void AddToCart(Article article) 
        {
            InvokeAsync(StateHasChanged);
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
            }
        }

        void RemoveFromCart(Article article)
        {
            var fArt =  Cart.FirstOrDefault(x => x.Name ==article.Name);
            if(fArt != null) fArt.Amount--;
            else Cart.Remove(article);
        }
    }
}
