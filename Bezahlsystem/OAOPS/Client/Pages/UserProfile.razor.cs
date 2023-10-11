using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OAOPS.Client.Pages
{
    public partial class UserProfile
    {
        public UserStatsDto? UserStats { get; set; } = null;
        public double[] ArticleData { get; set; } = Array.Empty<double>();
        public string[] ArticleLabels { get; set; } = Array.Empty<string>();

        public List<ChartSeries> PaymentData = new ();
        public string[] PaymentLabels = { "Jan", "Feb", "Mar", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dez" };

        private readonly ChartOptions _PaymentChartOptions = new();

        protected override void OnInitialized()
        {
            _PaymentChartOptions.YAxisTicks = 5;
        }

        async Task GetUserStats()
        {
            var tmpUser = user.Identity?.Name;
            if (tmpUser == null) return;
            var res = await DataService.GetUserStats(tmpUser);

            if (res.BalanceStats.Count == 0 || res.ArticleStats.Count == 0) return; 

            UserStats = res;

            foreach (var item in res.ArticleStats)
            {
                ArticleData = ArticleData.Append(item.Value).ToArray();
                ArticleLabels = ArticleLabels.Append(item.Key).ToArray();
            }

            foreach (var item in res.BalanceStats)
            {
                PaymentData.Add(new ChartSeries() { Name = item.Key, Data = item.Value.ToArray() });
            }
        }
    }
}
