using OAOPS.Client.Services;

namespace OAOPS.Client.Pages
{
    public partial class Suggestions
    {
        [Inject]
        public DataService dataService { get; set; }

        SuggestionDTO suggestion;

        protected override void OnInitialized()
        {
            suggestion = new SuggestionDTO();

        }

        async Task SubmitSuggestion()
        {
            var res = await dataService.AddSuggestion(suggestion);

            snackbar.Add(!res.IsSuccessStatusCode ? "Vorschlag konnte nicht versendet werden" + res.Content : "Vorschlag erfolgreich versendet", !res.IsSuccessStatusCode ? Severity.Error : Severity.Success);
            if (res.IsSuccessStatusCode)
            {
                nav.NavigateTo("/");
            }
            else
            {
                Console.WriteLine("Error: " + res.Content);
            }
        }

        void Back()
        {
            nav.NavigateTo(nav.BaseUri);
        }
    }
}
