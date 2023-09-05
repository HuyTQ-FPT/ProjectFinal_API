using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            HistoryBookings = new HashSet<HistoryBooking>();
        }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<HistoryBooking> HistoryBookings { get; set; }
    }
}
