using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.DTO
{
    public class UserStatsDto
    {
        public Dictionary<string, int> ArticleStats { get; set; } = new();
        public Dictionary<string, List<double>> BalanceStats { get; set; } = new();
    }
}
