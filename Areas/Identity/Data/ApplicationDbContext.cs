using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FormIOProject.Models;
using FormIOProject.Areas.Identity.Model;

namespace FormIOProject.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your other entities
    
public DbSet<FormIO> FormIO { get; set; } = default!;


    }
}