namespace Paymentsystem.Server.Hubs
{
    public class ErrorCode
    {
        public int Code { get; internal set; }
        public string ErrorText { get; internal set; }
        public bool IsSuccessErrorCode { get; internal set; }
        public int Id { get; internal set; }
    }
}