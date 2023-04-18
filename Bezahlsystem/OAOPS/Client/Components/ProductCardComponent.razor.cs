using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Components
{
    public partial class ProductCardComponent
    {
        public ProductCardComponent()
        {

        }

        [Parameter]
        public ArticleDto Article { get; set; }

        [Parameter]
        public string Path { get; set; } = string.Empty;
    }
}
