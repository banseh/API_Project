using Microsoft.AspNetCore.Http;

namespace BugTicketSys.BL;

public class AttachmentUploadDto
{
    public IFormFile File { get; set; } = null!;
}
