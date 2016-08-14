namespace DAL.Infrastructure
{
    using System;
    using NLog;

    [Serializable]
    public class NLogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Default static ctor
        /// </summary>
        static NLogger()
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        private NLogger()
        {
        }

        public static Logger Logger { get { return logger; } }
    }

}
