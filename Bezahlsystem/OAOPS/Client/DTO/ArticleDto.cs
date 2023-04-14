namespace OAOPS.Client.DTO
{
    public class ArticleDto
    {
        public string StorageName { get; set; } = string.Empty;
        public string StorageSlot { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; } = 0;
        public double PriceAmount { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public int QuantityActual { get; set; } = 0;
        public int QuantityAtStart { get; set; } = 0;
        public int MinAmount { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }
}
