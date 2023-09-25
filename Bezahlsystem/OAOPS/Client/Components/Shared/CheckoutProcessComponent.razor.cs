using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.Shared
{
    public partial class CheckoutProcessComponent
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public ILocalStorageService localStorage { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IDataService dataService { get; set; }

        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }

        [Parameter]
        public List<ArticleDto> Cart { get; set; } = new();

        public double TotalSum { get; set; } = 0;

        protected override void OnInitialized()
        {
            SetTotalAmount();
        }

        private void Cancel()
        {
            MudDialog?.Cancel();
        }

        private async void Pay()
        {
            var authState = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name;
            if (userName == null)
            {
                Snackbar.Add("Nicht angemeldet", Severity.Error);
                return;
            }

            var res = await dataService.Pay(Cart);
            if (res == null || !res.IsSuccessCode)
            {
                Snackbar.Add("Fehler beim Bezahlen:", Severity.Error);
                MudDialog?.Close(DialogResult.Cancel());
                return;
            }
            else
            {
                Snackbar.Add("Bezahlvorgang erfolgreich", Severity.Success);
                Cart = new();
                await localStorage.SetItemAsync("Cart", Cart);
                SetTotalAmount();
                await InvokeAsync(StateHasChanged);
                MudDialog.Close(DialogResult.Ok(Cart));
                return;
            }
        }

        async void AddArticle(ArticleDto article)
        {
            var fArt = Cart.FirstOrDefault(x => x.Name == article.Name);
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
                totalAmount += item.Amount * item.PriceAmount;
            }

            TotalSum = Math.Round(totalAmount, 1);
        }

        async void RemoveArticle(ArticleDto article)
        {
            var fArt = Cart.FirstOrDefault(x => x.Name == article.Name);
            if (fArt != null)
            {
                if (fArt.Amount > 1) fArt.Amount--;
                else if (fArt.Amount <= 1)
                {
                    Cart.Remove(fArt);
                    await localStorage.SetItemAsync("Cart", Cart);
                }

                if (Cart.Sum(x => x.Amount) < 1)
                {
                    MudDialog?.Cancel();
                }
            }

            await localStorage.SetItemAsync("Cart", Cart);
            SetTotalAmount();
            await InvokeAsync(StateHasChanged);
        }
    }
}
