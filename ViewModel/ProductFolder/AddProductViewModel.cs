using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using KhareedLo.Models;

namespace KhareedLo.ViewModel.Product
{
    public class AddProductViewModel
    {
        [Display(Name = "Category")]
        public int CatID { get; set; }

        [Required(ErrorMessage = "*Enter name of prduct")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Enter short description of prduct")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "*Enter long description of prduct")]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "*Enter price of prduct")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Is In Stock?")]
        public bool IsInStock { get; set; }

        [Required(ErrorMessage = "*Enter quantity of prduct")]
        [Display(Name = "Quantity")]
        public long quantity { get; set; }

        [Required(ErrorMessage = "*Choose image of prduct")]
        [Display(Name = "Choose Image")]
        public IFormFile ImagePhoto { get; set; }

        [Display(Name = "Select Category")]
        public CategoryModel CategoryName { get; set; }

    }
}
