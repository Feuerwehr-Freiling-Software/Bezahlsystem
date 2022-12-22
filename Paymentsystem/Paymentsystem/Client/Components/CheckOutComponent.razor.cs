using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Components
{
    public partial class CheckOutComponent
    {
        public CheckOutComponent()
        {

        }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } 

        [Parameter]
        public List<Article> Cart { get; set; } = new List<Article>();

        public double TotalSum { get; set; } = 0;

        private void Cancel()
        {
            MudDialog?.Cancel();
        }

        private void Pay()
        {
            SnackBar.Add("Bezahlt", Severity.Success);
            MudDialog.Close(DialogResult.Ok("success"));
        }

        protected override void OnInitialized()
        {
            SetTotalAmount();
            base.OnInitialized();
        }

        async void AddArticle(Article article)
        {
            var fArt = Cart.FirstOrDefault(x => x.Name == article.Name);
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

                await localStorage.SetItemAsync("Cart", Cart);
            }
            SetTotalAmount();
            await InvokeAsync(StateHasChanged);
        }

        private void SetTotalAmount()
        {
            double totalAmount = 0;

            foreach (var item in Cart)
            {
                totalAmount += item.Amount * item.Price.Amount;
            }

            TotalSum = Math.Round(totalAmount, 1);
        }

        async void RemoveArticle(Article article)
        {
            var fArt = Cart.FirstOrDefault(x => x.Name == article.Name);
            if (fArt != null) 
            {
                if (fArt.Amount > 1) fArt.Amount--;
                else if(fArt.Amount <= 1) Cart.Remove(fArt);
            }

            await localStorage.SetItemAsync("Cart", Cart);
            SetTotalAmount();
            await InvokeAsync(StateHasChanged);
        }
    }
}
