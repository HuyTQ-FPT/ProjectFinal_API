using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Booking
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? RoomId { get; set; }
        public string? CheckIn { get; set; }
        public int? IsComment { get; set; }
        public string? CheckOut { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Room? Room { get; set; }
    }
}
