using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OAOPS.Client.DTO;
using OAOPS.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages
{
    public partial class UserPayments
    {
        public UserPayments()
        {

        }

        [Inject]
        public IDataService DataService { get; set; }

        public List<ShortCategoryDto> ShortCategories { get; set; } = new();
        public ShortCategoryDto SelectedCategory { get; set; } = new();

        public List<PaymentDto> Payments { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            ShortCategories = await DataService.GetAllCategoriesShort();
            await GetAllPaymentsFiltered();
        }

        public async Task CategoryChanged()
        {
            await GetAllPaymentsFiltered();
        }

        public async Task FromDateChanged()
        {
            await GetAllPaymentsFiltered();
        }

        public async Task ToDateChanged()
        {
            await GetAllPaymentsFiltered();
        }

        public async Task MinAmountChanged()
        {
            await GetAllPaymentsFiltered();
        }

        public async Task MaxAmountChanged()
        {
            await GetAllPaymentsFiltered();
        }

        // Get All Payments Filtered
        public async Task GetAllPaymentsFiltered()
        {
            Payments = await DataService.GetAllPaymentsFiltered(fromDate, toDate, SelectedCategory, minAmount, maxAmount);
            StateHasChanged();
        }
    }
}
