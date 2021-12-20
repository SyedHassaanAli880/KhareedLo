using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KhareedLo.Models
{
    public partial class ReviewsAndComment
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public int? ProductId { get; set; }
    }
}
