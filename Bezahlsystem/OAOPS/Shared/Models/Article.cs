namespace OAOPS.Shared.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ArticleCategoryId { get; set; }
        public ArticleCategory ArticleCategory { get; set; } = null!;
        public string? PicturePath { get; set; }
    }
}