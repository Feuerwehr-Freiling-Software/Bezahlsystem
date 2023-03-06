using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
            var username = await localStorage.GetItemAsStringAsync("username");
            if (username == null)
            {
                snackBar.Add("Kein Username?");
                _nav.NavigateTo("");
                return;
            }

            var msg = await http.GetAsync($"https://localhost:7237/api/Auth/Logout/logout/{username}");
            var res = await msg.Content.ReadFromJsonAsync<Errorcode>();

            if (!res.IsSuccessErrorCode)
            {
                snackBar.Add("Fehler beim Ausloggen. Bitte erneut versuchen.", Severity.Error);
                _nav.NavigateTo("");
                return;
            }

            snackBar.Add("Erfolgreich ausgeloggt.", Severity.Success);
            await localStorage.RemoveItemAsync("username");
            await localStorage.RemoveItemAsync("token");
            await localStorage.RemoveItemAsync("refreshToken");
            await provider.GetAuthenticationStateAsync();
            _nav.NavigateTo("");
        }
    }
}
