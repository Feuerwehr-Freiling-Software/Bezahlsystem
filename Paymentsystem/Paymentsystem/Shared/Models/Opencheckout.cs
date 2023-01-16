using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Opencheckout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double ValueAtStart { get; set; }
        public double AcctualValue { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
