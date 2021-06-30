using Serilog;

namespace FluentDeploy.Logging
{
    public class Logging
    {
        public static void UseSerilogConsole(bool debug = false)
        {
            var config = new LoggerConfiguration()
                .WriteTo.Console();

            if (debug)
            {
                config.MinimumLevel.Debug();
            }
            else
            {
                config.MinimumLevel.Information();
            }

            Log.Logger = config.CreateLogger();
        }

        public static void SetLoggerConfiguration(Serilog.ILogger logger)
        {
            Log.Logger = logger;
        }
    }
}