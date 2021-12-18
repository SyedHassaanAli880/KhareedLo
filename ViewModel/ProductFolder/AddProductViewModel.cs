using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KhareedLo.Models.Category;

namespace KhareedLo.ViewModel.Product
{
    public class AddProductViewModel
    {
        public int CatID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Is In Stock?")]
        public bool IsInStock { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public long quantity { get; set; }

        [Required]
        [Display(Name = "Choose Image")]
        public IFormFile ImagePhoto { get; set; }

        ///*[Required]*/
        [Display(Name = "Select Category")]
        public List<CategoryModel> categories { get; set; }

    }
}
