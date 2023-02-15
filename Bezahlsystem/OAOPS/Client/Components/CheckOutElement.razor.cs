using Blazored.LocalStorage;

namespace Paymentsystem.Client.Components
{
    public partial class CheckOutElement : ComponentBase
    {
        public CheckOutElement()
        {

        }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject] public ILocalStorageService localStorage { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]

        [Parameter]
        public List<ArticleDto> Cart { get; set; } = new();

        public double TotalSum { get; set; } = 0;

        private void Cancel()
        {
            MudDialog?.Cancel();
        }

        private void Pay()
        {
            Snackbar.Add("Bezahlt", Severity.Success);
            MudDialog.Close(DialogResult.Ok("success"));
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
                else if (fArt.Amount <= 1) Cart.Remove(fArt);
            }

            await localStorage.SetItemAsync("Cart", Cart);
            SetTotalAmount();
            await InvokeAsync(StateHasChanged);
        }
    }
}
