namespace OAOPS.Shared.Models
{
    public class ErrorCode
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public bool IsSuccessCode { get; set; }
        public string ErrorText { get; set; } = null!;
    }
}