using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketSys.DAL
{
    public class User_Cofiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(u => u.UserName)
                  .HasMaxLength(255);
            builder.HasMany(u => u.User_Bugs)
               .WithOne(up => up.User)
               .HasForeignKey(up => up.User_id);

        }
    }
}
