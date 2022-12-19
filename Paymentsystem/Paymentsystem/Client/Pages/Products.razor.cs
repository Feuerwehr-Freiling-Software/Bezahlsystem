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
        }

        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Article> Cart { get; set; } = new();

        void AddToCart(Article article) 
        {
            var fArt = Cart.FirstOrDefault(x =>  x.Name == article.Name);
            if (fArt != null) fArt.Amount++;
            else Cart.Add(article);
        }

        void RemoveFromCart(Article article)
        {
            var fArt =  Cart.FirstOrDefault(x => x.Name ==article.Name);
            if(fArt != null) fArt.Amount--;
            else Cart.Remove(article);
        }
    }
}
