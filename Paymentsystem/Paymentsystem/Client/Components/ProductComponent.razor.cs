using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Components
{
    public partial class ProductComponent
    {
        public ProductComponent()
        {

        }

        [Parameter]
        public Article Article { get; set; }
        [Parameter]
        public string Path { get; set; }
    }
}
