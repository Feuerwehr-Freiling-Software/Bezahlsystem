using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Notificationsubscription
    {
        public Notificationsubscription()
        {
            UserHasNotifications = new HashSet<UserHasNotification>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserHasNotification> UserHasNotifications { get; set; }
    }
}
