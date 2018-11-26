using CustomizeException;

namespace bot
{
    class AdapterManager
    {
        IAdapter iAdapter = null;

        public AdapterManager()
        { }
        public IAdapter getInstanceByParam(string adapterBind, string parameters)
        {
            if (adapterBind == "APPL")
            {
                iAdapter = new CSV(parameters);
            }

            if (iAdapter == null)
            {
                throw new BotNotImplementedCommandException($"Sorry the command [{parameters}] is not implemented");
            }
            
            return iAdapter;

        }
    }
}
