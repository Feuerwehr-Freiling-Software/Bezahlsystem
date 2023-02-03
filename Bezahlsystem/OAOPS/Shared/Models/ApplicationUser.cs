using Microsoft.AspNetCore.Identity;

namespace OAOPS.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Suggestion>? Suggestions{ get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public double Balance { get; set; }
        public string? Comment { get; set; }
        public bool IsConfirmedUser { get; set; }
    }
}