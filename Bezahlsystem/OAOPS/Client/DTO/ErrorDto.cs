namespace OAOPS.Client.DTO
{
    public class ErrorDto
    {
        public int Code { get; set; } = -1;
        public bool IsSuccessCode { get; set; } = false;
        public string ErrorText { get; set; } = "Unexpected error occured. See logs for further Information";
    }
}
