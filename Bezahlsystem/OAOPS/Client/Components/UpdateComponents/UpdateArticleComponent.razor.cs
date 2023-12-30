using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.UpdateComponents
{
    public partial class UpdateArticleComponent
    {
        private StorageDto selectedStorage = new();
        private StorageSlotDto selectedSlot = new();

        [Parameter]
        public ArticleDto Article { get; set; }

        public UpdateArticleComponent()
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

        public IBrowserFile? file { get; set; }

        public StorageSlotDto SelectedSlot { get { return selectedSlot; } set { selectedSlot = value; InvokeAsync(StateHasChanged); } }
        public List<StorageSlotDto> Slots { get; set; }

        public ArticleCategoryDto SelectedCategory { get; set; }
        public List<ArticleCategoryDto> Categories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Storages = await DataService.GetAllStorages() ?? new List<StorageDto>();
            SelectedStorage = Storages.FirstOrDefault(s => s.StorageName == Article.StorageName) ?? new();
            Categories = await DataService.GetAllCategories() ?? new List<ArticleCategoryDto>();
            SelectedCategory = Categories.FirstOrDefault(c => c.Name == Article.Category) ?? new();
            await LoadSlots(SelectedStorage.StorageName);
            SelectedSlot = Slots.FirstOrDefault(s => s.SlotName == Article.StorageSlot) ?? new();
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
            Article.Category = SelectedCategory.Name;
            Article.StorageName = SelectedStorage.StorageName;
            Article.StorageSlot = SelectedSlot.SlotName;

            ErrorDto? res = await DataService.UpdateArticle(Article);
            snackbar.Add(res.IsSuccessCode ? $"Artikel {NewArticle} erfolgreich hinzugefügt." : $"Fehler beim updaten des Artikels {Article}: {res.ErrorText}", res.IsSuccessCode ? Severity.Success : Severity.Error);
            MudDialog.Close(DialogResult.Ok(Article.Name));

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

        private async Task OnInputFileChange(InputFileChangeEventArgs args)
        {
            var file = args.File;
            var resizedFile = await file.RequestImageFileAsync(file.ContentType, 640, 480); // resize the image file
            var buf = new byte[resizedFile.Size]; // allocate a buffer to fill with the file's data
            using (var stream = resizedFile.OpenReadStream())
            {
                await stream.ReadAsync(buf); // copy the stream to the buffer
            }

            Article.Base64data = Convert.ToBase64String(buf); // convert to a base64 string!!
        }
    }
}
