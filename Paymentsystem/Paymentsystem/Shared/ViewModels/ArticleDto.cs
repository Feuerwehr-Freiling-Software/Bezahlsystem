using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.ViewModels
{
    public class ArticleDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public double PriceAmount { get; set; }
    }
}
