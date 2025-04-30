using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketSys.DAL;

internal class UnitOfWork : IUnitOfWork
{

    public IprojectRepo ProjectRepo { get; }
    public IBugRepo bugRepo { get; }

    public IUserBugRepo UserBugRepo { get; }
    public IAttachmentRepo AttachRepo { get; }


    private readonly BugticketContext _context;

    public UnitOfWork(IprojectRepo iprojectRepo,
        BugticketContext con_text,
        IBugRepo ibugRepo,
        IUserBugRepo  _userBugRepo,
        IAttachmentRepo _attachmentRepo)
    {
        _context = con_text;
        ProjectRepo = iprojectRepo;
        bugRepo= ibugRepo;
        UserBugRepo = _userBugRepo;
        AttachRepo = _attachmentRepo;
    }

    public async Task<int> savechangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
