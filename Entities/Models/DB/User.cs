using System;
using System.Collections.Generic;

namespace Entities
{ 
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Messages>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyDepartment { get; set; }

        public ICollection<Messages> Messages { get; set; }
    }
}
