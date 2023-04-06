using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.DTO;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages.AdminArea
{
    public partial class SlotManagement
    {
        public SlotManagement()
        {

        }

        [Parameter]
        public string Name { get; set; }

        [Inject]
        public NavigationManager navigation { get; set; }

        [Inject]
        public IDataService dataService { get; set; }

        public List<StorageSlotDto>? Slots { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Slots = await dataService.GetSlotsOfStorageByName(Name) ?? new();
        }

        void GoBack()
        {
            navigation.NavigateTo("/manage/vendingMachines");
        }

        void DeleteSlot(StorageSlotDto slot)
        {
            
        }

        // events
        void StartedEditingItem(StorageSlotDto item)
        {

        }
        
        void CanceledEditingItem(StorageSlotDto item)
        {

        }

        async void CommittedItemChanges(StorageSlotDto item)
        {
            var res = await dataService.UpdateStorageSlot(item);

        }
    }
}
