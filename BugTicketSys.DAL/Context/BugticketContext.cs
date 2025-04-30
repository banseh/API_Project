
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BugTicketSys.DAL
{
    public class BugticketContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<Bug> Bugs => Set<Bug>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<User_Bugs> User_Bugss => Set<User_Bugs>();
    
        public BugticketContext(DbContextOptions<BugticketContext> options)
          : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BugticketContext).Assembly);
        }

    }
}
