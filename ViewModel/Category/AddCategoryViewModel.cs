using System.ComponentModel.DataAnnotations;

namespace KhareedLo.ViewModel.Category
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Is Active")]
        public int IsActive { get; set; }
    }
}
