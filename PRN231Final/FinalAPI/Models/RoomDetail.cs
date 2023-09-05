using System;
using System.Collections.Generic;

namespace FinalAPI.Models
{
    public partial class RoomDetail
    {
        public int? RoomId { get; set; }
        public int? Size { get; set; }
        public string? Introduce { get; set; }
        public int? Children { get; set; }
        public int? Adult { get; set; }
        public string? Device { get; set; }

        public virtual Room? Room { get; set; }
    }
}
