using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class HistoryBooking
    {
        public int HistoryId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
