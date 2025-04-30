using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BugTicketSys.DAL
{
    public class User:IdentityUser<Guid>
    {
        public string? customproperty { get; set; }
        public ICollection<User_Bugs> User_Bugs { get; set; } = null!;
    }
}
