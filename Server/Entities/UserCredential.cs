using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Model of input json with login data
    /// </summary>
    /// <returns></returns>
    public class UserCredential
    {
        public string user { get; set; }
        public string password { get; set; }

        public string keepSectionAlive { get; set; }

        public int chatRoomId { get; set; }
    }
}
