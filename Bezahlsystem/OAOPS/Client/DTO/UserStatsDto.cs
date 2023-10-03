namespace OAOPS.Client.DTO
{
    public class UserStatsDto
    {
        public Dictionary<string, int> ArticleStats { get; set; } = new();
        public Dictionary<string, Dictionary<string, List<double>>> BalanceStats { get; set; } = new();
    }
}
