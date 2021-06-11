using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ssbbr.Data.DataModels;
using ssbbr.Data;

namespace ssbbr.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<genres>().HasNoKey();
            //builder.Entity<Logins>().HasNoKey();
            base.OnModelCreating(builder);

        }

        public DbSet<Games> Games { get; set; }
        public DbSet<genres> genres { get; set; }
        public DbSet<illegal_contents> illegal_contents { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<ssbbr.Data.RegisterForm> RegisterForm { get; set; }



    }
}
