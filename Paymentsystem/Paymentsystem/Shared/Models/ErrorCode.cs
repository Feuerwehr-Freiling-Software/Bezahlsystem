using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Models
{
    public class ErrorCode
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public bool IsSuccessErrorCode { get; set; }
        public string ErrorText { get; set; } = string.Empty;
    }
}
