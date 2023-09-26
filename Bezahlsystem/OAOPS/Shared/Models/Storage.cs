namespace OAOPS.Shared.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string StorageName { get; set; } = string.Empty;
        public string ImageData { get; set; } = string.Empty;
    }
}