using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Models
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ArticleCategory? Parent { get; set; }
        public int? ParentId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<ArticleCategory>? Children { get; set; }
        public List<Article>? Articles { get; set; }
    }
}
