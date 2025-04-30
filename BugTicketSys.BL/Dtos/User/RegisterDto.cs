using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketSys.BL.Dtos.User
{


    public record RegisterDto(string username, string email, string password, string customproperty);

}
