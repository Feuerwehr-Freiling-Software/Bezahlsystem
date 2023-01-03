using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class User
    {
        public string Id { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public double Balance { get; set; }
        public string? Comment { get; set; }
        public string Email { get; set; } = null!;
        public bool ConfirmedEmail { get; set; }
        public string Role { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public int RefreshtokenId { get; set; }
        public int EmailConfirmationCodeId { get; set; }
        public bool IsConfirmedUser { get; set; }

        public virtual Emailconfirmationcode EmailConfirmationCode { get; set; } = null!;
        public virtual Refreshtoken Refreshtoken { get; set; } = null!;
    }
}
