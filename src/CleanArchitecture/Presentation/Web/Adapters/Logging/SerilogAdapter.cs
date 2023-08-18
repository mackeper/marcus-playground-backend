using Application.Logging;

namespace Web.Adapters.Logging;
public class SerilogAdapter : Application.Logging.ILogger
{
    private readonly Serilog.ILogger logger;

    public SerilogAdapter(Serilog.ILogger logger)
    {
        this.logger = logger;
    }

    public void Debug(string message) => logger.Debug(message);
    public void Error(string message, Exception exception) => logger.Error(message, exception);
    public void Fatal(string message, Exception exception) => logger.Fatal(message, exception);
    public void Information(string message) => logger.Information(message);
    public void Warning(string message) => logger.Warning(message);
}
