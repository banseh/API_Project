using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketSys.DAL
{
    public class projectRepo : GeniricRepo<Project> , IprojectRepo
    {


        public projectRepo(BugticketContext _context) :base(_context) { }
       
    }
}
