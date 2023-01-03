using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Suggestion
    {
        public int Id { get; set; }
        public string SuggestionText { get; set; } = null!;
        public int? Importance { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
