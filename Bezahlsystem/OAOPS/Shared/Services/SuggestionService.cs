using Microsoft.EntityFrameworkCore;
using OAOPS.Shared.DTO;

namespace OAOPS.Shared.Services
{
    public class SuggestionService : ISuggestionService
    {
        private readonly ApplicationDbContext db;

        public SuggestionService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddSuggestion(SuggestionDTO suggestionDto, string username)
        {
            var suggestion = new Suggestion()
            {
                Importance = Importance.NotSet,
                SuggestionText = suggestionDto.SuggestionText
            };

            var fUser = db.Users.FirstOrDefault(x => x.UserName == username);
            if(fUser == null) return 41;

            fUser.Suggestions.Add(suggestion);
            var res = db.SaveChanges();

            switch (res)
            {
                case < 1:
                    return 42;
                case 1:
                    return 40;
                case > 1:
                    return 43;
            }
        }

        public List<SuggestionDTO> GetSuggestions(bool? onlyNew = null, Importance? importance = null)
        {
            Importance? imp;
            if (onlyNew != null)
            {
                imp = Importance.NotSet;

                return db.Users
                    .Include(x => x.Suggestions)
                    .SelectMany(user => user.Suggestions, (user, suggestion) => new SuggestionDTO
                    {
                        Id = suggestion.Id,
                        Importance = suggestion.Importance,
                        SuggestionText = suggestion.SuggestionText,
                        Username = user.UserName
                    })
                    .Where(x => x.Importance == imp)
                    .ToList();
            }
            else if (importance != null)
            {
                imp = importance;
                return db.Users
                    .Include(x => x.Suggestions)
                    .SelectMany(user => user.Suggestions, (user, suggestion) => new SuggestionDTO
                    {
                        Id = suggestion.Id,
                        Importance = suggestion.Importance,
                        SuggestionText = suggestion.SuggestionText,
                        Username = user.UserName
                    })
                    .Where(x => x.Importance == imp)
                    .ToList();
            }
            else
            {
                return db.Users
                    .Include(x => x.Suggestions)
                    .SelectMany(user => user.Suggestions, (user, suggestion) => new SuggestionDTO
                    {
                        Id = suggestion.Id,
                        Importance = suggestion.Importance,
                        SuggestionText = suggestion.SuggestionText,
                        Username = user.UserName
                    })
                    .ToList();
            }
        }

        public int RemoveSuggestion(int id)
        {
            var suggestion = db.Suggestions.FirstOrDefault(s => s.Id == id);
            if (suggestion == null) return 44;

            db.Suggestions.Remove(suggestion);
            var res = db.SaveChanges();

            switch (res)
            {
                case < 1:
                    return 42;
                case 1:
                    return 40;
                case > 1:
                    return 43;
            }
        }

        public int UpdateSuggestion(SuggestionDTO suggestion)
        {
            var fSuggestion = db.Suggestions.FirstOrDefault(x => x.Id == suggestion.Id);
            if (fSuggestion == null) return 44;

            fSuggestion.SuggestionText = suggestion.SuggestionText;
            fSuggestion.Importance = suggestion.Importance;

            db.Suggestions.Update(fSuggestion);
            var res = db.SaveChanges();

            switch (res)
            {
                case < 1:
                    return 42;
                case 1:
                    return 40;
                case > 1:
                    return 43;
            }
        }
    }
}
