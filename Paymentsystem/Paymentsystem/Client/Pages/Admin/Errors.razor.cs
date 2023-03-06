using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Paymentsystem.Client.Components.AddComponents;
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
            UpdateErrorList();
        }

        async void UpdateErrorList() 
        {
            var res = await httpClient.GetAsync("https://localhost:7237/api/ErrorCode/GetAllErrorCodes");
            if (!res.IsSuccessStatusCode)
            {
                snackbar.Add("Not allowed ", Severity.Error);
                return;
            }

            try
            {
                ErrorList = await res.Content.ReadFromJsonAsync<List<Errorcode>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(await res.Content.ReadAsStringAsync());
                snackbar.Add($"Error code: {ex.Message}", Severity.Error);
            }

            InvokeAsync(StateHasChanged);
        }

        // events
        void StartedEditingItem(Errorcode item)
        {

        }

        void CanceledEditingItem(Errorcode item)
        {

        }

        async void AddErrorCode()
        {
            // Show Dialog
            var dialog = await DialogService.ShowAsync<AddErrorCodeComponent>("Errocode Hinzufügen");
            var result = await dialog.Result;
            UpdateErrorList();
        }

        async void DeleteErrorCode(Errorcode item)
        {
            var res = await httpClient.DeleteAsync($"https://localhost:7237/api/ErrorCode/DeleteError/{item.Id}");
            var error = await res.Content.ReadFromJsonAsync<Errorcode>();

            var text = error?.ErrorText ?? "Wos wüsdn du oida?";

            snackbar.Add(text, !res.IsSuccessStatusCode ? Severity.Error : Severity.Success);

            UpdateErrorList();
        }

        async void CommittedItemChanges(Errorcode item)
        {
            var res = await httpClient.PutAsJsonAsync("https://localhost:7237/api/ErrorCode/UpdateError", item);
            var error = await res.Content.ReadFromJsonAsync<Errorcode>();

            var text = error?.ErrorText ?? "Haha gibts nu ned lol";

            snackbar.Add(error.ErrorText, !res.IsSuccessStatusCode ? Severity.Error : Severity.Success);
            UpdateErrorList();
        }
    }
}
