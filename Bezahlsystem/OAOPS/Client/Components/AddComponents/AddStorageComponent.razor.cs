using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components.AddComponents
{
    public partial class AddStorageComponent
    {
        public AddStorageComponent()
        {

        }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public StorageDto NewStorage { get; set; } = new();

        private async Task AddFile()
        {
            
        }

        private async Task OnInputFileChange(IBrowserFile file)
        {
            var resizedFile = await file.RequestImageFileAsync(file.ContentType, 640, 480); // resize the image file
            var buf = new byte[resizedFile.Size]; // allocate a buffer to fill with the file's data
            using (var stream = resizedFile.OpenReadStream())
            {
                await stream.ReadAsync(buf); // copy the stream to the buffer
            }

            NewStorage.ImageData = Convert.ToBase64String(buf); // convert to a base64 string!!
        }

        void Cancel()
        {
            MudDialog?.Cancel();
        }

        protected async Task AddStorage()
        {
            var res = await DataService.AddStorage(NewStorage);
            if(res != null && res.Count <= 0)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
            else
            {
                MudDialog.Close(DialogResult.Ok(false));
            }
        }
    }
}
