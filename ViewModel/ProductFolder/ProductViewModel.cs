using System.Collections.Generic;

namespace KhareedLo.ViewModel
{
    public class ProductViewModel
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public bool IsInStock { get; set; }

        public long Quantity { get; set; }

        public string ImagePhoto { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
