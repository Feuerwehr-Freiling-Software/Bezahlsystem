using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OAOPS.Shared.Helpers.Enums;

namespace OAOPS.Shared.Models
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string SuggestionText { get; set; } = string.Empty;
        public Importance Importance { get; set; } = Importance.NotSet;
    }
}
