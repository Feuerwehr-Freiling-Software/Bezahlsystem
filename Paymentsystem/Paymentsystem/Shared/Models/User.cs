using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            PaymentExecutors = new HashSet<Payment>();
            PaymentPeople = new HashSet<Payment>();
            TopUpExecutors = new HashSet<TopUp>();
            TopUpPeople = new HashSet<TopUp>();
        }

        public string Discriminator { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double? Balance { get; set; }
        public string? Comment { get; set; }
        public DateTime? OpenCheckoutDate { get; set; }
        public double? OpenCheckoutValue { get; set; }

        public virtual ICollection<Payment> PaymentExecutors { get; set; }
        public virtual ICollection<Payment> PaymentPeople { get; set; }
        public virtual ICollection<TopUp> TopUpExecutors { get; set; }
        public virtual ICollection<TopUp> TopUpPeople { get; set; }
    }
}
