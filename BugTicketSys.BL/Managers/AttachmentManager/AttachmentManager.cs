
using BugTicketSys.DAL;

namespace BugTicketSys.BL;
public class AttachmentManager : IAttachmentManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AttachmentUploadDtoValidator _validations;

    public AttachmentManager(IUnitOfWork unitOfWork,
        AttachmentUploadDtoValidator validations)
    {
        _unitOfWork = unitOfWork;
        _validations = validations;
    }
    public async Task<GeneralResult> SaveAttachmentAsync(AttachmentUploadDto request, Guid bugId)
    {
        var validationResult = await _validations.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var bug = await _unitOfWork.bugRepo
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found"
            };
        }
        var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
        var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var attachmentsFolder = Path.Combine(webRootPath, "attachments");

        if (!Directory.Exists(attachmentsFolder))
        {
            Directory.CreateDirectory(attachmentsFolder);
        }

        var filePath = Path.Combine(attachmentsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        var fileUrl = $"/attachments/{fileName}";

        var attachment = new Attachment
        {
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            FileUrl = fileUrl,
            Bug_id = bugId
        };

        await _unitOfWork.AttachRepo
            .Add(attachment);
        await _unitOfWork.savechangesAsync();

        return new GeneralResult<AttachmentViewDto>
        {
            Success = true,
            Message = "Attachment uploaded successfully",
            Data = new AttachmentViewDto
            {
                AttachmentId = attachment.AttachmentId,
                FileName = attachment.FileName,
                FilePath = $"http://localhost:5279{fileUrl}",
                CreatedDate = attachment.CreatedDate
            }
        };
    }
    public async Task<GeneralResult> GetAttachmentsByBugIdAsync(Guid bugId)
    {
        var attachments = await _unitOfWork.AttachRepo
            .GetAttachmentsByBugIdAsync(bugId);
        if (attachments == null || !attachments.Any())
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No attachments found for this bug"
            };
        }
        var attachmentDtos = attachments.Select(a => new AttachmentViewDto
        {
            AttachmentId = a.AttachmentId,
            FileName = a.FileName,
            FilePath = $"http://localhost:5279{a.FileUrl}",
            CreatedDate = a.CreatedDate
        }).ToList();
        return new GeneralResult<List<AttachmentViewDto>>
        {
            Success = true,
            Data = attachmentDtos
        };
    }
    public async Task<GeneralResult> DeleteAttachmentByIdAndBugIdAsync(Guid bugId, Guid attachmentId)
    {
        var bug = await _unitOfWork.bugRepo
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found"
            };
        }
        var attachment = await _unitOfWork.AttachRepo
            .GetAttachmentByIdAndBugIdAsync(attachmentId, bugId);
        if (attachment == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Attachment not found"
            };
        }
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", attachment.FileUrl);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var attachmentToDelete = await _unitOfWork.AttachRepo
            .GetByIdAsync(attachmentId);
        await _unitOfWork.AttachRepo
            .Delete(attachmentToDelete);
        await _unitOfWork.savechangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Attachment deleted successfully"
        };
    }
    public async Task<GeneralResult> GetAllAttachmentsAsync()
    {
        var attachments = await _unitOfWork.AttachRepo
            .GetAllAsync();
        if (attachments == null || !attachments.Any())
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No attachments found"
            };
        }
        var attachmentDtos = attachments.Select(a => new AttachmentViewDto
        {
            AttachmentId = a.AttachmentId,
            FileName = a.FileName,
            FilePath = $"http://localhost:5279{a.FileUrl}",
            CreatedDate = a.CreatedDate
        }).ToList();
        return new GeneralResult<List<AttachmentViewDto>>
        {
            Success = true,
            Data = attachmentDtos
        };
    }

    public async Task<GeneralResult> GetAttachmentByIdAsync(Guid attachmentId)
    {
        var attachment = await _unitOfWork.AttachRepo
            .GetByIdAsync(attachmentId);
        if (attachment == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Attachment not found"
            };
        }
        var attachmentDto = new AttachmentViewDto
        {
            AttachmentId = attachment.AttachmentId,
            FileName = attachment.FileName,
            FilePath = $"http://localhost:5279{attachment.FileUrl}",
            CreatedDate = attachment.CreatedDate
        };
        return new GeneralResult<AttachmentViewDto>
        {
            Success = true,
            Data = attachmentDto
        };
    }
}


