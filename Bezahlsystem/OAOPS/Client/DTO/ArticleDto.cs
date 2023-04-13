namespace OAOPS.Client.DTO
{
    public class ArticleDto
    {
        public string StorageName { get; set; } = string.Empty;
        public string StorageSlot { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; }
        public double PriceAmount { get; set; }
        public string Category { get; set; } = string.Empty;
        public int QuantityActual { get; set; }
        public int QuantityAtStart { get; set; }
        public int MinAmount { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
