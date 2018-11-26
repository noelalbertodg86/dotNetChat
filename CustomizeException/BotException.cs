using System;
using System.Diagnostics;

namespace CustomizeException
{
    public class BotNotImplementedCommandException : Exception
    {
        public BotNotImplementedCommandException() :
            base()
        {
          
        }

        public BotNotImplementedCommandException(string message)
            : base(message)
        {
            //persist the error log in the pc event viewer
            EventLog.WriteEntry("BOT", message, EventLogEntryType.Error);
        }

        public BotNotImplementedCommandException(string message, Exception inner)
            : base(message, inner)
        {
            //persist the error log in the pc event viewer
            EventLog.WriteEntry("BOT", message, EventLogEntryType.Error);
        }
    }



}
