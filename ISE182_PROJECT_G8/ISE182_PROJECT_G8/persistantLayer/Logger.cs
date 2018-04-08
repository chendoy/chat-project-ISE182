using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.persistantLayer
{
    public sealed class Logger  // singleton design pattern
    {
        private static Logger instance = null;
        private static readonly object padlock = new object();
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private constructor for singleton
        private Logger()
        {
            
        }

        public static Logger Instance
        {
            get
            {   //only if there is no instance lock object, otherwise return instance
                if (instance == null)
                {
                    lock (padlock) // senario: n threads in here,
                    {              //locking the first and others going to sleep till the first get new Instance
                        if (instance == null)  // rest n-1 threads no need new instance because its not null anymore.
                        {
                            instance = new Logger();
                        }
                    }
                }
                return instance;
            }
        }

        public void Info(string log)
        {
            logger.Info(log);
        }

        public void Warn(string log)
        {
            logger.Warn(log);
        }

        public void Error(string log)
        {
            logger.Error(log);
        }

        public void Fatal(string log)
        {
            logger.Fatal(log);
        }
    }
}
