using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketSys.DAL;

public class AttachmentRepo : GeniricRepo<Attachment>, IAttachmentRepo
{
    private readonly BugticketContext _context;


    public AttachmentRepo(BugticketContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId)
    {
        return await _context.Set<Attachment>()
            .Include(a => a.Bug)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AttachmentId == attachmentId && a.Bug_id == bugId)
            ?? null;
    }
    public async Task<List<Attachment>?> GetAttachmentsByBugIdAsync(Guid bugId)
    {
        return await _context.Set<Attachment>()
            .Include(a => a.Bug)
            .Where(a => a.Bug_id == bugId)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Attachment?> GetAttachmentByFilePathAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        return await _context.Set<Attachment>()
            .FirstOrDefaultAsync(a => a.FileUrl == filePath);
    }
    public async Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId)
    {
        var attachment = await _context.Attachments
        .FirstOrDefaultAsync(a => a.AttachmentId == attachmentId && a.Bug_id == bugId);

        if (attachment == null)
        {
            return false;
        }

        _context.Attachments.Remove(attachment);
        return true;
    }
    public async Task<Attachment?> UploadAttachmentAsync(Guid bugId, Attachment attachment)
    {

        var bug = await _context.Bugs
            .FirstOrDefaultAsync(b => b.Id == bugId);
        if (bug == null)
        {
            return null;
        }
        attachment.Bug_id = bugId;

        await _context.Attachments.AddAsync(attachment);
        return attachment;
    }
}