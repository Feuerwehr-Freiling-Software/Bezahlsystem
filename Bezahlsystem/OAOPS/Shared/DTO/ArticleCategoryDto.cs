using System.Text.Json.Serialization;

namespace OAOPS.Shared.DTO
{
    public class ArticleCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ArticleCategoryDto? Parent { get; set; }
        
        public List<ArticleCategoryDto>? Children { get; set; } = new List<ArticleCategoryDto>();
    }
}