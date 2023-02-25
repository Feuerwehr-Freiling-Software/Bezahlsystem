using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages
{
    public partial class Products
    {
        public Products()
        {

        }
        [Inject]
        IDataService dataService { get; set; }

        List<ArticleDto> Articles = new ();

        protected override async Task OnInitializedAsync()
        {
            Articles = await dataService.GetArticles() ?? new();
        }
    }
}
