namespace OAOPS.Client.Pages
{
    public partial class Suggestions
    {
        SuggestionDTO suggestion;

        protected override void OnInitialized()
        {
            suggestion = new SuggestionDTO();

        }

        async Task SubmitSuggestion()
        {
            var res = await http.PostAsJsonAsync<SuggestionDTO>("api/Suggestion/AddSuggestion", suggestion);

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
