using OAOPS.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OAOPS.Shared.Helpers.Enums;

namespace OAOPS.Shared.DTO
{
    public class SuggestionDTO
    {
        public SuggestionDTO()
        {

        }

        public string SuggestionText { get; set; } = string.Empty;
        public Importance Importance { get; set; } = Importance.NotSet;
        public string Username { get; set; } = string.Empty;
    }
}
