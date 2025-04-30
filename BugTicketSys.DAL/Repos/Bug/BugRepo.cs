using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketSys.DAL
{
    public class BugRepo : GeniricRepo<Bug>, IBugRepo
    {
        private readonly BugticketContext context;

        public BugRepo(BugticketContext _context) : base(_context)
        {
            context = _context;
        }


        public async Task<List<Bug>> GetAllAsync()
        {
            return await context.Bugs
                .Include(b => b.Attachments)
                .ToListAsync();
        }

        public async Task<Bug> GetByIdAsync(Guid id)
        {
            await context.Set<Bug>().FindAsync(id);

            return await context.Bugs
                .Include(b => b.Attachments)
        .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
