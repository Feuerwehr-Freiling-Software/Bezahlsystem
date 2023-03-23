using OAOPS.Shared.DTO;

namespace OAOPS.Client.DTO
{
    public class PaymentDto
    {
        public List<ArticleDto> Articles { get; set; }
        public string Username { get; set; }
    }
}
