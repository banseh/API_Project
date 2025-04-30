namespace BugTickectSysApi.Controllers
{
    using BugTicketSys.BL;
    using BugTicketSys.BL.Dtos;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]

    public class AttachmentController:ControllerBase
    {

        private readonly IAttachmentManager _Iattachmanger;

        public AttachmentController(IAttachmentManager Iattach)
        {
            _Iattachmanger = Iattach;
        }


        [HttpGet]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetAllAttachments()
        {
            var response = await _Iattachmanger
                .GetAllAttachmentsAsync();
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }

        [HttpGet("{attachmentId:guid}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetAttachmentById([FromRoute] Guid attachmentId)
        {
            var response = await _Iattachmanger
                .GetAttachmentByIdAsync(attachmentId);
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
