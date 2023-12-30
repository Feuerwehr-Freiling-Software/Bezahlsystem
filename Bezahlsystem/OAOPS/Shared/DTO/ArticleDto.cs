namespace OAOPS.Shared.DTO
{
    public class ArticleDto
    {
        public int Id { get; set; } = 0;
        public string StorageName { get; set; } = string.Empty;
        public string StorageSlot { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; } = 0;
        public double PriceAmount { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public int QuantityActual { get; set; } = 0;
        public int QuantityAtStart { get; set; } = 0;
        public int MinAmount { get; set; } = 0;
        public string Base64data { get; set; } = string.Empty;
    }
}
