using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.AddComponents
{
    public partial class AddTopUpComponent
    {
        [CascadingParameter]
        public MudDialogInstance? MudDialog { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Parameter]
        public UserDto? User { get; set; }

        [Parameter]
        public string ExecutorName { get; set; }

        private double topUpAmount = 0;

        void Cancel()
        {
            MudDialog?.Cancel();
        }

        async Task AddTopUp()
        {
            ErrorDto res = await DataService.AddTopUp(topUpAmount, User.Username, executorName: ExecutorName);
            MudDialog?.Close(DialogResult.Ok(true));
        }
    }
}
