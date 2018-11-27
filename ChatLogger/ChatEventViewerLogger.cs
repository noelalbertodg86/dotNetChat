using System.Diagnostics;

namespace ChatLogger
{
    public class ChatEventViewerLogger
    {
        private string source = string.Empty;
        private string messageToLog = string.Empty;

        public ChatEventViewerLogger()
        {

        }

        public void setErrorLog(string source, string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Error);
        }
        public void setWarningLog(string source, string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Warning);
        }

        public void setInformationLog(string source, string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Information);
        }


    }
}
