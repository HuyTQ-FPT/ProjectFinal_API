using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int? AccountId { get; set; }
        public int? IsReceive { get; set; }
        public int? IsSent { get; set; }
        public string? Message1 { get; set; }

        public virtual Account? Account { get; set; }
    }
}
