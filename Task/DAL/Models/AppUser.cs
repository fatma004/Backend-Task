using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
   public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
