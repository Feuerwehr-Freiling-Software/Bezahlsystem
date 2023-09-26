using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using OAOPS.Client.Components.AddComponents;
using OAOPS.Client.Services;
using OAOPS.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        [Inject]
        public IDialogService DialogService { get; set; }

        public List<StorageDto> VendingMachines { get; set; }


        protected override async Task OnInitializedAsync()
        {
            VendingMachines = await DataService.GetAllStorages() ?? new List<StorageDto>();
        }

        void OpenSlotsOfStorage(string name)
        {
            navigation.NavigateTo("/manage/slot/" + name);
        }

        async void CreateStorage()
        {
            var opt = new DialogOptions()
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium
            };

            var dialog = await DialogService.ShowAsync<AddStorageComponent>("Lagerplatz hinzufügen", opt);
            var res = await dialog.Result;
        }

        string getImageData(string name)
        {
            var fMachine = VendingMachines.FirstOrDefault(x => x.StorageName == name);
            if (fMachine != null && fMachine.ImageData != "")
            {
                return $"data:image/png;base64,{fMachine.ImageData}";
            }
            else
            {
                return "/images/no-image.png";
            }
        }
    }
}
