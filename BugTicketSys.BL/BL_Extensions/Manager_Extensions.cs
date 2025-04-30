using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.BL.Managers;
using BugTicketSys.BL.Managers.BugManager;
using BugTicketSys.BL.Managers.ProjectManager;
using BugTicketSys.BL.Managers.UserBugManager;
using BugTicketSys.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketSys.BL
{
    public  static class Manager_Extensions
    {

        public static void Addbuissinessservices(this IServiceCollection services)
        {
            {
                services.AddScoped<IProjectManager, ProjectManager>();
                services.AddScoped<IBugManager, BugManager>();
                services.AddScoped<IBugManager, BugManager>();
                services.AddScoped<IUserBugManager, UserBugManager>();
                services.AddScoped<IAttachmentManager, AttachmentManager>();
                services.AddValidatorsFromAssembly(
                    typeof(Manager_Extensions).Assembly
                    );
            }
        }
    }
}
