namespace Server
{
    /// <summary>
    /// Response structure of chatcontroller
    /// </summary>
    /// <returns></returns>
    public class ResponseMessage
    {
        public string Codigo { get; set; }
        public string MensajeRetorno { get; set; }

        public ResponseMessage()
        {
            Codigo = string.Empty;
            MensajeRetorno = string.Empty;
        }

        public ResponseMessage(string CodigoParam, string MensajeRetornoParam)
        {
            Codigo = CodigoParam;
            MensajeRetorno = MensajeRetornoParam;
        }
    }
}
