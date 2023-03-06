using static OAOPS.Shared.Helpers.Enums;

namespace OAOPS.Shared.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public LogSeverity Severity { get; set; }
        public string Sender { get; set; } = string.Empty;
        public int ErrorCodeId { get; set; }
        public ErrorCode ErrorCode { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
