using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using OAOPS.Client.DTO;
using OAOPS.Client.Helpers;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages.BamstisArea
{
    public partial class ManageSuggestions : ComponentBase
    {
        [Inject]
        public IDataService dataService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        public ManageSuggestions()
        {

        }

        private IEnumerable<SuggestionDTO> Elements = new List<SuggestionDTO>();

        protected override async Task OnInitializedAsync()
        {            
            Elements = await dataService.GetAllSuggestions() ?? new List<SuggestionDTO>();
        }

        // events
        void StartedEditingItem(SuggestionDTO item)
        {

        }

        void CanceledEditingItem(SuggestionDTO item)
        {

        }

        async void CommittedItemChanges(SuggestionDTO item)
        {
            var res = await dataService.UpdateSuggestion(item);
            if (res.IsSuccessCode)
            {
                Elements = await dataService.GetAllSuggestions() ?? new List<SuggestionDTO>();
                StateHasChanged();
                Snackbar.Add($"Dringlichkeit des Vorschlags von {item.Username} erfolgreich gespeichert.", Severity.Success);
            }
        }
    }
}
