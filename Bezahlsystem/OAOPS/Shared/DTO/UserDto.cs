using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.DTO
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double Balance { get; set; }
        public string? Comment { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsConfirmedUser { get; set; }
    }
}
