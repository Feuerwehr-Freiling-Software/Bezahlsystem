using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Articlecategory
    {
        public Articlecategory()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Level { get; set; } = null!;

        public int? ParentCategoryId { get; set; }
        public Articlecategory? ParentCategory { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}