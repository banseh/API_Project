using BugTicketSys.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;



namespace BugTickectSysApi.Controllers
{


  [ApiController]
    [Route("api/[controller]")]
    public class ValueController : ControllerBase
        {
            private readonly UserManager<User> _userManager;
            public ValueController(UserManager<User> UUserManager)
            {
                _userManager = UUserManager;
            }
            [HttpGet]
            [Authorize]
            public async Task<IEnumerable<string>> Get()
            {
                return new string[] {"value1" , "Value2"};
            }



        [HttpGet("for-admin")]
       [Authorize(Constants.ForAdminOnly)]
        public async Task<IEnumerable<string>> Getadmin()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            //////noteeeeeee // Assume id is stored in nameidentifier

            var user2 = await _userManager.GetUserAsync(User);
            return new List<string>()
            {
                "value_admin===>username&& mail" ,

                user2.UserName,
                user2.Email
            };

        }

        [HttpGet("for-anyone")]
        [Authorize(Constants.ForAnyOne)]
        public async Task<IEnumerable<string>> GetAnyone()
        {

            //User is a prop in ControllerBase of type ClaimsPrincipal
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            //////noteeeeeee // Assume id is stored in nameidentifier

            var user2 = await _userManager.GetUserAsync(User);

            return new List<string>()
        {
            "value_user1" ,
            "value_user2" ,
            "value_user3"
        };

        }
    }
    }

