using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KhareedLo.Models.cart
{
    public partial class AddToCart
    {
        [Key]
        public int Id { get; set; }
        public int ProdId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public int TotalOfProduct { get; set; }
    }
}
