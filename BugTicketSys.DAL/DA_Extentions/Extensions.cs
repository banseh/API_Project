using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BugTicketSys.DAL
{
    public static class Extensions
    {
        public static void AdddataAccessServices(this IServiceCollection services , IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("default");

            services.AddDbContext<BugticketContext>(options =>
                options.UseSqlServer(connectionString));



            services.AddScoped<IprojectRepo, projectRepo>();
            services.AddScoped<IBugRepo, BugRepo>();
            services.AddScoped<IUserBugRepo, UserBugRepo>();
            services.AddScoped<IAttachmentRepo, AttachmentRepo>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;   
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail= true;

            }).AddEntityFrameworkStores<BugticketContext>();


            ///////////////////////
            ///
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretkey = configuration.GetValue<string>("secretkey");
        var secretkeyinbytes = Encoding.UTF8.GetBytes(secretkey);
        var key = new SymmetricSecurityKey(secretkeyinbytes);

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key
        };

    }
    );

   

            //////////////////////////////
            ///

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    Constants.ForAdminOnly,
                    builder => builder
                        .RequireClaim(ClaimTypes.Role, "Manager", "Developer" , "Tester")
                        .RequireClaim(ClaimTypes.NameIdentifier)
                );

                //options.AddPolicy(
                //    Constants.ForAnyOne,
                //    builder => builder
                //        .RequireClaim(ClaimTypes.Role, "customValue")
                //        .RequireClaim(ClaimTypes.NameIdentifier)
                //);


                 options.AddPolicy(
                 Constants.ForAnyOne,
                 builder => builder
                  .RequireClaim(ClaimTypes.NameIdentifier)
                .RequireAssertion(context =>
                    {
                    var roles = context.User.FindAll(ClaimTypes.Role);
                     return roles.Any(role => role.Value == "customValue") ||
                   !roles.Any(role => new[] { "Manager", "Developer", "Tester" }.Contains(role.Value));
                          })
                           );

            });

            ///////

        }


    }
}
