using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.BL.Dtos;
using BugTicketSys.BL.Dtos.Project;
using BugTicketSys.BL.Dtos.UserBug;
using BugTicketSys.DAL;

namespace BugTicketSys.BL.Managers.UserBugManager
{
    public class UserBugManager : IUserBugManager
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserBugManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task Add(UserBugDto userBugDto)
        //{
        //    if (string.IsNullOrEmpty(userBugDto.User_id) || string.IsNullOrEmpty(userBugDto.Bug_id ))
        //    {
        //        throw new ArgumentException("User_id and Bug_id must not be empty.");
        //    }
        //    var userBug = new User_Bugs
        //    {
        //        User_id = userBugDto.User_id,
        //        Bug_id = userBugDto.Bug_id
        //    };
        //    Console.WriteLine(userBug.Bug_id);
        //    Console.WriteLine(userBug.User_id);
        //    _unitOfWork.UserBugRepo.Add(userBug);
        //    await _unitOfWork.savechangesAsync();
        //}

        public async Task<GeneralResult> Add(UserBugDto userBugDto)
        {

            var buguserExists = await _unitOfWork.UserBugRepo.GetByIdAsync(userBugDto.Bug_id, userBugDto.User_id);
            var bugexist = await _unitOfWork.bugRepo.GetByIdAsync(userBugDto.Bug_id);

            try
            {
                if (buguserExists != null )
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Message = "UserBug Already exist.",
                    };
                }

                if (bugexist == null) {

                    return new GeneralResult
                    {
                        Success = false,
                        Message = "UserBug not exist in original tanle.",
                    };
                }

                var userBugx = new User_Bugs
                {
                    User_id = userBugDto.User_id,
                    Bug_id = userBugDto.Bug_id
                };

                await _unitOfWork.UserBugRepo.Add(userBugx);
                await _unitOfWork.savechangesAsync();

                return new GeneralResult<UserBugDto>
                {
                    Success = true,
                    Message = "UserBug added successfully.",
                  Data = new UserBugDto
                        {
                      User_id = userBugx.User_id,
                      Bug_id = userBugx.Bug_id
                  }
                };
            }
            catch (Exception ex) {
                return new GeneralResult<UserBugDto>
                {
                    Success = false,
                    Message = "An error occurred while adding the UserBug.",
                    Errors = new List<ResultError>
            {
                new ResultError { Message = ex.Message }
            },
                    Data = null
                };
            }
        }



        public async Task<GeneralResult> Delete(Guid userId, Guid bugId)
        {
          

                try
                {
                    var userBug = await _unitOfWork.UserBugRepo.GetByIdAsync(userId, bugId);
                if (userBug != null)
                {


                    await _unitOfWork.UserBugRepo.Delete(userBug);
                    await _unitOfWork.savechangesAsync();
                    return new GeneralResult<UserBugDto>
                    {
                        Success = true,
                        Message = "Userbug deleted successfully.",

                    };

                }
                return new GeneralResult
                {
                    Success = false,
                    Message = "Userbug Not Exist to Delete",

                };

            }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting the user-bug relation", ex);
                }

            }
        

        public async Task< GeneralResult<List<UserBugDto>>> GetAll()
        {
            var userbug = await _unitOfWork.UserBugRepo.GetAllAsync();
            var response= userbug.Select(p => new UserBugDto { User_id = p.User_id, Bug_id = p.Bug_id }).ToList();
            try
            {
                return new GeneralResult<List<UserBugDto>>
                {
                    Success = true,
                    Message = "Projects fetched successfully.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                {
                    return new GeneralResult<List<UserBugDto>>
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

        public async Task<GeneralResult<UserBugDto?>> GetById(Guid userId, Guid bugId)
        {
            try
            {
                var userBug = await _unitOfWork.UserBugRepo.GetByIdAsync(userId, bugId);

                if (userBug != null)
                {
                    var response = new UserBugDto
                    {
                        User_id = userBug.User_id,
                        Bug_id = userBug.Bug_id
                    };
                    return new GeneralResult<UserBugDto?>
                    {
                        Success = true,
                        Message = "UserProject fetched successfully.",
                        Data = response
                    };
                }
                else
                {
                    return new GeneralResult<UserBugDto?>
                    {
                        Success = false,
                        Message = "UserProject not  Exist.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                {
                    return new GeneralResult<UserBugDto?>
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

            /*
             * 
             *             try
            {
                var projectyid = await _unitOfWork.ProjectRepo.GetByIdAsync(id);


                if (projectyid != null)
                {
                    var projectyidgetted = new ProjectReadDto { Id = projectyid.Id, Name = projectyid.Name, Title = projectyid.Title };

                    return new GeneralResult<ProjectReadDto>
                    {
                        Success = true,
                        Message = "Project fetched successfully.",
                        Data = projectyidgetted
                    };
                }
                else
                {
                    return new GeneralResult<ProjectReadDto>
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
                    return new GeneralResult<ProjectReadDto>
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

             * */


        }


        public Task<int> savechangesAsync()
        {
            return _unitOfWork.savechangesAsync();
        }
        //public async Task Update(UserBugDto userBugDto)
        //{
        //    var userBug = await _unitOfWork.UserBugRepo.GetByIdAsync(userBugDto.User_id, userBugDto.Bug_id);
        //    if (userBug != null)
        //    {
        //        _unitOfWork.UserBugRepo.Update(userBug);
        //    }
        //}
    }
}
