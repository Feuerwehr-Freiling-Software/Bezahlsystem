using OAOPS.Shared.DTO;
using OAOPS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OAOPS.Shared.Helpers.Enums;

namespace OAOPS.Shared.Interfaces
{
    public interface ISuggestionService
    {
        public List<SuggestionDTO> GetSuggestions(bool? onlyNew = null, Importance? importance = null);
        public int AddSuggestion(SuggestionDTO suggestion, string username);
        public int RemoveSuggestion(int id);
        public int UpdateSuggestion(int id, SuggestionDTO suggestion);
    }
}
