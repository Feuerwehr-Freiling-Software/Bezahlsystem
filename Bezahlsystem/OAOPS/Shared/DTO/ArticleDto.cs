using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.DTO
{
    public class ArticleDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public double PriceAmount { get; set; }
        public string Category { get; set; }
    }
}
