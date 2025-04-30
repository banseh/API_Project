using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using BugTicketSys.BL.Dtos.Bug;
using BugTicketSys.BL.Dtos.Project;
using BugTicketSys.DAL;

namespace BugTicketSys.BL.Managers.ProjectManager
{
    public class ProjectManager : IProjectManager
    {
        //private readonly IprojectRepo _IPrepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ////////mapping///////////////////////////////////
        public async Task<GeneralResult> Add(ProjectAddDto project)
        {
            try
            {
                var pro = new Project
                {
                    Name = project.Name,
                    Title = project.Title
                };

                await _unitOfWork.ProjectRepo.Add(pro);
                await _unitOfWork.savechangesAsync();

                return new GeneralResult
                {
                    Success = true,
                    Message = "Project added successfully.",
                 
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult
                {
                    Success = false,
                    Message = "An error occurred while adding the project.",
                    Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            },
                };
            }
        }


        public async Task<GeneralResult> Delete(ProjectReadDto project)
        {
            var projectyidtodelete = await _unitOfWork.ProjectRepo.GetByIdAsync(project.Id);

            try
            {
                if (projectyidtodelete != null) {
                    _unitOfWork.ProjectRepo.Delete(projectyidtodelete);
                    await _unitOfWork.savechangesAsync();
                    return new GeneralResult
                    {
                        Success = true,
                        Message = "Project deleted successfully.",

                    };
                }

                return new GeneralResult
                {
                    Success = false,
                    Message = "Project Failed to Delete.",

                };


            }
            catch (Exception ex)
            {
                {
                    return new GeneralResult<ProjectReadDto>
                    {
                        Success = false,
                        Message = "An error occurred while deleted Projects.",
                        Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            },
                    };
                }


            }
        }

        public async Task<GeneralResult<List<ProjectReadDto>>> GetAllProjects()
        {
            var projectfromdb = await _unitOfWork.ProjectRepo.GetAllAsync();
            var response = projectfromdb.Select(p => new ProjectReadDto { Id = p.Id, Name = p.Name, Title = p.Title }).ToList();
            try
            {
                return new GeneralResult<List<ProjectReadDto>>
                {
                    Success = true,
                    Message = "Projects fetched successfully.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                {
                    return new GeneralResult<List<ProjectReadDto>>
                    {
                        Success = false,
                        Message = "An error occurred while fetching Projects.",
                        Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            },
                        Data = null
                    };
                }


            }
        }

        public async Task< GeneralResult< ProjectReadDto?>> GetProjectById(Guid id)
        {
           
            try
            {
                var projectyid = await _unitOfWork.ProjectRepo.GetByIdAsync(id);


                if (projectyid != null)
                {
                    var projectyidgetted = new ProjectReadDto { Id = projectyid.Id, Name = projectyid.Name, Title = projectyid.Title };

                    return new GeneralResult<ProjectReadDto?>
                    {
                        Success = true,
                        Message = "Project fetched successfully.",
                        Data = projectyidgetted
                    };
                }
                else
                {
                    return new GeneralResult<ProjectReadDto?>
                    {
                        Success = false,
                        Message = "Project not  Exist.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                {
                    return new GeneralResult<ProjectReadDto?>
                    {
                        Success = false,
                        Message = "An error occurred while fetching Project.",
                        Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            },
                        Data = null
                    };
                }


            }
        }

        public Task<int> savechangesAsync()
        {
            return _unitOfWork.savechangesAsync();
        }

        public async Task<GeneralResult<ProjectReadDto?>> Update(ProjectReadDto project)
        {

            var existingProject = await _unitOfWork.ProjectRepo.GetByIdAsync(project.Id);

           
                if (existingProject == null)
                {
                    return new GeneralResult<ProjectReadDto>
                    {
                        Success = false,
                        Message = "project not found."
                    };
                }

            

            existingProject.Name = project.Name;
            existingProject.Title = project.Title;

            await _unitOfWork.savechangesAsync();

            var updatedProject = new ProjectReadDto
            {
                Id = existingProject.Id,
                Name = existingProject.Name,
                Title = existingProject.Title
            };

            return new GeneralResult<ProjectReadDto>
            {
                Success = true,
                Message = "project updated successfully.",
                Data= updatedProject
            };
            //throw new NotImplementedException();

        }



        /*
         * 
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
 
         * 
         */
    }
}
