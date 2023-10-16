namespace OAOPS.Client.DTO
{
    public class TopUpDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double CashAmount { get; set; }
        public string ExecutorName { get; set; }
    }
}
