

namespace BugTicketSys.DAL
{
    public class User_Bugs
    {
        public Guid User_id { get; set; }
        public User User { get; set; } = null!;
        public Guid Bug_id { get; set; }
        public Bug Bug { get; set; } = null!;
    }
}
