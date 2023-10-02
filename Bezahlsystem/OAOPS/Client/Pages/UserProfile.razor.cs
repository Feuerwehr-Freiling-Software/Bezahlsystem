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
        public UserProfile()
        {

        }

        async Task GetUserStats()
        {
            UserStatsDto res = await DataService.GetUserStats();

        }
    }
}
