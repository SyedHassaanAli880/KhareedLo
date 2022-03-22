using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace KhareedLo.Models
{
    public partial class Product
    {
        public int Id { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "*Enter name of product")]
        [StringLength(25)]
        public string Name { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "*Enter short description of category")]
        [StringLength(925)]
        public string ShortDescription { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "*Enter long description of category")]
        [StringLength(925)]
        public string LongDescription { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "*Enter price of category")]
        public decimal Price { get; set; }

        [NotMapped]
        public string NewFileName { get; set; }

        [JsonIgnore]
        public bool IsInStock { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "*Enter quantity of category")]
        public long Quantity { get; set; }

        //[Required(ErrorMessage = "*Enter name of category")]
        //[StringLength(25)]
        //[NotMapped]
        public string ImagePhoto { get; set; }

        [JsonIgnore]
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual CategoryModel Category { get; set; }
    }
}
