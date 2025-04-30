using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketSys.DAL
{
    public class Attachement_Cofiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasOne(b => b.Bug)
                   .WithMany(p => p.Attachments)
                   .HasForeignKey(b => b.Bug_id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
