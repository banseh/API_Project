using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.DAL;

namespace BugTicketSys.BL.Dtos.UserBug
{
    public class UserBugDto
    {
        public Guid User_id { get; set; }
        public Guid Bug_id { get; set; }
    }
}

