namespace OAOPS.Shared.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StorageId { get; set; }
        public Storage Storage { get; set; } = null!;
    }
}