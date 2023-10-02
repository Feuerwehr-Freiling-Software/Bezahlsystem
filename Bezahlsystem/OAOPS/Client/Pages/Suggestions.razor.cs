using OAOPS.Client.Services;
using static MudBlazor.CategoryTypes;

namespace OAOPS.Client.Pages
{
    public partial class Suggestions
    {
        [Inject]
        public IDataService dataService { get; set; }

        SuggestionDTO suggestion;

        protected override void OnInitialized()
        {
            suggestion = new SuggestionDTO();

        }

        async Task SubmitSuggestion()
        {
            var res = await dataService.AddSuggestion(suggestion);

            if (res.IsSuccessCode)
            {
                snackbar.Add("Vorschlag erfolgreich hinzugefügt.");
                nav.NavigateTo("/");
            }
            else
            {
                snackbar.Add(res.ErrorText, Severity.Error);
            }
        }

        void Back()
        {
            nav.NavigateTo(nav.BaseUri);
        }
    }
}
