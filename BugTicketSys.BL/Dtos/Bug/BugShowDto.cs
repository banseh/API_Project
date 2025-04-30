using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketSys.DAL;

namespace BugTicketSys.BL.Dtos.Bug;

public class BugShowDto
{

    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;


    public Guid Project_id { get; set; }

    public ICollection<AttachmentViewDto>? Attachments { get; set; }

}
