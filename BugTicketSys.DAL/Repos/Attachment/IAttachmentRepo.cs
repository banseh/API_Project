using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketSys.DAL;

public interface IAttachmentRepo : IGenericRepo<Attachment>
{
    Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId);
    Task<Attachment?> GetAttachmentByFilePathAsync(string filePath);
    Task<Attachment?> UploadAttachmentAsync(Guid bugId, Attachment attachment);
    Task<List<Attachment>?> GetAttachmentsByBugIdAsync(Guid bugId);
    Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId);
}
