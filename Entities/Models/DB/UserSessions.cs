using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class UserSessions
    {
        public int UserSessionsId { get; set; }
        public DateTime? LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public int UserId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
    }
}
