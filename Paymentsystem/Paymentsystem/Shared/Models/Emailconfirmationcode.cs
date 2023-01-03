using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Emailconfirmationcode
    {
        public int Id { get; set; }
        public string ConfirmationCode { get; set; } = null!;
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
    }
}