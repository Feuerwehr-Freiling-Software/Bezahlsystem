using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.DTO;
using OAOPS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages.BamstisArea
{
    public partial class ManageSuggestions
    {
        public ManageSuggestions()
        {

        }

        private IEnumerable<SuggestionDTO> Elements = new List<SuggestionDTO>();

        protected override async Task OnInitializedAsync()
        {
            var res = await httpClient.GetFromJsonAsync<List<SuggestionDTO>>("api/suggestion/GetAllSuggestions");
            Elements = res;

            foreach (var item in Enum.GetValues(typeof(Enums.Importance)))
            {
                Console.WriteLine(item);
            }
        }

        // events
        void StartedEditingItem(SuggestionDTO item)
        {

        }

        void CanceledEditingItem(SuggestionDTO item)
        {

        }

        void CommittedItemChanges(SuggestionDTO item)
        {

        }
    }
}
