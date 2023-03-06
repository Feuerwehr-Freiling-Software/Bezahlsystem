using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.ViewModels
{
    public class ErrorDto
    {
        public int Code { get; set; }
        public string ErrorText { get; set; }
        public bool IsSuccessErrorcode { get; set; }
    }
}
