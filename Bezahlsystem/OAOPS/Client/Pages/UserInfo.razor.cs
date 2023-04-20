using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages
{
    public partial class UserInfo
    {
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }

        [Inject]
        public ILocalStorageService localStorage { get; set; }

        [Inject]
        public IDataService dataService { get; set; }

        public UserInfo()
        {

        }

        string username;

        protected override async Task OnInitializedAsync()
        {
            var authState = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            username = authState.User.Identity?.Name;
            if (username == null)
            {
                return;
            }
        }
    }
}
