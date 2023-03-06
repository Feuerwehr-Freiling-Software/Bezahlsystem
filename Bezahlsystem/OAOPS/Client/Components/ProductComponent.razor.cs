namespace Paymentsystem.Client.Components
{
    public partial class ProductComponent : ComponentBase
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
