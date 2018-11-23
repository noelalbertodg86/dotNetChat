namespace Server
{
    /// <summary>
    /// Model the data of input json  message  from a user 
    /// </summary>
    /// <returns></returns>
    public class ChatMessaje
    {
        public string user { get; set; }
        public string message { get; set; }

        public int chatroom { get; set; }
    }
}
