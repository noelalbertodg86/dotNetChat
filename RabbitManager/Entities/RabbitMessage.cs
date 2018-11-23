namespace RabbitManager
{
    /// <summary>
    /// model class to build and  standardize the rabbit message
    /// </summary>
    public class RabbitMessage
    {
        public string IdMessage = string.Empty;
        public string Message = string.Empty;

        public RabbitMessage()
        {
        }

        public RabbitMessage(string IdMessageParam, string MessageParam)
        {
            IdMessage = IdMessageParam;
            Message = MessageParam;
        }

    }
}
