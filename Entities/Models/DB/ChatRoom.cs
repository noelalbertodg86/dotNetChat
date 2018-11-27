using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class ChatRoom
    {
        public ChatRoom()
        {
            Messages = new HashSet<Messages>();
        }

        public int ChatRoomId { get; set; }
        public string Name { get; set; }

        public ICollection<Messages> Messages { get; set; }
    }
}
