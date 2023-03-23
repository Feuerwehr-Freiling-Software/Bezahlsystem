using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using OAOPS.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages.AdminArea
{
    public partial class StorageManagement
    {
        public StorageManagement()
        {

        }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public NavigationManager navigation { get; set; }

        public List<StorageVM> VendingMachines { get; set; }


        protected override async Task OnInitializedAsync()
        {
            VendingMachines = await DataService.GetAllStorages() ?? new List<StorageVM>();
        }

        void OpenSlotsOfStorage(string name)
        {
            navigation.NavigateTo("/manage/slot/" + name);
        }

    }
}
