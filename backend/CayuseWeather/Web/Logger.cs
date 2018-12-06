using log4net.Core;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    public class Logger : ILogger
    {
      
        private log4net.ILog Log { get; set; }

        public string Name => throw new NotImplementedException();

        public ILoggerRepository Repository => throw new NotImplementedException();

        public Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        public void Error(object msg)
        {
            Log.Error(msg);
        }

        public void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public void Info(object msg)
        {
            Log.Info(msg);
        }

        void ILogger.Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        void ILogger.Log(LoggingEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabledFor(Level level)
        {
            throw new NotImplementedException();
        }
    }
   
}

