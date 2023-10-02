using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.Shared
{
    public partial class DeleteConfirmationDialog
    {
        public DeleteConfirmationDialog()
        {

        }

        [Parameter]
        public string Name { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        async Task DeleteAsync()
        {
            MudDialog?.Close(DialogResult.Ok(true));
        }

        async Task CancelAsync()
        {
            MudDialog?.Cancel();
        }
    }
}
