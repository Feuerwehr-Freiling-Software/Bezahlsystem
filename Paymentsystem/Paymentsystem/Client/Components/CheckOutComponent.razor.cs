using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Components
{
    public partial class CheckOutComponent
    {
        public CheckOutComponent()
        {

        }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } 

        [Parameter]
        public List<Article> Cart { get; set; } = new List<Article>();

        private void Cancel()
        {
            MudDialog?.Cancel();
        }

        private void Pay()
        {
            SnackBar.Add("Bezahlt", Severity.Success);
            MudDialog.Close(DialogResult.Ok("success"));
        }
    }
}
