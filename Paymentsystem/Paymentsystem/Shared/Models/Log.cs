using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int Severity { get; set; }
        public string Sender { get; set; } = null!;
        public int ErrorCodeId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Errorcode ErrorCode { get; set; } = null!;
    }
}
