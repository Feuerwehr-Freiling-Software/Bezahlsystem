using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Shared
{
    public partial class AuthenticationButtons
    {
        public AuthenticationButtons()
        {

        }

        void Login()
        {
            _nav.NavigateTo("login");
        }

        async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            await authProvider.GetAuthenticationStateAsync();
        }
    }
}
