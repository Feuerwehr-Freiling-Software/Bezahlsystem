﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Components.AddComponents;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages.AdminArea
{
    public partial class CategoryManagement
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public NavigationManager navigation { get; set; }

        public List<ArticleCategoryDto> Categories { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            Categories = await DataService.GetCategories() ?? new ();

        }

        void GoBack()
        {
            navigation.NavigateTo("/");
        }

        async Task AddNewCategory()
        {
            // Open a new Dialog to add a new category
            var opt = new DialogOptions()
            {
                MaxWidth = MaxWidth.Large
            };

            var param = new DialogParameters()
            {
                { "Category", new ArticleCategoryDto() }
            };

            var dialogResult = DialogService.Show<AddCategoryDialog>("Kategorie Hinzufügen", options: opt, parameters: param);
            var result = await dialogResult.Result;

            Snackbar.Add(!result.Canceled ? "Kategorie erfolgreich hinzugefügt." : "Fehler beim Hinzufügen", !result.Canceled ? Severity.Success : Severity.Error);

            await InvokeAsync(StateHasChanged);
        }
    }
}
