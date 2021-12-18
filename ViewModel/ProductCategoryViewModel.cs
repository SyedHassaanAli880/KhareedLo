using KhareedLo.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
