using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace KhareedLo.Models
{
    public partial class CategoryModel
    {
        public CategoryModel()
        {

            Products = new HashSet<Product>();
        }

        //[BindNever]
        public int Id { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage ="*Enter name of category")]
        [StringLength(25)]
        public string Name { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Verify if active or not:")]
        public int IsActive { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
