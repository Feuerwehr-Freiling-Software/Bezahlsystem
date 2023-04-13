using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Components.AddComponents;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.Shared
{
    public partial class CategoryComponent
    {
        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public ArticleCategoryDto ParentCategory { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        public CategoryComponent()
        {

        }

        private async void RemoveCategory(ArticleCategoryDto category)
        {
            // Open a new Dialog to add a new category
            var opt = new DialogOptions()
            {
                MaxWidth = MaxWidth.Large
            };

            bool? result = await DialogService.ShowMessageBox(
                "Kategorie löschen",
                "Achtung! Du bist dabei die Kategorie",
                yesText: "Löschen",
                cancelText: "Cancel"
                );

            if (result != null)
            {
                var res = await DataService.DeleteCategory(category);
                if (res.IsSuccessCode)
                {
                    Snackbar.Add(res.IsSuccessCode ? "Kategorie erfolgreich gelöscht." : $"Fehler beim löschen: {res.ErrorText}", res.IsSuccessCode ? Severity.Success : Severity.Error);
                }
            }

            await InvokeAsync(StateHasChanged);
        }

        private async void AddNewCategory(ArticleCategoryDto category)
        {
            // Open a new Dialog to add a new category
            var opt = new DialogOptions()
            {
                MaxWidth = MaxWidth.Large
            };

            var param = new DialogParameters()
            {
                { "Category", category }
            };

            var dialogResult = DialogService.Show<AddCategoryDialog>("Kategorie Hinzufügen", options: opt, parameters: param);
            var result = await dialogResult.Result;

            Snackbar.Add(!result.Canceled ? "Kategorie erfolgreich hinzugefügt." : "Fehler beim Hinzufügen", !result.Canceled? Severity.Success : Severity.Error);

            await InvokeAsync(StateHasChanged);

        }

    }
}
