using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
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
    }
}
