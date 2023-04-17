namespace OAOPS.Client.DTO
{
    public class ArticleDto
    {
        private double priceAmount = 0;

        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; } = 0;
        public double PriceAmount { get => priceAmount; 
            set 
            {
                priceAmount = Math.Round(value, 2);
            } }
        public string Category { get; set; } = string.Empty;
        public int QuantityActual { get; set; } = 0;
        public int QuantityAtStart { get; set; } = 0;
        public int MinAmount { get; set; } = 0;
        public string StorageName { get; set; } = string.Empty;
        public string StorageSlot { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
