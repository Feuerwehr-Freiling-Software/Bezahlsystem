using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.DTO;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.UpdateComponents
{
    public partial class UpdateStorageSlot
    {
        [CascadingParameter]
        MudDialogInstance MudDialog {  get; set; }

        [Parameter] 
        public StorageSlotDto Slot { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public List<ArticleDto> Articles { get; set; }

        public List<StorageDto> Storages { get; set; }

        public ArticleDto SelectedArticle { get; set; } = new ArticleDto();

        public StorageDto SelectedStorage { get; set; } = new StorageDto();

        protected override async void OnAfterRender(bool firstRender)
        {
            await LoadStorages();
            await LoadArticles();

            SelectedStorage = new StorageDto() { StorageName = Slot.StorageName };
            SelectedArticle = new ArticleDto() { Name = Slot.ArticleName };

            await InvokeAsync(StateHasChanged);
        }

        private void Cancel()
        {
            MudDialog?.Cancel();
        }

        private async Task LoadArticles()
        {
            Articles = await DataService.GetArticles() ?? new List<ArticleDto>();
        }

        private async Task LoadStorages()
        {
            Storages = await DataService.GetAllStorages() ?? new List<StorageDto>();
        }

        private async Task UpdateSlot()
        {
            Slot.ArticleName = SelectedArticle.Name ?? string.Empty;
            Slot.StorageName = SelectedStorage.StorageName ?? string.Empty;

            var res = await DataService.UpdateStorageSlot(Slot);
            Snackbar.Add(res.IsSuccessCode ? $"Slot {Slot} erfolgreich geupdated." : $"Fehler beim updaten des Slots {Slot}: {res.ErrorText}", res.IsSuccessCode ? Severity.Success : Severity.Error );
            MudDialog.Close(DialogResult.Ok(Slot.SlotId));

            await InvokeAsync(StateHasChanged);
        }

        private void ClearSelectedArticle()
        {
            SelectedArticle = new ArticleDto();
        }

        private async Task<IEnumerable<ArticleDto>> AutocompleteArticleSearch(string input)
        {
            if (input == null)
            {
                return Articles;
            }
            else
            {
                input = input.ToLower();
                return Articles.Where(a => a.Name.StartsWith(input));
            }
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
    }
}
