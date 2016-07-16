using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DAL.Infrastructure
{
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
