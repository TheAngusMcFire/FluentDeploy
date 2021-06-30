using Serilog;

namespace FluentDeploy.Logging
{
    public class Logging
    {
        public void UseSerilogConsole(bool debug = false)
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

        public void SetLoggerConfiguration(Serilog.ILogger logger)
        {
            Log.Logger = logger;
        }
    }
}