using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketSys.DAL
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid Project_id { get; set; }
        public Project Project { get; set; } = null!;
        public ICollection<Attachment> Attachments { get; set; } = null!;
        public ICollection<User_Bugs> User_Bugs { get; set; } = null!;
    }
}
