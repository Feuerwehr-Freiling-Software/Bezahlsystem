using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Components
{
    public partial class CartIconComponent
    {
        public CartIconComponent()
        {

        }

        [Parameter]
        public List<Article> Cart { get; set; }

        private async void Checkout()
        {
            var parameters = new DialogParameters { ["Cart"] = Cart };

            var dialog = await DialogService.ShowAsync<CheckOutComponent>("Checkout", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                // Post zum Server


            }
        }
    }
}
