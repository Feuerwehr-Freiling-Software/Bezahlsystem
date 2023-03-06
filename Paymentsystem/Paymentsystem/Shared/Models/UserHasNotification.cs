using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class UserHasNotification
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!; 
        public int NotificationId { get; set; }

        public virtual Notificationsubscription Notification { get; set; } = null!;
    }
}
