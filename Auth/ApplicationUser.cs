using Microsoft.AspNetCore.Identity;
using System;

namespace KhareedLo.Auth
{
    public class ApplicationUser:IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public IdentityRole Role { get; set; }
    }
}
