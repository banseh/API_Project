using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.BL.Dtos.Bug;
using BugTicketSys.DAL;

namespace BugTicketSys.BL.Managers.BugManager
{
    public class BugManager : IBugManager
    {

        private readonly IUnitOfWork _unitOfWork;
        public BugManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<GeneralResult> Add(BugOtherDto bugo)
        {
            try
            {
                var pro = new Bug
                {
                    Description = bugo.Description,
                    Project_id = bugo.Project_id
                };
                await _unitOfWork.bugRepo.Add(pro);
                await _unitOfWork.savechangesAsync();

                return new GeneralResult
                {
                    Success = true,
                    Message = "Bug added successfully."
                };
            }
            catch (Exception ex) {
                return new GeneralResult
                {
                    Success = false,
                    Message = "An error occurred while adding the bug.",
                    Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            }
                };
            }

        }

        public async Task<GeneralResult> Delete(BugShowDto bug)
        {
            var bugidtodelete = await _unitOfWork.bugRepo.GetByIdAsync(bug.Id);
            try
            {
               
                    await _unitOfWork.bugRepo.Delete(bugidtodelete);
                    await _unitOfWork.savechangesAsync();
               
                return new GeneralResult
                {
                    Success = true,
                    Message = "Bug Deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult
                {
                    Success = false,
                    Message = "An error occurred while deleting the bug.",
                    Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            }
                };
            }
        }

        public async Task<GeneralResult<List<BugShowDto>>> GetAllbugs()
        {
            try
            {
                var bugfromdb = await _unitOfWork.bugRepo.GetAllAsync() ?? new List<Bug>();

                var bugs = bugfromdb.Select(B => new BugShowDto
                {
                    Id = B.Id,
                    Description = B.Description,
                    Project_id = B.Project_id,
                    Attachments = (B.Attachments ?? new List<Attachment>()).Select(A => new AttachmentViewDto
                    {
                        AttachmentId = A.AttachmentId,
                        FileName = A.FileName,
                        FilePath = A.FileUrl,
                        CreatedDate = A.CreatedDate
                    }).ToList()
                }).ToList();

                return new GeneralResult<List<BugShowDto>>
                {
                    Success = true,
                    Message = "Bugs fetched successfully.",
                    Data = bugs
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<List<BugShowDto>>
                {
                    Success = false,
                    Message = "An error occurred while fetching bugs.",
                    Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            },
                    Data = null
                };
            }
        }



        public async Task<GeneralResult<BugShowDto>> GetProjectById(Guid id)
        {
            try
            {
                var bugbyid = await _unitOfWork.bugRepo.GetByIdAsync(id);

                if (bugbyid == null)
                {
                    return new GeneralResult<BugShowDto>
                    {
                        Success = false,
                        Message = "Bug not found",
                        Data = null
                    };
                }

                var bugDto = new BugShowDto
                {
                    Id = bugbyid.Id,
                    Description = bugbyid.Description,
                    Project_id = bugbyid.Project_id,
                    Attachments = bugbyid.Attachments != null
                        ? bugbyid.Attachments.Select(A => new AttachmentViewDto
                        {
                            AttachmentId = A.AttachmentId,
                            FileName = A.FileName,
                            FilePath = A.FileUrl,
                            CreatedDate = A.CreatedDate
                        }).ToList()
                        : new List<AttachmentViewDto>()
                };

                return new GeneralResult<BugShowDto>
                {
                    Success = true,
                    Message = "Bug fetched successfully",
                    Data = bugDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<BugShowDto>
                {
                    Success = false,
                    Message = "An error occurred while fetching the bug",
                    Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            },
                    Data = null
                };
            }
        }


        public Task<int> savechangesAsync()
        {
            return _unitOfWork.savechangesAsync();
        }

        public async Task<GeneralResult> Update(BugShowDto bugOther)
        {
            var existingBug = await _unitOfWork.bugRepo.GetByIdAsync(bugOther.Id);

            if (existingBug == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Message = "Bug not found."
                };
            }

            existingBug.Description = bugOther.Description;


            await _unitOfWork.savechangesAsync();

            return new GeneralResult
            {
                Success = true,
                Message = "Bug updated successfully."
            };
        }

    }
}
