using log4net;
using log4net.Config;

namespace Cloudcre.Infrastructure.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly ILog _log;

        public Log4NetAdapter()
        {
            XmlConfigurator.Configure();
            //_log = LogManager.GetLogger(ApplicationSettingsFactory.GetApplicationSettings().LoggerName);
        }

        #region ILogger Members

        public void Log(string message)
        {
            _log.Info(message);
        }

        #endregion
    }
}