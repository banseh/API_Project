using System.Diagnostics;
using Azure;
using BugTicketSys.BL;
using BugTicketSys.BL.Dtos.Bug;
using BugTicketSys.BL.Managers.BugManager;
using BugTicketSys.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTickectSysApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BugController: ControllerBase
    {


        


            private readonly IBugManager _Ibugmanger;
            private readonly IAttachmentManager _attachmentManager;

            public BugController(IBugManager Ipm,
                IAttachmentManager attachmentManager
                )
            {
            _Ibugmanger = Ipm;
            _attachmentManager = attachmentManager;
        }
            [HttpGet]
            public async Task<Results<Ok<GeneralResult<List<BugShowDto>>>, NotFound<GeneralResult<List<BugShowDto>>>>> Getall()
            {
            var response=await _Ibugmanger.GetAllbugs();
            if (!response.Success || response.Data == null)
            {
                return TypedResults.NotFound(response);
            }

            return TypedResults.Ok(response);
        }

            [HttpGet]
            [Route("{id}")]

        public async Task<Results<Ok<GeneralResult<BugShowDto>>, NotFound<GeneralResult<BugShowDto>>>> GetbyId(Guid id)
        {
            var response = await _Ibugmanger.GetProjectById(id);

            if (!response.Success || response.Data == null)
            {
                return TypedResults.NotFound(response);
            }

            return TypedResults.Ok(response);
        }



        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> Addbug(BugOtherDto new_bug)
        {
            var response = await _Ibugmanger.Add(new_bug);

            if (response.Success)
            {
               
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
 

        [HttpDelete]
         [Route("{id}")]

            public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> deleted(Guid id)
            {
                var delresult = await _Ibugmanger.GetProjectById(id);
            

            var response= await _Ibugmanger.Delete(delresult.Data);

            if (response.Success)
            {

                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }



        }



        [HttpPut]
        [Route("{id}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>, BadRequest<GeneralResult>>> Update_doc(Guid id, BugShowDto updated_bug)
        {
            if (id != updated_bug.Id)
            {
                var badRequestResult = new GeneralResult
                {
                    Success = false,
                    Message = "ID in URL does not match ID in body."
                };
                return TypedResults.BadRequest(badRequestResult);
            }

            var existingBug = await _Ibugmanger.GetProjectById(id);

            if (existingBug is null)
            {
                var notFoundResult = new GeneralResult
                {
                    Success = false,
                    Message = "Bug not found."
                };
                return TypedResults.NotFound(notFoundResult);
            }

            var updateResult = await _Ibugmanger.Update(updated_bug);

            if (updateResult.Success)
            {
                return TypedResults.Ok(updateResult);
            }
            else
            {
                return TypedResults.BadRequest(updateResult);
            }
        }





        /////////////////////////////////////////////////////////////////////////
        ///

        [HttpPost("{bugId:guid}/attachments")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UploadAttachment([FromRoute] Guid bugId, [FromForm] AttachmentUploadDto dto)
        {
            var response = await _attachmentManager
                .SaveAttachmentAsync(dto, bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpGet("{bugId:guid}/attachments")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetAttachmentsByBugId([FromRoute] Guid bugId)
        {
            var response = await _attachmentManager
                .GetAttachmentsByBugIdAsync(bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpDelete("{bugId:guid}/attachments/{attachmentId:guid}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> DeleteAttachment([FromRoute] Guid bugId, [FromRoute] Guid attachmentId)
        {
            var response = await _attachmentManager
                .DeleteAttachmentByIdAndBugIdAsync(bugId, attachmentId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }



    }
}
