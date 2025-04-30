using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketSys.DAL
{
    public class UserBugRepo: GeniricRepo<User_Bugs>, IUserBugRepo
    {
        private readonly BugticketContext context;

        public UserBugRepo(BugticketContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<User_Bugs> GetByIdAsync(Guid userid, Guid  bugid)
        {
            return await context.Set<User_Bugs>()
                .FirstOrDefaultAsync(up => up.User_id == userid && up.Bug_id == bugid);
        }
    }
}
