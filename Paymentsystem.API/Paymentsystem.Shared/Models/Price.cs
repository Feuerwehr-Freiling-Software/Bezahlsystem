using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Price
    {
        public Price()
        {
            Articles = new HashSet<Article>();
            BoughtArticles = new HashSet<BoughtArticle>();
        }

        public int PriceId { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
        public double Amount { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<BoughtArticle> BoughtArticles { get; set; }
    }
}
