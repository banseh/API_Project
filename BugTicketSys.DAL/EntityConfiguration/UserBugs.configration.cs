using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketSys.DAL
{
    public class UserBugs : IEntityTypeConfiguration<User_Bugs>
    {
        public void Configure(EntityTypeBuilder<User_Bugs> builder)
        {
            builder.HasKey(up => new { up.User_id, up.Bug_id });

            builder
           .HasOne(up => up.User)
           .WithMany(u=>u.User_Bugs) 
           .HasForeignKey(up => up.User_id)
           .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(up => up.Bug)
                .WithMany(b=>b.User_Bugs) // Or specify a collection navigation property in Bug class
                .HasForeignKey(up => up.Bug_id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
