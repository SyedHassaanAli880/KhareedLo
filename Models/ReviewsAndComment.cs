using System;
using System.Collections.Generic;

#nullable disable

namespace KhareedLo.Models
{
    public partial class ReviewsAndComment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? ProductId { get; set; }
    }
}
