using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string? CommentContent { get; set; }
        public int AccountId { get; set; }
        public int? Start { get; set; }
        public string? DateComment { get; set; }
        public int? RoomId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Room? Room { get; set; }
    }
}
