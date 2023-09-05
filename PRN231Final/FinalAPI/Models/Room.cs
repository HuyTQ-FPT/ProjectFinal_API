using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
            Comments = new HashSet<Comment>();
        }

        public int RoomId { get; set; }
        public int? CagogoryId { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? IsBooking { get; set; }

        public virtual Category? Cagogory { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
