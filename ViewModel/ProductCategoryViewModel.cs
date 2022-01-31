using KhareedLo.Models;
using System.Collections.Generic;

namespace KhareedLo.ViewModel
{
    public class ProductCategoryViewModel
    {
        public ProductCategoryViewModel()
        {
            categories = new List<CategoryModel>();
        }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<CategoryModel> categories { get; set; }

    }
}
