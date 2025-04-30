using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketSys.DAL
{
    public interface IUserBugRepo : IGenericRepo<User_Bugs>
    {
       public  Task<User_Bugs> GetByIdAsync(Guid userid , Guid bugid);
       
    }
}
