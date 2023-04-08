using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.AddComponents
{
    public partial class AddCategoryDialog
    {

        [CascadingParameter] 
        MudDialogInstance MudDialog { get; set; }

        
        public ArticleCategoryDto NewCategory { get; set; }

        [Parameter]
        public ArticleCategoryDto Category { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public AddCategoryDialog()
        {

        }

        protected override Task OnInitializedAsync()
        {
            Category = new ArticleCategoryDto();
            return base.OnInitializedAsync();
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task AddCategory()
        {
            // check if Category Name is valid
            Category.Children.Add(NewCategory);
            ErrorDto res = await DataService.UpdateCategory(Category);
            MudDialog.Close(res.IsSuccessCode ? DialogResult.Ok("valid") : DialogResult.Cancel());
        }
    }
}
