using System;
using System.Collections.Generic;

#nullable disable

namespace KhareedLo.Models
{
    public partial class CategoryModel
    {
        public CategoryModel()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
