namespace OAOPS.Client.DTO
{
    public class ErrorDto
    {
        public int Code { get; set; } = 0;
        public bool IsSuccessCode { get; set; } = false;
        public string ErrorText { get; set; } = string.Empty;
    }
}
