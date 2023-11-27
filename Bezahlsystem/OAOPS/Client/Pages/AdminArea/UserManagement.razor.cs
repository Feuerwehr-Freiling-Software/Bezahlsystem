using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Components.AddComponents;
using OAOPS.Client.Components.UpdateComponents;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages.AdminArea
{
    public partial class UserManagement
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        public MudDataGrid<UserDto> DataGrid { get; set; } = new();

        public UserDto SelectedUser { get; set; } = new();

        public List<FilterDefinition<UserDto>> FilterDefinitions { get; set; }

        public string currentUsername;

        private string _usernameFilter = string.Empty;
        public string UsernameFilter
        {
            get { return _usernameFilter; }
            set
            {
                _usernameFilter = value;
                DataGrid?.ReloadServerData();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            FilterDefinitions = new List<FilterDefinition<UserDto>>
            {
                new FilterDefinition<UserDto>()
                {
                    FilterFunction = UsernameFilterFunction
                }
            };

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            currentUsername = authState.User.Identity.Name;
        }

        protected bool UsernameFilterFunction(UserDto userDto)
            => string.IsNullOrEmpty(UsernameFilter) || userDto.Username.Contains(UsernameFilter);

        protected string RowClass(UserDto article, int idx)
        {
            StringBuilder classes = new();
            if (article == SelectedUser)
            {
                classes.Append(" mud-info");
            }

            return classes.ToString();
        }

        protected async Task<GridData<UserDto>> LoadData(GridState<UserDto> state)
        {
            List<UserDto> users = await DataService.GetUsersFiltered(username: UsernameFilter, page: state.Page, pageSize: state.PageSize) ?? new();
            users.ForEach(user => { user.Balance = Math.Round(user.Balance, 2); });
            return new GridData<UserDto>()
            {
                Items = users,
                TotalItems = users.Count
            };
        }

        private async void DeactivateUser(UserDto user)
        {
            if (user == null)
            {
                return;
            }
            var res = await DialogService.ShowMessageBox("Deactivate User", $"Are you sure you want to deactivate user {user.Username}?", "Deaktivieren", "Nicht deaktivieren");
            if (res == null) return;

            if (res.Value)
            {
                await DataService.DeactivateUser(user.Username);
                DataGrid?.ReloadServerData();
            }
        }

        private async void UpdateUser(UserDto user)
        {
            if (user == null)
            {
                return;
            }

            var parameter = new DialogParameters() {
            {
                    "User",
                    user
            } };
            
            var opt = new DialogOptions()
            {
                MaxWidth = MaxWidth.Large
            };

            var res = await DialogService.ShowAsync<UpdateUserComponent>("Benutzer Bearbeiten", options: opt, parameters: parameter);
            if (res == null) return;
            
        }

        private async Task AddTopup(UserDto user)
        {
            if (user == null)
            {
                return;
            }

            var parameters = new DialogParameters
            {
                { "User", user },
                { "Executorname", currentUsername },
            };

            var res = await DialogService.ShowAsync<AddTopUpComponent>();
            if (res == null) return;
        }
    }
}
