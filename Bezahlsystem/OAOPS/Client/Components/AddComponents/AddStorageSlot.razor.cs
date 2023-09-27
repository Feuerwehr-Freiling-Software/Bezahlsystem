using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.DTO;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.AddComponents
{
    public partial class AddStorageSlot
    {
        public AddStorageSlot()
        {

        }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public StorageSlotDto NewSlot { get; set; } = new();

        [Parameter]
        public string StorageName { get; set; } = string.Empty;

        void Cancel()
        {
            MudDialog.Cancel();
        }

        async Task AddSlot()
        {
            NewSlot.StorageName = StorageName;
            var res = await DataService.AddStorageSlot(NewSlot);
            if (res.IsSuccessCode == true)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
            else
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
