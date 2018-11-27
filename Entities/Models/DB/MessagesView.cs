using System;
using System.Collections.Generic;

namespace Entities
{
    public  class MessageView
    {
        public string MessageText { get; set; }
        public int OriginUserId { get; set; }
        public string OriginUserName { get; set; }
        public int? DestinyUserId { get; set; }
        public string DestinyUserName { get; set; }
        public int? ChatRoomId { get; set; }
        public DateTime MessageDateTime { get; set; }
    }
}
