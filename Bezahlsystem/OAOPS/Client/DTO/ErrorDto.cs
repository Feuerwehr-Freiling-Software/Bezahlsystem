namespace OAOPS.Client.DTO
{
    public class ErrorDto
    {
        public int Code { get; set; }
        public bool IsSuccessCode { get; set; }
        public string ErrorText { get; set; } = null!;
    }
}
