using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Models
{
    public class OpenCheckout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double ValueAtStart { get; set; }
        public double ValueActual { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
    }
}
