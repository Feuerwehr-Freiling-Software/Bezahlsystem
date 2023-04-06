using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
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

        public List<ArticleDto>? Articles { get; set; }

        private void Cancel()
        {
            MudDialog?.Cancel();
        }

        private async Task UpdateSlot()
        {
            var res = await DataService.UpdateStorageSlot(Slot);
            Snackbar.Add(res.IsSuccessCode ? $"Slot {Slot} erfolgreich geupdated." : $"Fehler beim updaten des Slots {Slot}: {res.ErrorText}", res.IsSuccessCode ? Severity.Success : Severity.Error );
            MudDialog.Close(DialogResult.Ok(Slot.SlotId));
        }

        private async Task<IEnumerable<ArticleDto>> AutocompleteSearch(string input)
        {
            Articles ??= await DataService.GetArticles() ?? new List<ArticleDto>();

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
    }
}
