using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Pages.Admin
{
    public partial class Errors
    {
        public Errors()
        {

        }

        public List<Errorcode> ErrorList { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            ErrorList = await httpClient.GetFromJsonAsync<List<Errorcode>>("https://localhost:7237/api/ErrorCode/GetAllErrorCodes") ?? new();
        }

        // events
        void StartedEditingItem(Errorcode item)
        {

        }

        void CanceledEditingItem(Errorcode item)
        {

        }

        async void CommittedItemChanges(Errorcode item)
        {
            var res = await httpClient.PutAsJsonAsync("https://localhost:7237/api/ErrorCode/UpdateError", item);
            var error = await res.Content.ReadFromJsonAsync<Errorcode>();

            var text = error.ErrorText ?? "Haha gibts nu ned lol";

            snackbar.Add(error.ErrorText, !res.IsSuccessStatusCode ? Severity.Error : Severity.Success);
        }
    }
}
