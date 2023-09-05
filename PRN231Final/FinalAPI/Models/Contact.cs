using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Date { get; set; }
        public int? IsAdmin { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
    }
}
