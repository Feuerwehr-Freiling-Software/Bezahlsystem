namespace Paymentsystem.Client.Components
{
    public partial class CartIconComponent
    {
        public CartIconComponent()
        {

        }

        [Parameter]
        public List<ArticleDto> Cart { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }
        
        private async void Checkout()
        {
            var parameters = new DialogParameters { ["Cart"] = Cart };

            var dialog = await DialogService.ShowAsync(typeof(CheckOutComponent), "Checkout", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // Post zum Server


            }
        }
    }
}
