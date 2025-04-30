using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.BL.Dtos;
using BugTicketSys.BL.Dtos.UserBug;

namespace BugTicketSys.BL.Managers.UserBugManager
{
    public interface IUserBugManager
    {
        /// no update commposite key not updated
  
        Task<GeneralResult<List<UserBugDto>>> GetAll();
        Task<GeneralResult<UserBugDto?>> GetById(Guid userId, Guid bugId);
        Task <GeneralResult> Add(UserBugDto userBug);
        Task  <GeneralResult> Delete(Guid userId, Guid bugId);
      
        Task<int> savechangesAsync();
    }
}
