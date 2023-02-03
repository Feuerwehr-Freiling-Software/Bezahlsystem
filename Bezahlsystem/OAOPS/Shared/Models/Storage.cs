namespace OAOPS.Shared.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string StorageName { get; set; } = null!;
    }
}