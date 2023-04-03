using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
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

        void GoBack()
        {
            navigation.NavigateTo("/manage/vendingMachines");
        }
    }
}
