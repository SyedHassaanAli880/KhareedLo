using System.ComponentModel.DataAnnotations;

namespace KhareedLo.Models.Category
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Is Active")]
        public int IsActive { get; set; }
    }
}
