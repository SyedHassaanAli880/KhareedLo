using KhareedLo.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KhareedLo.Models
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Feedbacks> Feedbacks { get; set; }
        public DbSet<CategoryModel> CategoryModels { get; set; }
        public DbSet<ReviewsAndComment> ReviewsAndComments { get; set; }

        //public DbSet<ExternalUser> ExternalUsers { get; set; }
    }
}
