using BugTicketSys.BL.Dtos.User;
using BugTicketSys.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTickectSysApi.Controllers
{
    namespace API.identity.Controllers
    {
        [ApiController]
        [Route("/api/[controller]")]
        public class Login : ControllerBase

        {
            private readonly IConfiguration _configuration;
            private readonly UserManager<User> _usermanager;

            public Login(IConfiguration _config, UserManager<User> usermng)
            {
                _configuration = _config;
                _usermanager = usermng;

            }
       

            [HttpPost]
            [Route("Login")]
            public async Task<Results<Ok<TokenDto>, UnauthorizedHttpResult>> Llogin(LoginDto login)
            {
                var user = await _usermanager.FindByNameAsync(login.username);
                if (user is null)
                {
                    return TypedResults.Unauthorized();
                }
                var passisvalid = await _usermanager.CheckPasswordAsync(user, login.password);
                if (!passisvalid)
                {
                    return TypedResults.Unauthorized();
                }
                var claims = await _usermanager.GetClaimsAsync(user);
                var tokendto = Generatetokn(claims.ToList());
                return TypedResults.Ok(tokendto);
            }
            [HttpPost]
            [Route("Register")]
            public async Task<Results<NoContent, BadRequest<List<string>>>> Register(RegisterDto register)
            {
                var user = new User
                {
                    UserName = register.username,
                    Email = register.email,
                    customproperty = register.customproperty,
                };
                var CreationResult = await _usermanager.CreateAsync(user, register.password);
                if (!CreationResult.Succeeded)
                {
                    var errors = CreationResult.Errors
                                .Select(e => e.Description)
                                .ToList();
                    return TypedResults.BadRequest(errors);
                }


                var claims = new List<Claim>
            {
            new (ClaimTypes.NameIdentifier , user.Id.ToString()) ,
             new (ClaimTypes.Name , user.UserName) ,
            new (ClaimTypes.Email , user.Email) ,
            new (ClaimTypes.Role , user.customproperty) ,
            };
                await _usermanager.AddClaimsAsync(user, claims);
                return TypedResults.NoContent();
            }

            private TokenDto Generatetokn(List<Claim> claims)
            {

                var secretkey = _configuration.GetValue<string>("secretkey");
                var secretkeyinbytes = Encoding.UTF8.GetBytes(secretkey);
                var key = new SymmetricSecurityKey(secretkeyinbytes);

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddDays(8),
                    claims: claims,
                    signingCredentials: new SigningCredentials(
                        key,
                        SecurityAlgorithms.HmacSha256));
                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                return new TokenDto(tokenstring, token.ValidTo);

            }


            //[HttpGet]
            //[Authorize]
            //public List<string> get()
            //{
            //    return new List<string>
            //{
            //    "xxx" ,
            //    "yyyy"
            //};
            //}
        }
    }
}
