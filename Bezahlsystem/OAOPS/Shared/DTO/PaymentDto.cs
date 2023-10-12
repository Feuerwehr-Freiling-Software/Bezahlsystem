using OAOPS.Shared.DTO;

namespace OAOPS.Client.DTO
{
    public class PaymentDto
    {
        public ArticleDto Article { get; set; }
        public string Username { get; set; }
        public double Sum { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
