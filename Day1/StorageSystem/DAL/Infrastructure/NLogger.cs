namespace DAL.Infrastructure
{
    using System;
    using NLog;

    [Serializable]
    public class NLogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static NLogger()
        {
        }

        private NLogger()
        {
        }

        public static Logger Logger { get { return logger; } }
    }

}
