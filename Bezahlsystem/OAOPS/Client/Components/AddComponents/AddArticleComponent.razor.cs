﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OAOPS.Client.DTO;

namespace OAOPS.Client.Components.AddComponents
{
    public partial class AddArticleComponent
    {
        private StorageDto selectedStorage = new ();
        private StorageSlotDto selectedSlot = new ();

        public AddArticleComponent()
        {

        }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public ISnackbar snackbar { get; set; }

        public List<StorageDto> Storages { get; set; }
        public StorageDto SelectedStorage
        {
            get => selectedStorage; set
            {
                selectedStorage = value;
                LoadSlots(value.StorageName);
            }
        }

        public StorageSlotDto SelectedSlot { get { return selectedSlot; } set { selectedSlot = value; InvokeAsync(StateHasChanged); } }
        public List<StorageSlotDto> Slots { get; set; }

        public ArticleCategoryDto SelectedCategory { get; set; }
        public List<ArticleCategoryDto> Categories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NewArticle = new ArticleDto();
            Storages = await DataService.GetAllStorages() ?? new List<StorageDto>();
            Categories = await DataService.GetAllCategories() ?? new List<ArticleCategoryDto>();
        }

        public ArticleDto NewArticle { get; set; }

        private async Task LoadSlots(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Slots = new List<StorageSlotDto>();
                await InvokeAsync(StateHasChanged);
                return;
            }

            Slots = await DataService.GetSlotsOfStorageByName(name) ?? new List<StorageSlotDto>();
            await InvokeAsync(StateHasChanged);
            return;
        }

        private async Task SelectedStorageChanged()
        {
            await LoadSlots(SelectedStorage.StorageName);
        }

        private async Task AddArticle()
        {
            NewArticle.Category = SelectedCategory.Name;
            NewArticle.StorageName = SelectedStorage.StorageName;
            NewArticle.StorageSlot = SelectedSlot.SlotName;

            ErrorDto? res = await DataService.AddArticle(NewArticle);
            snackbar.Add(res.IsSuccessCode ? $"Artikel {NewArticle} erfolgreich hinzugefügt." : $"Fehler beim updaten des Artikels {NewArticle}: {res.ErrorText}", res.IsSuccessCode ? Severity.Success : Severity.Error);
            MudDialog.Close(DialogResult.Ok(NewArticle.Name));

            await InvokeAsync(StateHasChanged);
        }

        private async Task Cancel()
        {
            MudDialog?.Cancel();
        }

        private async Task OnClearStorageClick()
        {
            SelectedStorage = new StorageDto();
            SelectedSlot = new StorageSlotDto();

            await InvokeAsync(StateHasChanged);
        }

        private async Task OnClearSlotClick()
        {
            SelectedSlot = new StorageSlotDto();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnClearCategoryClick()
        {
            SelectedCategory = new ArticleCategoryDto();
            await InvokeAsync(StateHasChanged);
        }

        private async Task<IEnumerable<StorageDto>> AutocompleteStorageSearch(string input)
        {
            if (input == null)
            {
                return Storages;
            }
            else
            {
                input = input.ToLower();
                return Storages.Where(a => a.StorageName.StartsWith(input));
            }
        }

        private async Task<IEnumerable<StorageSlotDto>> AutocompleteSlotSearch(string input)
        {
            if (input == null)
            {
                return Slots;
            }
            else
            {
                input = input.ToLower();
                return Slots.Where(a => a.SlotName.StartsWith(input));
            }
        }

        private async Task<IEnumerable<ArticleCategoryDto>> AutocompleteCategorySearch(string input)
        {
            if (input == null)
            {
                return Categories;
            }
            else
            {
                input = input.ToLower();
                return Categories.Where(a => a.Name.StartsWith(input));
            }
        }
    }
}
