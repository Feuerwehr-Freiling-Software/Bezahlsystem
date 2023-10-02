using OAOPS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OAOPS.Client.Helpers.Enums;

namespace OAOPS.Client.DTO
{
    public class SuggestionDTO
    {
        public SuggestionDTO()
        {

        }

        public int Id { get; set; } = 0;
        public string SuggestionText { get; set; } = string.Empty;
        public Importance Importance { get; set; } = Importance.NotSet;
        public string Username { get; set; } = string.Empty;
    }
}
