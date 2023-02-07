using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.DTO;
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
        private bool _readOnly;
        private bool _isCellEditMode;
        private List<string> _events = new();
        private bool _editTriggerRowClick;

        protected override async Task OnInitializedAsync()
        {
            var res = await httpClient.GetFromJsonAsync<List<SuggestionDTO>>("api/suggestion/GetAllSuggestions");
            Elements = res;
        }

        // events
        void StartedEditingItem(SuggestionDTO item)
        {
            _events.Insert(0, $"Event = StartedEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        void CanceledEditingItem(SuggestionDTO item)
        {
            _events.Insert(0, $"Event = CanceledEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        void CommittedItemChanges(SuggestionDTO item)
        {
            _events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }
    }
}
