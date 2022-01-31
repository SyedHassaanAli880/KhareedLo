using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KhareedLo.Models
{
    public partial class AddToCart
    {
        public int Id { get; set; }
        public int ProdId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? Quantity { get; set; }
        public int TotalOfProduct { get; set; }
    }
}
