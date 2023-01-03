using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Refreshtoken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}
