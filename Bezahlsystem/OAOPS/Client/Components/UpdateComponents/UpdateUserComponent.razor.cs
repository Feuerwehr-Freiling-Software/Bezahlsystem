using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.UpdateComponents
{
    public partial class UpdateUserComponent
    {
        public UpdateUserComponent()
        {

        }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public UserDto User { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public List<string> Roles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // get all roles
            var tmp = await DataService.GetRoles();
            if (tmp != null)
            {
                Roles = tmp.Select(a => a.Name).ToList();
            }
            else
            {
                Roles = new List<string>();
            }
        }

        void Cancel()
        {
            MudDialog?.Cancel();
        }

        async Task UpdateUser()
        {
            var res = await DataService.UpdateUser(User);
            if (res == null) return;

            if (res.IsSuccessCode)
            {
                Snackbar.Add($"Der Benutzer {User.FirstName} {User.LastName} wurde erfolgreich bearbeitet", Severity.Success);
                MudDialog?.Close(DialogResult.Ok(true));
            }
            else
            {
                Snackbar.Add($"Fehler beim bearbeiten des Benutzers {User.FirstName} {User.LastName}", Severity.Error);
                MudDialog?.Close(DialogResult.Ok(true));
            }

        }

        async Task<IEnumerable<string>> AutocompleteRoleSearch(string input)
        {
            if (input == null)
            {
                return Roles;
            }
            else
            {
                return Roles.Where(a => a.ToLower().StartsWith(input.ToLower())).ToList();
            }
        }
    }
}
