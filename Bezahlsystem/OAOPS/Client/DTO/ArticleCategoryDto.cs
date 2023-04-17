namespace OAOPS.Client.DTO
{
    public class ArticleCategoryDto
    {        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ArticleCategoryDto? Parent { get; set; }
        public List<ArticleCategoryDto>? Children { get; set; } = new List<ArticleCategoryDto>();

        public override string ToString()
        {
            return Name;
        }
    }
}