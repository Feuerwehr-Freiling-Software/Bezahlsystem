using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using OAOPS.Client.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages
{
    public partial class Suggestions
    {
        public Suggestions()
        {

        }

        SuggestionDTO suggestion;

        protected override void OnInitialized()
        {
            suggestion = new SuggestionDTO();
        }

        async Task SubmitSuggestion()
        {
            var res = await http.PostAsJsonAsync<SuggestionDTO>("api/Suggestion/AddSuggestion", suggestion);

            snackbar.Add(!res.IsSuccessStatusCode ? "Vorschlag konnte nicht versendet werden" + res.Content : "Vorschlag erfolgreich versendet", !res.IsSuccessStatusCode ? Severity.Error : Severity.Success);
            if (res.IsSuccessStatusCode)
            {
                nav.NavigateTo("/");
            }
            else
            {
                Console.WriteLine("Error: " + res.Content);
            }
        }

        void Back()
        {
            nav.NavigateTo(nav.BaseUri);
        }
    }
}
