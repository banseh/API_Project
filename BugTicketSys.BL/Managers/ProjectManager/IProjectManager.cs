using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.BL.Dtos.Project;
using BugTicketSys.DAL;

namespace BugTicketSys.BL.Managers.ProjectManager
{
    public interface IProjectManager
    {

        Task<GeneralResult<List<ProjectReadDto>>> GetAllProjects();
        Task<GeneralResult<ProjectReadDto?>> GetProjectById(Guid id);
        Task<GeneralResult> Add(ProjectAddDto project);
        Task<GeneralResult> Delete(ProjectReadDto project);
        Task<GeneralResult<ProjectReadDto>> Update(ProjectReadDto project);
        Task<int> savechangesAsync();

    }
}
