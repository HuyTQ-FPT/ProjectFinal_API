using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? AccountId { get; set; }
        public int? SentByAccountId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Date { get; set; }
        public int? IsNew { get; set; }

        public virtual Account? Account { get; set; }
    }
}
