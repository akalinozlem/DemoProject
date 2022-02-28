using Microsoft.EntityFrameworkCore;

namespace DemoProject.Models
{
    public class AdminUserContext : DbContext
    {
        public AdminUserContext(DbContextOptions<AdminUserContext> options) : base(options)
        {

        }

        public DbSet<AdminUser> AdminUsers { get; set; }

        
    }
}
