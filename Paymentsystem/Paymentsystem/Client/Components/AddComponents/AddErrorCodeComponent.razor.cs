using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Components.AddComponents
{
    public partial class AddErrorCodeComponent
    {
        public AddErrorCodeComponent()
        {

        }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        public ErrorDto Errorcode { get; set; } = new()
        {
            Code = 0,
            ErrorText = string.Empty,
            IsSuccessErrorcode = false
        };

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async void AddError()
        {
            var msg = await http.PostAsJsonAsync("https://localhost:7237/api/ErrorCode/AddError", Errorcode);
            var res = await msg.Content.ReadFromJsonAsync<Errorcode>();
            if (!res.IsSuccessErrorCode)
            {
                Snackbar.Add("Fehler beim Hinzufügen des ErrorCodes", Severity.Error);
                MudDialog.Close(DialogResult.Cancel);
                return;
            }

            Snackbar.Add("Added ErrorCode", Severity.Success);
            MudDialog.Close(DialogResult.Ok(Errorcode.Code));
        }
    }
}
