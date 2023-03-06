using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ArticleTypeId { get; set; }

        public virtual Articlecategory ArticleType { get; set; } = null!;
    }
}
