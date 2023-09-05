
using FinalAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAPI.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string? CommentContent { get; set; }
        public int AccountId { get; set; }
        public int? Start { get; set; }
        public string? DateComment { get; set; }
        public int? RoomId { get; set; }

        public virtual AccountDTO Account { get; set; } = null!;
    }
}
