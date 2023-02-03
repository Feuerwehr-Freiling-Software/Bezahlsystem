using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Models
{
    public class Price
    {
        public int Id { get; set; }
        public DateTime Since { get; set; }
        public DateTime? Until { get; set; }
        public double Amount { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
    }
}
