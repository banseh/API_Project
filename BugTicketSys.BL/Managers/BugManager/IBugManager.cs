using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.BL.Dtos.Bug;

namespace BugTicketSys.BL
{
    public interface IBugManager
    {

        Task<GeneralResult<List<BugShowDto>>> GetAllbugs();
        Task<GeneralResult<BugShowDto>> GetProjectById(Guid id);
        Task<GeneralResult> Add(BugOtherDto bug);
        Task<GeneralResult> Delete(BugShowDto bug);
        Task<GeneralResult> Update(BugShowDto bug);
        Task<int> savechangesAsync();
    }
}
