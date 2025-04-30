using Azure;
using BugTicketSys.BL;
using BugTicketSys.BL.Dtos.Project;
using BugTicketSys.BL.Dtos.UserBug;
using BugTicketSys.BL.Managers.UserBugManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTickectSysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBugController : ControllerBase
    {
        private readonly IUserBugManager user_BugManager;

        public UserBugController(IUserBugManager _userBugManager)
        {
            user_BugManager = _userBugManager;
        }

        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> AddUserBug(UserBugDto new_UB)
        {
          var response= await  user_BugManager.Add(new_UB);
            Console.WriteLine(new_UB.Bug_id );
            Console.WriteLine(new_UB.User_id );
            if (!response.Success )
            {
                return TypedResults.NotFound(response);
            }
            return TypedResults.Ok(response);
        }


        [HttpGet]
        public async Task<Results<Ok<GeneralResult<List<UserBugDto>>>, NotFound<GeneralResult<List<UserBugDto>>>>> Getall()
        {
          var response=  await user_BugManager.GetAll();
            if (!response.Success || response.Data == null)
            {
                return TypedResults.NotFound(response);
            }

            return TypedResults.Ok(response);
        }

      

        [HttpGet]
        [Route("{userId}/{bugId}")]
        public async Task<Results<Ok<GeneralResult<UserBugDto>>, NotFound<GeneralResult<UserBugDto>>>> GetById(Guid userId, Guid bugId)
        {
            var response = await user_BugManager.GetById(userId, bugId);
            if (!response.Success || response.Data == null)
            {
                return TypedResults.NotFound(response);
            }

            return TypedResults.Ok(response);
        }


        [HttpDelete]
        [Route("{userId}/{bugId}")]

        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> deleted(Guid userId, Guid bugId)
        {
            var del = await user_BugManager.GetById(userId , bugId);
          


           var response=  await user_BugManager.Delete(userId, bugId);


            if (response.Success)
            {

                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }



        }



    }
}
