namespace Paymentsystem.Client.Components
{
    public partial class ProductComponent
    {
        public ProductComponent()
        {

        }

        [Parameter]
        public ArticleDto Article { get; set; }
        [Parameter]
        public string Path { get; set; }
    }
}
