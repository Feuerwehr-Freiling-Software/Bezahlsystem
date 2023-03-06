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

            if (res.Count == 0)
            {
                snackbar.Add("Vorschlag erfolgreich hinzugefügt.");
                nav.NavigateTo("/");
            }
            else
            {
                foreach (var item in res)
                {
                    snackbar.Add(item.ErrorText, Severity.Error);
                }
            }
        }

        void Back()
        {
            nav.NavigateTo(nav.BaseUri);
        }
    }
}
