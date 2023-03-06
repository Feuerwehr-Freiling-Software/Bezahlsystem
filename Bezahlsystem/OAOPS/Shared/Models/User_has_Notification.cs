using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Models
{
    public class User_has_Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public int NotificationId { get; set; }
        public NotificationSubscription Notification { get; set; } = null!;
    }
}
