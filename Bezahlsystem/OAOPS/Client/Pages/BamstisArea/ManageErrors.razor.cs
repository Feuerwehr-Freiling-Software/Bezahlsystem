using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Helpers;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages.BamstisArea
{
    public partial class ManageErrors : ComponentBase
    {
        public ManageErrors()
        {

        }

        [Inject]
        public IDataService DataService { get; set; }

        private List<ErrorDto> Elements = new();


        protected override async Task OnInitializedAsync()
        {
            Elements = await DataService.GetAllErrors() ?? new();

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
