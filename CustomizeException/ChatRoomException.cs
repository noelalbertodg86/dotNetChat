using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomizeException
{
    public class ExistingChatRoomException : Exception
    {
        public ExistingChatRoomException():
            base("The name of this ChatRoom is already taken")
        {
        }

        public ExistingChatRoomException(string message)
            : base(message)
        {
        }

        public ExistingChatRoomException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ChatRoomNotFound : Exception
    {
        public ChatRoomNotFound()
            : base("Sorry we can not find this chat room")
        {
        }

        public ChatRoomNotFound(string message)
            : base(message)
        {
        }

        public ChatRoomNotFound(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
