using System;
using System.Collections.Generic;
using static Paymentsystem.Shared.ViewModels.Enums;

namespace Paymentsystem.Shared.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public LogSeverity Severity { get; set; }
        public string Sender { get; set; } = null!;
        public int ErrorCodeId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Errorcode ErrorCode { get; set; } = null!;
    }
}
