namespace OAOPS.Client.DTO
{
    public class ArticleDto
    {
        public string Name { get; set; } = string.Empty;
        public int ArticleCategoryId { get; set; }
        public ArticleCategoryDto ArticleCategory { get; set; } = null!;
        public int Amount { get; set; }
        public double PriceAmount { get; set; }
    }
}
