using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations; // ✅ Correct namespace

namespace FormIOProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
