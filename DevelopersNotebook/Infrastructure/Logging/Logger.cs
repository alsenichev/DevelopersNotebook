using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace DevelopersNotebook.Infrastructure.Logging{

    public class Logger{

        public static void Setup(string logFile){
            Hierarchy hierarchy = (Hierarchy) LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout{
                ConversionPattern =
                    "%date %level message=%message method=%method line=%line thread=%thread %newline"
            };
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender{
                AppendToFile = true,
                File = logFile,
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "3MB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;
        }

    }

}