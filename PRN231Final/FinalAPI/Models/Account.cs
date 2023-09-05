using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Account
    {
        public Account()
        {
            Comments = new HashSet<Comment>();
            Contacts = new HashSet<Contact>();
            Customers = new HashSet<Customer>();
            Messages = new HashSet<Message>();
            Notifications = new HashSet<Notification>();
        }

        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public int? IsReport { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
