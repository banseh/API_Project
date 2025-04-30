using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using BugTicketSys.BL;
using BugTicketSys.BL.Dtos;
using BugTicketSys.BL.Dtos.Bug;
using BugTicketSys.BL.Dtos.Project;
using BugTicketSys.BL.Managers.ProjectManager;
using BugTicketSys.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BugTickectSysApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProjectController:ControllerBase
    {


        private readonly IProjectManager _Iprojectmanager;
        private readonly UserManager<User> _usermanager;

        public ProjectController(IProjectManager  Ipm  , UserManager<User> User_Manager)
        {
            _Iprojectmanager = Ipm;
            _usermanager= User_Manager;
        }
        [HttpGet]
        public async Task<Results<Ok<GeneralResult<List<ProjectReadDto>>>, NotFound<GeneralResult<List<ProjectReadDto>>>>> Getall()
        {
            var response=await _Iprojectmanager.GetAllProjects();

            if (!response.Success || response.Data == null)
            {
                return TypedResults.NotFound(response);
            }

            return TypedResults.Ok(response);
        }

        [HttpGet]
        [Route("{id}")]

          public async Task<Results<Ok<GeneralResult<ProjectReadDto>>, NotFound<GeneralResult<ProjectReadDto>>>> GetbyId(Guid id)
        {
            var response = await _Iprojectmanager.GetProjectById(id);
            if (!response.Success || response.Data == null)
            {
                return TypedResults.NotFound(response);
            }

            return TypedResults.Ok(response);
        }

        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> Addproject(ProjectAddDto new_Projectrea)
        {
          var response=  await _Iprojectmanager.Add(new_Projectrea);
            if (!response.Success )
            {
                return TypedResults.NotFound(response);
            }



            return TypedResults.Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> deleted(Guid id)
        {
            var del = await _Iprojectmanager.GetProjectById(id);
            if (del.Data == null)
            {
                return TypedResults.NotFound(new GeneralResult
                {
                    Success = false,
                    Message = "Project not found",
                });
            }

            var response= await _Iprojectmanager.Delete(del.Data);

            if (!response.Success )
            {
                return TypedResults.NotFound(response);
            }

            return TypedResults.Ok(response);


        }



        [HttpPut]
        [Route("{id}")]

        public async Task<Results<Ok<GeneralResult<ProjectReadDto>>, NotFound<GeneralResult<ProjectReadDto>>, BadRequest>> Update_doc(Guid id, ProjectReadDto updated_proj)
        {
            if (id != updated_proj.Id)
                return TypedResults.BadRequest();

            var new_upddate_doc = await _Iprojectmanager.GetProjectById(id);
            if (new_upddate_doc is null)
            {
                return TypedResults.NotFound(new GeneralResult<ProjectReadDto>
                {
                    Success = false,
                    Message = "Project not found"
                });
            }

            var response= await _Iprojectmanager.Update(updated_proj);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }

            return TypedResults.NotFound(response);   
        
        }





            /////////////////////////////////////////////////////////////////////////
            ///


        }
}
