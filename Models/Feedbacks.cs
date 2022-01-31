using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace KhareedLo.Models
{
    public partial class Feedbacks
    {
        [Key]
        public int FeedbackId { get; set; }

        //[Required]
        //[StringLength(100,ErrorMessage = "Your name is required.")]
        public string Name { get; set; }

        
        //[Required(ErrorMessage = "Your email is required.")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Feedback is required.")]
        public string Message { get; set; }

        public bool ContactMe { get; set; }

        public class FeedbackMap
        {
            public FeedbackMap(EntityTypeBuilder<Feedbacks> entityTypeBuilder)
            {
                //entityTypeBuilder.HasKey(x => x.FeedbackID);
                entityTypeBuilder.Property(x => x.Name).HasMaxLength(100);
                entityTypeBuilder.Property(x => x.Message).IsRequired();
                entityTypeBuilder.Property(x => x.Email).IsRequired();
            }
        }
    }
}
