using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace BugTicketSys.DAL
{
    public interface IUnitOfWork
    {
        public  IprojectRepo ProjectRepo { get; }
        public IBugRepo bugRepo { get; }
        public IUserBugRepo UserBugRepo { get; }
        public IAttachmentRepo AttachRepo { get; }

        Task<int> savechangesAsync();
    }
}
