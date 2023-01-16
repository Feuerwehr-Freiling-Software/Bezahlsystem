using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public double Balance { get; set; }
        public string? Comment { get; set; }
    }
}
