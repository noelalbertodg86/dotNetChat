using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Messages
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public int OriginUserId { get; set; }
        public int? DestinyUserId { get; set; }
        public int? ChatRoomId { get; set; }
        public DateTime MessageDateTime { get; set; }

        public ChatRoom ChatRoom { get; set; }
        public User OriginUser { get; set; }
    }
}
