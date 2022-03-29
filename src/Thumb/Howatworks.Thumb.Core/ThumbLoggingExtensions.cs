using Howatworks.SubEtha.Common.Logging;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Howatworks.Thumb.Core
{
    public static class ThumbLoggingExtensions
    {
        public static ILoggingBuilder UseThumbLogging(this ILoggingBuilder builder, IConfiguration config)
        {
            var logFolder = config["LogFolder"];
            Directory.CreateDirectory(logFolder);

            GlobalContext.Properties["logfolder"] = logFolder;

            SubEthaLog.LogEvent += SubEthaLog_LogEvent;

            return builder;
        }

        private static void SubEthaLog_LogEvent(object sender, SubEthaLogEvent log)
        {
            var logger = LogManager.GetLogger(log.Source);
            switch (log)
            {
                case var d when log.Level == SubEthaLogLevel.Debug:
                    logger.Debug($"{d.Source}: {d.Message}");
                    break;
                case var w when log.Level == SubEthaLogLevel.Warn:
                    logger.Warn($"{w.Source}: {w.Message}", w.Exception);
                    break;
                case var e when log.Level == SubEthaLogLevel.Error:
                    logger.Error($"{e.Source}: {e.Message}", e.Exception);
                    break;
                default:
                    logger.Info($"{log.Source}: {log.Message}");
                    break;
            }
        }
    }
}
