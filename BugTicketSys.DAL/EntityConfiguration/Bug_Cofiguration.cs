using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketSys.DAL
{
    public class Bug_Cofiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.HasKey(e => e.Id);



            builder.HasMany(u => u.User_Bugs)
               .WithOne(up => up.Bug)
               .HasForeignKey(up => up.Bug_id);




            builder.HasOne(b => b.Project)
           .WithMany(p => p.Bugs)
           .HasForeignKey(b => b.Project_id)
           .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
