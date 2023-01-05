using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Pages.User
{
    public partial class Logout
    {
        public Logout()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            snackBar.Add("Erfolgreich ausgeloggt.", MudBlazor.Severity.Success);
            await localStorage.RemoveItemAsync("token");
            await localStorage.RemoveItemAsync("refreshToken");
            await provider.GetAuthenticationStateAsync();
            _nav.NavigateTo("");
        }
    }
}
