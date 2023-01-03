using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Errorcode
    {
        public Errorcode()
        {
            Logs = new HashSet<Log>();
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public sbyte IsSuccessErrorCode { get; set; }
        public string ErrorText { get; set; } = null!;

        public virtual ICollection<Log> Logs { get; set; }
    }
}
