namespace Unnamed.Common
{
    using System;

    public class SimpleLogger : ILogger
    {
        public void LogMessage(string message)
        {
            //Perhaps log message into Db.
            throw new NotImplementedException();
        }
    }
}
